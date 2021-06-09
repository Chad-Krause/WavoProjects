using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WavoProjects.Api.DatabaseModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool Enabled { get; set; }
        public bool Confirmed { get; set; }
        public int? GraduationYear { get; set; }
        public int? YearJoined { get; set; }
        public DateTime? Birthday { get; set; }
        public int? ProfileImageId { get; set; }
        [JsonIgnore]
        public string Pin { get; set; }

        public Role Role { get; set; }

        [JsonIgnore]
        public Image ProfileImage { get; set; }

        public List<Tidbit> Tidbits { get; set; }


        public string FullName { get { return FirstName + " " + LastName; } }

    }

    public partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            entity.Property(i => i.Id).ValueGeneratedOnAdd();
            entity.HasOne(i => i.ProfileImage).WithOne(i => i.User).HasForeignKey("ProfileImageId");
            entity.Property(i => i.CreatedOn).ValueGeneratedOnAdd();
            entity.Property(i => i.UpdatedOn).ValueGeneratedOnAddOrUpdate();
            entity.Ignore(i => i.FullName);
        }
    }
}
