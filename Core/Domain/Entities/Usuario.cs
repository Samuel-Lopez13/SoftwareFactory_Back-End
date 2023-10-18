using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Entities;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Usuario_Id { get; set; }
    
    [Required]
    [MaxLength(60)]
    public string Nombre { get; set; } = null!;
    
    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Correo { get; set; } = null!;
    
    [Required]
    [MaxLength(150)]
    public string? Descripcion { get; set; }
    
    [Required]
    public string Contrasena { get; set; } = null!;
    
    [Required]
    public string FotoPerfil { get; set; } = null!;
    
    //Relaciones (Muchas)
    public virtual ICollection<Producto> Productos { get; set; }
    public virtual ICollection<Calificacion> Calificaciones { get; set; }
    public virtual ICollection<Compra> Compras { get; set; }
    public virtual ICollection<Comentario> Comentarios { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
}