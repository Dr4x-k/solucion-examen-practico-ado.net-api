using System;
using System.Data;
using System.Data.SqlClient;
using examen_practico_api.Data.Interfaces;
using examen_practico_api.DTO.Producto;
using examen_practico_api.Model;

namespace examen_practico_api.Data.Services;

public class ProductoService : IProductoService {
  private readonly SqlConnection _connection;
  public ProductoService(SqlConnectionSingleton connectionSingleton) {
    _connection = connectionSingleton.GetConnection();
  }

  #region Create
  public async Task<ProductoModel?> Create(CreateProductoDTO createProductoDTO) {
    using (SqlCommand cmd = new SqlCommand("sp_producto_create", _connection)) {
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@producto", createProductoDTO.Producto);
      cmd.Parameters.AddWithValue("@descripcion", createProductoDTO.Descripcion);
      cmd.Parameters.AddWithValue("@precio", createProductoDTO.Precio);
      cmd.Parameters.AddWithValue("@stock", createProductoDTO.Stock);

      try {
        if (_connection.State == ConnectionState.Closed) {
          await _connection.OpenAsync();
        }

        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
          if (await reader.ReadAsync()) {
            return new ProductoModel {
              IdProducto = reader.GetInt32(0),
              Producto = reader.GetString(1),
              Descripcion = reader.GetString(2),
              Precio = reader.GetDecimal(3),
              Stock = reader.GetInt32(4)
            };
          }
        }
      } catch (Exception ex) {
      }
    }
    return null;
  }
  #endregion

  #region Update
  public async Task<ProductoModel?> Update(UpdateProductoDTO updateProductoDTO, int targetId) {
    ProductoModel? producto = null;

    using (SqlCommand cmd = new SqlCommand("sp_producto_update", _connection)) {
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@targetId", targetId);
        cmd.Parameters.AddWithValue("@producto", updateProductoDTO.Producto ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@descripcion", updateProductoDTO.Descripcion ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@precio", updateProductoDTO.Precio ?? (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@stock", updateProductoDTO.Stock ?? (object)DBNull.Value);

        try {
            if (_connection.State == ConnectionState.Closed) await _connection.OpenAsync();

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
                if (await reader.ReadAsync()) {
                    producto = new ProductoModel {
                        IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
                        Producto = reader.GetString(reader.GetOrdinal("producto")),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) 
                            ? null 
                            : reader.GetString(reader.GetOrdinal("descripcion")),
                        Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
                        Stock = reader.GetInt32(reader.GetOrdinal("stock"))
                    };
                }
            }
        } catch (Exception ex) {
            throw new Exception("Error al actualizar el producto", ex);
        }
    }

    return producto;
  }
  #endregion

  #region Remove
  public async Task<ProductoModel?> Remove(int targetId) {
    ProductoModel? producto = null;

    using(SqlCommand cmd =  new SqlCommand("sp_producto_remove", _connection)) {
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@targetId", targetId);

      try {
        if (_connection.State == ConnectionState.Closed) await _connection.OpenAsync();

        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
          if (await reader.ReadAsync()) {
            producto = new ProductoModel {
              IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
              Producto = reader.GetString(reader.GetOrdinal("producto")),
              Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
              ? null
              : reader.GetString(reader.GetOrdinal("descripcion")),
              Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
              Stock = reader.GetInt32(reader.GetOrdinal("stock"))
            };
          }
        }
      } catch (Exception ex) {
        throw new Exception("Ocurrió un error al borrar el producto", ex);
      }
    }

    return producto;
  }
  #endregion

  #region FindAll
  public async Task<List<ProductoModel?>> FindAll() {
    List<ProductoModel?> productos = new List<ProductoModel>();

    using (SqlCommand cmd = new SqlCommand("sp_producto_findall", _connection)) {
      cmd.CommandType = CommandType.StoredProcedure;

      try {
        if (_connection.State == ConnectionState.Closed) {
          await _connection.OpenAsync();
        }

        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
          while (await reader.ReadAsync()) {
            productos.Add(new ProductoModel
            {
              IdProducto = reader.GetInt32(reader.GetOrdinal("idProducto")),
              Producto = reader.GetString(reader.GetOrdinal("producto")),
              Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) 
                ? null 
                : reader.GetString(reader.GetOrdinal("descripcion")),
              Precio = reader.GetDecimal(reader.GetOrdinal("precio")),
              Stock = reader.GetInt32(reader.GetOrdinal("stock"))
            });
          }
        }
      } catch (Exception ex) {
        throw new Exception("Error al obtener productos", ex);
      }
      return productos;
    }
  }
  #endregion

  #region FindOne
  public async Task<ProductoModel?> FindOne(int targetId) {
    ProductoModel? producto = null;
    using (SqlCommand cmd = new SqlCommand("sp_producto_findone", _connection)) {
      cmd.CommandType = CommandType.StoredProcedure;

      cmd.Parameters.AddWithValue("@targetId", targetId);
      
      try {
        if (_connection.State == ConnectionState.Closed) await _connection.OpenAsync();

        using(SqlDataReader reader = await cmd.ExecuteReaderAsync()) {
          if (await reader.ReadAsync()) {
            producto = new ProductoModel {
              IdProducto = reader.GetInt32(0),
              Producto = reader.GetString(1),
              Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2),
              Precio = reader.GetDecimal(3),
              Stock = reader.GetInt32(4)
            };
          }
        }
      } catch (Exception ex) {
        throw new Exception("Error al obtener producto", ex);
      }
    }

    return producto;
  }
  #endregion
}
