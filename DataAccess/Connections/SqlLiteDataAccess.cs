using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Connections
{
    public class SqlLiteDataAccess
    {
        private readonly QueryBuilder query = new QueryBuilder();

        public SqlLiteDataAccess()
        {
            this.query = new QueryBuilder();
        }

        public dynamic SaveData(string table, List<KeyValuePair<string, string[]>> model)
        {
            using(IDbConnection cnn = new SQLiteConnection(ConnectionString()))
            {
                string q = query.GetSaveDataQuery(table, model);
                cnn.Execute(q);

                q = query.GetLastInsertedRowQuery(table);
                var output = cnn.Query(q, new DynamicParameters());
                return output;
            }
        }

        public dynamic GetById(string table, int id)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string q = query.GetByIdQuery(table, id);
                    var output = cnn.Query(q, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return false;
            }
        }

        public dynamic GetAll(string table)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string q = query.GetAllQuery(table);
                    var output = cnn.Query(q, new DynamicParameters());
                    return output.ToList();
                }
            }
            catch
            {
                return false;
            }
        }

        public bool TestConnection()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static string ConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
