using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncriptacinDistribuidos.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Assistance> Asistance { get; set; }
        public DbSet<Employee> Employee { get; set; }

        // Aquí puedes agregar más DbSets si tienes más entidades

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cadena de conexión a tu base de datos
            optionsBuilder.UseSqlServer("Server=DESKTOP-PU0TEGE;Database=dbAs;User Id=sa;Password=12345678;Trusted_Connection=True;TrustServerCertificate=true");

        }
    }

    }
