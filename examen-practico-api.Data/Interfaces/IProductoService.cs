using System;
using examen_practico_api.DTO.Producto;
using examen_practico_api.Model;

namespace examen_practico_api.Data.Interfaces;

public interface IProductoService {
  public Task<ProductoModel?> Create(CreateProductoDTO createProductoDTO);
  public Task<ProductoModel?> Update(UpdateProductoDTO updateProductoDTO, int targetId);
  public Task<ProductoModel?> Remove(int targetId);
  public Task<List<ProductoModel?>> FindAll();
  public Task<ProductoModel?> FindOne(int targetId);
}
