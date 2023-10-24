using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGWalksPersistence
{
	public class NGAuthDbContext : IdentityDbContext
	{
        public NGAuthDbContext(DbContextOptions<NGAuthDbContext> options) : base(options) 
        {

        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "303e7a33-c035-4a0a-8eb5-95e796815594";
			var writerRoleId = "c6d9d9e8-8564-4796-a35d-742b867ee7f6";


			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Id = readerRoleId,
					ConcurrencyStamp = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper()
				},

				new IdentityRole
				{
					Id = writerRoleId,
					ConcurrencyStamp = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper()
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}

	}
}
