using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WavoProjects.Api.DatabaseModels;
using WavoProjects.Api.Hubs;
using WavoProjects.Api.Models;

namespace WavoProjects.Api.Workers
{
    public class AutoTimesheetSignOutWorker : BackgroundService
    {
        // This class is made to run things once at startup
        private WavoContext m_db;
        private IHubContext<WavOpsHub, IWavOpsHubClient> m_hub;
        private readonly IServiceProvider m_serviceProvider;
        private IServiceScope m_scope;
        private readonly ILogger<AutoTimesheetSignOutWorker> m_logger;

        public AutoTimesheetSignOutWorker(ILogger<AutoTimesheetSignOutWorker> logger, IServiceProvider serviceProvider, IHubContext<WavOpsHub, IWavOpsHubClient> hub)
        {
            m_logger = logger;
            m_serviceProvider = serviceProvider;
            m_scope = m_serviceProvider.CreateScope();
            m_db = m_scope.ServiceProvider.GetRequiredService<WavoContext>();
            m_hub = hub;
        }

        public override Task StartAsync(CancellationToken stoppingToken)
        {
            m_logger.LogInformation($"Starting AutoTimesheetSignOutWorker Service");
            return base.StartAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await WaitForNextRun(true, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                m_logger.LogInformation($"Signing out students who were signed in...");

                List<Timesheet> peopleWhoForgotToSignOut = await m_db.Timesheets.Where(i => i.ClockOut == null).ToListAsync(stoppingToken);
                foreach (Timesheet dummy in peopleWhoForgotToSignOut)
                {
                    dummy.ClockOut = DateTime.Now;
                    dummy.AutoClockOut = true;
                }
                await m_db.SaveChangesAsync(stoppingToken);
                await SendTimesheetUpdate();

                m_logger.LogInformation($"Automatically signed {peopleWhoForgotToSignOut.Count} students out");

                await WaitForNextRun(false, stoppingToken);

            }

        }

        private async Task WaitForNextRun(bool firstRun, CancellationToken cancellationToken)
        {
            int dayOfWeek = (int)DateTime.Now.DayOfWeek;

            if(!firstRun) { dayOfWeek = (dayOfWeek + 1) % 6; } // If it's not the first run, get tomorrow's logout time

            DateTime signOutTime = (await m_db.AutoSignOutTimes.SingleAsync(i => i.DayOfWeek == dayOfWeek)).SignOutTime;

            if (DateTime.Now.TimeOfDay > signOutTime.TimeOfDay && firstRun)
            {
                dayOfWeek = (dayOfWeek + 1) % 6;
                signOutTime = (await m_db.AutoSignOutTimes.SingleAsync(i => i.DayOfWeek == dayOfWeek)).SignOutTime;
            }

            TimeSpan waitTime = signOutTime.TimeOfDay - DateTime.Now.TimeOfDay;

            m_logger.LogInformation($"Next Auto Clock Out happening on {signOutTime.DayOfWeek} at {signOutTime.ToShortTimeString()}");
            m_logger.LogInformation($"Waiting {waitTime.TotalHours:F2}h to log students out.");
            await Task.Delay((int)waitTime.TotalMilliseconds, cancellationToken);
        }

        private async Task SendTimesheetUpdate()
        {
            m_logger.LogInformation($"Sending SignalR Timesheet Updates");
            await m_hub.Clients.Groups(WavOpsHub.kTimesheetGroup).UpdateTimesheets(await m_db.GetTimesheetTeamMembers());
        }
    }
}
