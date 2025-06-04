// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Threading.Tasks;
// using MariaDbConnector; // Assume a MariaDB .NET connector is available
// using Templates;

// namespace unused;

// public class MariaDbRepo<T> : IRepository<T> where T : IModel, new()
// {
//     private readonly string _connectionString;
//     private readonly string _tableName;

//     public MariaDbRepo(string connectionString, string tableName)
//     {
//         _connectionString = connectionString;
//         _tableName = tableName;
//     }

//     public void Add(T item)
//     {
//         // Implement logic to insert item into MariaDB
//         // Example: using Dapper or ADO.NET
//         throw new NotImplementedException("Add method must be implemented for MariaDB.");
//     }

//     public void Remove(T item)
//     {
//         RemoveAt(item.Id);
//     }

//     public void RemoveAt(int id)
//     {
//         // Implement logic to delete item by id from MariaDB
//         throw new NotImplementedException("RemoveAt method must be implemented for MariaDB.");
//     }

//     public void UpdateById(int id, T item)
//     {
//         // Implement logic to update item by id in MariaDB
//         throw new NotImplementedException("UpdateById method must be implemented for MariaDB.");
//     }

//     public T? GetById(int id)
//     {
//         // Implement logic to get item by id from MariaDB
//         throw new NotImplementedException("GetById method must be implemented for MariaDB.");
//     }

//     public List<T> GetAll()
//     {
//         // Implement logic to get all items from MariaDB
//         throw new NotImplementedException("GetAll method must be implemented for MariaDB.");
//     }

//     public Dictionary<int, T> GetAllAsDictionary()
//     {
//         var list = GetAll();
//         var dict = new Dictionary<int, T>();
//         foreach (var item in list)
//         {
//             dict[item.Id] = item;
//         }
//         return dict;
//     }

//     public void WriteToDataBase()
//     {
//         // In a DB context, this might be a no-op or a transaction commit
//         // Implement if needed
//     }

//     public void ConfirmFileAndFolderExistence()
//     {
//         // Not needed for DB, but you could check connection or table existence
//     }
// }
