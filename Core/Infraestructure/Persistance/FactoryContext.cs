using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infraestructure.Persistance;

public class FactoryContext : DbContext
{
    public FactoryContext(DbContextOptions<FactoryContext> options) : base(options) { }
    
    public DbSet<Usuario> Usuarios { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Usuario_Id);
            entity.Property(e => e.Nombre).IsRequired()
                .HasMaxLength(60);
            entity.HasIndex(e => e.Correo)
                .IsUnique();
            entity.Property(e => e.Descripcion).IsRequired();
            entity.Property(e => e.Contrasena).IsRequired();
            entity.Property(e => e.FotoPerfil).IsRequired();
        });
        
        modelBuilder.Entity<Producto>(entity => {
            entity.HasOne(u => u.Usuarios) //Modelo de clase desde la entidad original
                .WithMany(d => d.Productos) //Referencia ICollection
                .HasForeignKey(u => u.Usuario_Id) //LLave foranea
                .HasConstraintName("Fk_Producto_Id_Usuario");
        });
        
        modelBuilder.Entity<Documento>(entity => {
            entity.HasOne(u => u.Productos) //Modelo de clase desde la entidad original
                .WithMany(d => d.Documentos) //Referencia ICollection
                .HasForeignKey(u => u.Producto_Id) //LLave foranea
                .HasConstraintName("Fk_Documentos_Id_Producto");
        });

        modelBuilder.Entity<Like>(entity => {
            entity.HasOne(u => u.Productos) //Modelo de clase desde la entidad original
                .WithMany(d => d.Likes) //Referencia ICollection
                .HasForeignKey(u => u.Producto_Id) //LLave foranea
                .HasConstraintName("Fk_Like_Id_Producto")
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(u => u.Usuarios) //Modelo de clase desde la entidad original
                .WithMany(d => d.Likes) //Referencia ICollection
                .HasForeignKey(u => u.Usuario_Id) //LLave foranea
                .HasConstraintName("Fk_Like_Id_Usuario");
        });

        modelBuilder.Entity<Compra>(entity => {
            entity.HasOne(u => u.Productos) //Modelo de clase desde la entidad original
                .WithMany(d => d.Compras) //Referencia ICollection
                .HasForeignKey(u => u.Producto_Id) //LLave foranea
                .HasConstraintName("Fk_Compra_Id_Producto")
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(u => u.Usuarios) //Modelo de clase desde la entidad original
                .WithMany(d => d.Compras) //Referencia ICollection
                .HasForeignKey(u => u.Usuario_Id) //LLave foranea
                .HasConstraintName("Fk_Compra_Id_Usuario");
        });

        modelBuilder.Entity<Comentario>(entity => {
            entity.HasOne(u => u.Usuarios) //Modelo de clase desde la entidad original
                .WithMany(d => d.Comentarios) //Referencia ICollection
                .HasForeignKey(u => u.Usuario_Id) //LLave foranea
                .HasConstraintName("Fk_Comentario_Id_Usuario");

            entity.HasOne(u => u.Productos) //Modelo de clase desde la entidad original
                .WithMany(d => d.Comentarios) //Referencia ICollection
                .HasForeignKey(u => u.Producto_Id) //LLave foranea
                .HasConstraintName("Fk_Comentario_Id_Producto")
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Calificacion>(entity => {
            entity.HasOne(u => u.Productos) //Modelo de clase desde la entidad original
                .WithMany(d => d.Calificaciones) //Referencia ICollection
                .HasForeignKey(u => u.Producto_Id) //LLave foranea
                .HasConstraintName("Fk_Calificacion_Id_Producto")
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(u => u.Usuarios) //Modelo de clase desde la entidad original
                .WithMany(d => d.Calificaciones) //Referencia ICollection
                .HasForeignKey(u => u.Usuario_Id) //LLave foranea
                .HasConstraintName("Fk_Calificacion_Id_Usuario");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}