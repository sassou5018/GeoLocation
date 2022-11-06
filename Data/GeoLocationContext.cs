using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeoLocation.Models;

namespace GeoLocation.Data
{
    public class GeoLocationContext : DbContext
    {
        public GeoLocationContext (DbContextOptions<GeoLocationContext> options)
            : base(options)
        {
        }

        public DbSet<GeoLocation.Models.GeoLoc> GeoLoc { get; set; } = default!;
    }
}
