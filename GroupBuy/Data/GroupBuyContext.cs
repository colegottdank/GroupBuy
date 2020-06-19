using GroupBuy.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GroupBuy.Data
{
	public class GroupBuyContext : DbContext
	{
		public DbSet<ShopToken> ShopToken { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Tier> Tier { get; set; }

		public GroupBuyContext()
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=groupbuy; Trusted_Connection=True; MultipleActiveResultSets=true");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<ShopToken>()
			//  .HasKey(p => new { p.Token, p.Shop });
		}

	}
}
