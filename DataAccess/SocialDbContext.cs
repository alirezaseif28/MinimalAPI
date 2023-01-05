using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SocialDbContext : DbContext
    {
        public SocialDbContext(DbContextOptions opt) : base(opt)
        {

        }
        public DbSet<Post> Post { get; set; }
    }
}
