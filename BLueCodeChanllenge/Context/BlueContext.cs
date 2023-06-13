using BLueCodeChanllenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.Reflection.Metadata;

namespace BLueCodeChanllenge.Context
{
    public class BlueContext : DbContext
    {
        public BlueContext(DbContextOptions<BlueContext> options) : base(options)
        {
        }
        public BlueContext()
        {
                
        }
        public DbSet<ShortUrls> Urls { get; set; }
    }

}
