using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class Image
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public byte[] Data { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        //public User User { get; set; }
    }
    public partial class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> entity)
        {
            entity.ToTable("Image");
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
