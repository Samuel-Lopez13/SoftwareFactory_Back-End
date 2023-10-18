using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Comentario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Comentario_Id { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public string Descripcion { get; set; } = null!;
    public int Usuario_Id { get; set; }
    public int Producto_Id { get; set; }
    
    //Relaciones
    public Usuario Usuarios { get; set; } = null!;
    public Producto Productos { get; set; } = null!;
}