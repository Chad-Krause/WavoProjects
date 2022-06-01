using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class HubClient
    {
        public string Id { get; set; }
        public DateTime ConnectedOn { get; set; }
        public DateTime? DisconnectedOn { get; set; }
        public string IpAddress { get; set; }
    }

    public partial class HubClientConfiguration : IEntityTypeConfiguration<HubClient>
    {
        public void Configure(EntityTypeBuilder<HubClient> entity)
        {
            entity.Property(i => i.Id);
            entity.ToTable("HubClient");
        }
    }
}
