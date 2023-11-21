using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeViagem.WEB.Models;

namespace GerenciadorDeViagem.WEB.Data
{
    public class GerenciadorDeViagemWEBContext : DbContext
    {
        public GerenciadorDeViagemWEBContext (DbContextOptions<GerenciadorDeViagemWEBContext> options)
            : base(options)
        {
        }

        public DbSet<GerenciadorDeViagem.WEB.Models.Viagem> Viagem { get; set; } = default!;
    }
}
