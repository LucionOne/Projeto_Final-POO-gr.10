// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using Dapper;
// using MySql.Data.MySqlClient;

// namespace Templates
// {
//     /// <summary>
//     /// Generic repository base‑class that talks directly to a MariaDB table via Dapper.
//     /// All CRUD methods open a connection, run the SQL, and therefore persist instantly,
//     /// so no explicit “Save” call is really necessary.  Nevertheless we keep the
//     /// <c>WriteToDataBase()</c> stub so the class fulfils the same <c>IRepo<T></c>
//     /// contract as file‑based repositories.
//     /// </summary>
//     public abstract class MariaDbRepo<T> : IRepo<T> where T : IModel
//     {
//         private readonly string _connectionString;
//         protected abstract string TableName { get; }

//         protected MariaDbRepo(string connectionString)
//         {
//             _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
//         }

//         /// <summary>
//         /// Creates a new connection every time so that repo instances remain thread‑safe.
//         /// </summary>
//         protected IDbConnection Connection => new MySqlConnection(_connectionString);

//         #region CRUD

//         public virtual void Add(T item)
//         {
//             using var db = Connection;
//             var insertProps = typeof(T).GetProperties().Where(p => p.Name != nameof(IModel.Id));
//             var columns = string.Join(", ", insertProps.Select(p => p.Name));
//             var parameters = string.Join(", ", insertProps.Select(p => "@" + p.Name));

//             var sql = $"INSERT INTO {TableName} ({columns}) VALUES ({parameters}); SELECT LAST_INSERT_ID();";
//             db.Open();
//             var id = db.QuerySingle<int>(sql, item);
//             item.Id = id;
//         }

//         public virtual void Remove(T item) => RemoveAt(item.Id);

//         public virtual void RemoveAt(int id)
//         {
//             using var db = Connection;
//             var sql = $"DELETE FROM {TableName} WHERE Id = @Id";
//             db.Execute(sql, new { Id = id });
//         }

//         public virtual void UpdateById(int id, T item)
//         {
//             using var db = Connection;
//             var updateProps = typeof(T).GetProperties().Where(p => p.Name != nameof(IModel.Id));
//             var setClause = string.Join(", ", updateProps.Select(p => p.Name + " = @" + p.Name));

//             var sql = $"UPDATE {TableName} SET {setClause} WHERE Id = @Id";
//             var parameters = new DynamicParameters(item);
//             parameters.Add("Id", id);

//             db.Execute(sql, parameters);
//             item.Id = id;
//         }

//         public virtual T? GetById(int id)
//         {
//             using var db = Connection;
//             var sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
//             return db.QuerySingleOrDefault<T>(sql, new { Id = id });
//         }

//         public virtual List<T> GetAll()
//         {
//             using var db = Connection;
//             var sql = $"SELECT * FROM {TableName}";
//             return db.Query<T>(sql).AsList();
//         }

//         public virtual Dictionary<int, T> GetAllAsDictionary() => GetAll().ToDictionary(x => x.Id, x => x);

//         public virtual T? Last()
//         {
//             using var db = Connection;
//             var sql = $"SELECT * FROM {TableName} ORDER BY Id DESC LIMIT 1";
//             return db.QuerySingleOrDefault<T>(sql);
//         }

//         #endregion

//         /// <summary>
//         /// No‑op implementation kept only to respect the <c>IRepo<T></c> API.  Each CRUD
//         /// method already persists directly to MariaDB, so calling this method does
//         /// nothing by design.  You can, however, override it to wrap several
//         /// operations in a single transaction if your service layer expects that.
//         /// </summary>
//         public virtual void WriteToDataBase() { /* intentionally left blank */ }
//     }
// }
