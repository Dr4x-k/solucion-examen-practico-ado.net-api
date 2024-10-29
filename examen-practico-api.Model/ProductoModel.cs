using System;

namespace examen_practico_api.Model;

public class ProductoModel {
  public int IdProducto { get; set; }
  public string Producto { get; set; }
  public string Descripcion { get; set; }
  public decimal Precio { get; set; }
  public int Stock { get; set; }
}
