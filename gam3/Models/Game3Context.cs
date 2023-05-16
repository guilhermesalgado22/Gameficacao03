using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gam3.Models
{
    public partial class Game3Context : DbContext
    {
        public Game3Context()
        {
        }

        public Game3Context(DbContextOptions<Game3Context> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; } // DbSet para a entidade Pedido
        public DbSet<ItemPedido> ItensPedido { get; set; } // DbSet para a entidade ItemPedido

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySql("server=localhost;database=game3;user=root;password=33551427Gui$", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
