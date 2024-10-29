using System;

namespace examen_practico_api.DTO.Producto;

public class UpdateProductoDTO
{
  public string? Producto { get; set; }
  public string? Descripcion { get; set; }
  public decimal? Precio { get; set; }
  public int? Stock { get; set; }
}
