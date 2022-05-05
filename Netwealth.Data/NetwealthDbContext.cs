using System;
using Microsoft.EntityFrameworkCore;
using NetWealth.Data.Entities;

namespace Netwealth.Data
{
    public class NetwealthDbContext : DbContext
    {
        public NetwealthDbContext(DbContextOptions<NetwealthDbContext> options) : base(options)
        { }

        public DbSet<CountryCurrency> CountryCurrencies { get; set; }

        public DbSet<ApiUser> ApiUsers { get; set; }
    }
}
