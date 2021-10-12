using APIUrnaEletronica.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUrnaEletronica.Data
{
    public class APIUrnaContext : DbContext
    {
        public DbSet<Candidatos> Candidatos { get; set; }
        public DbSet<Votos> Votos { get; set; }

        public APIUrnaContext(DbContextOptions<APIUrnaContext> opt) : base(opt)
        {

        }
    }
}
