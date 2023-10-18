using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Calificacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Calificacion_Id { get; set; }
    public int Producto_Id { get; set; }
    public int Usuario_Id { get; set; }

    //Relaciones
    public Producto Productos { get; set; } = null!;
    public Usuario Usuarios { get; set; } = null!;
}