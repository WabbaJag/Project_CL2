using CL2.Models;
using CL2.Models.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CL2.Data
{
    public class CL2Context : IdentityDbContext<ApplicationUser>
    {
        public CL2Context(DbContextOptions<CL2Context> options) : base(options)
        {
        }
        //inicialización de tablas, si crean una clase, deben agregarla acá
        public DbSet<Administracion> Administraciones { get; set; }
        public DbSet<Legislativo> Legislativos { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Diputado> Diputados { get; set; }
        public DbSet<Fraccion> Fracciones { get; set; }
        public DbSet<ProyectoLey> ProyectoLeys { get; set; }
        public DbSet<ProyectoTema> ProyectoTemas { get; set; }
        public DbSet<ProyectoDiputado> ProyectoDiputados { get; set; }
        public DbSet<Comision> Comisiones { get; set; }
        public DbSet<ComisionDiputados> ComisionDiputados { get; set; }
        public DbSet<ComisionLegislativo> ComisionLegislativo { get; set; }
        public DbSet<Plenario> Plenarios { get; set; }
        public DbSet<IntegrantesPlenario> IntegrantesPlenarios { get; set; }
        public DbSet<TipoActividadComision> TipoActividadComisiones { get; set; }

        //Seccion de sesion de comision 
        public DbSet<SesionComision> SesionComision { get; set; }
        public DbSet<SesionComisionActividad> SesionComisionActividad { get; set; }
        public DbSet<DiscusionComision> DiscusionComision { get; set; }

        public DbSet<SesionPlenario> SesionPlenarios { get; set; }
        public DbSet<ComentarioPlenario> ComentarioPlenarios { get; set; }
        public DbSet<ControlPolitico> ControlPoliticos { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //para que funcione el migration de add identity
            base.OnModelCreating(modelBuilder);

            //Codigo para que las tablas se creen con nombres singulares
            modelBuilder.Entity<Administracion>().ToTable("Administracion");
            modelBuilder.Entity<Administracion>().HasIndex(b => b.AdministracionPeriodo).IsUnique();
            modelBuilder.Entity<Legislativo>().ToTable("Legislativo");
            modelBuilder.Entity<Legislativo>().HasIndex(b => b.AnnoLegislativo).IsUnique();
            modelBuilder.Entity<Tema>().ToTable("Tema");
            modelBuilder.Entity<Tema>().HasIndex(b => b.TemaDesc).IsUnique();

            modelBuilder.Entity<Diputado>().ToTable("Diputado");
            modelBuilder.Entity<Fraccion>().ToTable("Fraccion");
            modelBuilder.Entity<Fraccion>().HasIndex(b => b.NombreFraccion).IsUnique();
            modelBuilder.Entity<ProyectoLey>().ToTable("ProyectoLey");
            modelBuilder.Entity<ProyectoTema>().ToTable("ProyectoTema");
            modelBuilder.Entity<ProyectoDiputado>().ToTable("ProyectoDiputado");

            modelBuilder.Entity<Plenario>().ToTable("Plenario");
            modelBuilder.Entity<IntegrantesPlenario>().ToTable("IntegrantesPlenario");


            modelBuilder.Entity<Comision>().ToTable("Comision");
            modelBuilder.Entity<ComisionDiputados>().ToTable("ComisionDiputados");
            modelBuilder.Entity<ComisionLegislativo>().ToTable("ComisionLegislativo");

            //Instancias de Seseion Comision, seseion comision actividades, tipo activivades comision, discusion Comision
            modelBuilder.Entity<SesionComision>().ToTable("SesionComision");
            //Se convierte en unique la variale de comisionID y Numero Sesion
            modelBuilder.Entity<SesionComision>().HasIndex(sc => new { sc.ComisionLegislativoID, sc.NumeroSesionComision }).IsUnique();
            modelBuilder.Entity<SesionComisionActividad>().ToTable("SesionComisionActividad");
            modelBuilder.Entity<TipoActividadComision>().ToTable("TipoActividadComision");
            modelBuilder.Entity<DiscusionComision>().ToTable("DiscusionComision");

            //Instancias de Sesion Plenario, actividades y control político
            modelBuilder.Entity<SesionPlenario>().ToTable("SesionPlenario");
            modelBuilder.Entity<SesionPlenario>().HasIndex(s => new { s.PlenarioID, s.NumeroSesion }).IsUnique();
            
            modelBuilder.Entity<ComentarioPlenario>().ToTable("ComentarioPlenario");
            modelBuilder.Entity<ControlPolitico>().ToTable("ControlPolitico");


            //Creación de llave compuesta para AdministracionLegislativo
            modelBuilder.Entity<ProyectoTema>().HasKey(c => new { c.ProyectoLeyID, c.TemaID });
            modelBuilder.Entity<ProyectoDiputado>().HasKey(c => new { c.ProyectoLeyID, c.DiputadoID });

            //Creación de llave compuesta para DiscusionComision, SesionComisionActividad, SesionComision
            modelBuilder.Entity<SesionComisionActividad>().HasKey(sc => new { sc.SesionComisionActividadID, sc.SesionComisionID });
            modelBuilder.Entity<SesionComisionActividad>().Property(p => p.SesionComisionActividadID).ValueGeneratedOnAdd();


            modelBuilder.Entity<DiscusionComision>().HasKey(dc => new { dc.DiscusionComisionID, dc.SesionComisionActividadID, dc.SesionComisionID});
            modelBuilder.Entity<DiscusionComision>().Property(p => p.DiscusionComisionID).ValueGeneratedOnAdd();

            modelBuilder.Entity<IntegrantesPlenario>().HasKey(c => new { c.PlenarioID, c.DiputadoID });

            //Comisiones
            modelBuilder.Entity<ComisionDiputados>().HasKey(c => new { c.ComisionLegislativoID, c.DiputadoID });

        }


    }
}