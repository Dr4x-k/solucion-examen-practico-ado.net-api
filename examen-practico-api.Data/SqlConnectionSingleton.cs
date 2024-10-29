using System;
using System.Data.SqlClient;

namespace examen_practico_api.Data;

public class SqlConnectionSingleton
{
  private static SqlConnectionSingleton _instance = null;
  private static readonly object _lock = new object();
  private readonly SqlConnection _connection;

  private SqlConnectionSingleton() {
    string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    _connection = new SqlConnection(connectionString);
  }

  public static SqlConnectionSingleton Instance {
    get {
      if (_instance == null) {
        lock (_lock) {
          if (_instance == null) {
            _instance = new SqlConnectionSingleton();
          }
        }
      }
      return _instance;
    }
  }

  public SqlConnection GetConnection() {
    if (_connection.State == System.Data.ConnectionState.Closed) {
      _connection.Open();
    }
    return _connection;
  }
}
