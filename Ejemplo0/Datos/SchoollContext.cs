using Ejemplo0.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Ejemplo0.Datos
{
    public class SchoollContext : DbContext
    {
        public SchoollContext(DbContextOptions<SchoollContext> options ) : base(options) 
        { 
        
        }

        public DbSet<Studiante> Studiantes { get; set; }
        public DbSet<Grado> Grados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grado>().HasData(
                new Grado()
                {
                    IdGrados =1,
                    NombreGrado="Primer",
                    Seccion='A'
                },
                new Grado()
                {
                    IdGrados =2,
                    NombreGrado="Segundo",
                    Seccion='B'
                }
                );
            modelBuilder.Entity<Studiante>().HasData(
                new Studiante()
                {
                    IdStudiante= 1,
                    NombreStudiante= "Manolo Sanchez",
                    DateOfBirth= new DateTime(2018, 6, 15),
                    IdGrado= 1
                },
                new Studiante()
                {
                    IdStudiante= 2,
                    NombreStudiante= "Manue Pedrolo",
                    DateOfBirth= new DateTime(2016, 5, 12),
                    IdGrado= 2
                },
                new Studiante()
                {
                    IdStudiante= 3,
                    NombreStudiante= "Kequito Dulce",
                    DateOfBirth = new DateTime(2017, 8, 20),
                    IdGrado= 1
                }
                );
        }

    }
}
