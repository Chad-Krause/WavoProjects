
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class Tidbit
    {
        public int UserId { get; set; }
        public int TidbitTypeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public User User { get; set; }
        public TidbitType TidbitType { get; set; }

    }

    public partial class TidbitConfiguration : IEntityTypeConfiguration<Tidbit>
    {
        public void Configure(EntityTypeBuilder<Tidbit> entity)
        {
            entity.ToTable("Tidbit");
            entity.HasKey(tidbit => new { tidbit.UserId, tidbit.TidbitTypeId });
        }
    }
}
