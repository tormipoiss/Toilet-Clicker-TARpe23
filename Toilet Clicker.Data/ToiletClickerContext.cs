using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Toilet_Clicker.Core.Domain;

namespace Toilet_Clicker.Data
{
	public class ToiletClickerContext : IdentityDbContext<ApplicationUser>
	{
        public ToiletClickerContext(DbContextOptions<ToiletClickerContext> options) : base(options) { }
        public DbSet<Toilet> Toilets { get; set; }

		public DbSet<FileToDatabase> FilesToDatabase { get; set; }
		public DbSet<IdentityRole> IdentityRoles { get; set; }
		public DbSet<PlayerProfile> PlayerProfiles { get; set; }
	}
}
