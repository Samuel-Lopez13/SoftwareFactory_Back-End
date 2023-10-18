using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Producto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Producto_Id { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public double Precio { get; set; }
    public string Lenguaje { get; set; }
    public int Usuario_Id { get; set; }
    public int NumLikes { get; set; }
    public int NumComentarios { get; set; }
    public int PromedioDatos { get; set; }
    public int Promedio { get; set; }
    
    //Relaciones
    public Usuario Usuarios { get; set; } = null!;

    //Relaciones (Muchas)
    public virtual ICollection<Documento> Documentos { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public virtual ICollection<Comentario> Comentarios { get; set; }
    public virtual ICollection<Calificacion> Calificaciones { get; set; }
    public virtual ICollection<Compra> Compras { get; set; }
}