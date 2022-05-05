using System;
using Microsoft.EntityFrameworkCore;
using NetWealth.Data.Entities;

namespace Netwealth.Data
{
    public class NetwealthDbContext : DbContext
    {
        public NetwealthDbContext(DbContextOptions<NetwealthDbContext> options) : base(options)
        { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Currency> Currencies { get; set; }

    }
}
