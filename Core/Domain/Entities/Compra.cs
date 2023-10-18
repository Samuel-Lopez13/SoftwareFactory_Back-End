using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Compra
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Compra_Id { get; set; }
    public DateTime FechaCompra { get; set; }
    public int Producto_Id { get; set; }
    public int Usuario_Id { get; set; }
    
    //Relaciones
    public Producto Productos { get; set; } = null!;
    public Usuario Usuarios { get; set; } = null!;
}