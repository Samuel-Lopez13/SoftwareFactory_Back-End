using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;

public class Documento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Documento_Id { get; set; }
    public int Producto_Id { get; set; }
    public string Link { get; set; }
    public int Tipo { get; set; }  //1 Imagen 2 Video 3 Archivo
    
    //Relaciones
    public Producto Productos { get; set; } = null!;
}