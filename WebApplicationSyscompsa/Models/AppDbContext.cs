using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplicationSyscompsa.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<User_web>  user_web  { get; set; }
        public DbSet<Des_notes> des_notes { get; set; }
        public DbSet<Clasnotes> clasnotes { get; set; }


        //asignamos el valor de los decimales que estan truncandose mediante c#
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CAMBIAMOS EL PRIMARY KEY QUE VIEN POR DEFECTO EN ID Y ESCIFICAMOS EL QUE QUEREMOS USAR
            #region
            modelBuilder.Entity<User_web>().HasKey(pk => pk.token_user).HasName("codec_worker");
            modelBuilder.Entity<User_web>().HasKey(pk => pk.email_user).HasName("codec_worker");
            modelBuilder.Entity<Clasnotes>().HasKey(pk => pk.n_class).HasName("codec_worker");
            #endregion

            //EVITAMOS EL CRASHED(COFLICTO) DE DATOS
            #region
            modelBuilder.Entity<Des_notes>().Property(a => a.campoB).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Des_notes>().Property(a => a.cantidad).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Des_notes>().Property(a => a.time).HasColumnType("decimal(10,2)");
            #endregion

        }

    }
}
