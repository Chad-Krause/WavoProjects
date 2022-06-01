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

namespace WavoProjects.Api.Workers
{
    public class StartUpWorker : BackgroundService
    {
        // This class is made to run things once at startup
        private WavoContext m_db;
        private readonly IServiceProvider m_serviceProvider;
        private IServiceScope m_scope;
        private readonly ILogger<StartUpWorker> m_logger;

        public StartUpWorker(ILogger<StartUpWorker> logger, IServiceProvider serviceProvider)
        {
            m_logger = logger;
            m_serviceProvider = serviceProvider;
            m_scope = m_serviceProvider.CreateScope();
            m_db = m_scope.ServiceProvider.GetRequiredService<WavoContext>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            m_logger.LogInformation($"Starting StartUpWorker Service");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            m_logger.LogInformation($"Setting all clients to disconnected...");

            // Any client that has a null DisconnectedAt should have it's DisconnectedAt set to provide accurate records
            List<HubClient> orphanedClients = await m_db.HubClients.Where(i => i.DisconnectedOn == null).ToListAsync(stoppingToken);
            foreach (HubClient orphan in orphanedClients)
            {
                orphan.DisconnectedOn = DateTime.Now;
            }
            await m_db.SaveChangesAsync(stoppingToken);

            m_logger.LogInformation($"Updated {orphanedClients.Count} disconnected clients.");
        }
    }
}
