using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Connections
{
    public class QueryBuilder
    {
        public string GetAllQuery(string Table)
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table));
            }

            string query = "SELECT * FROM " + Table;

            return query;
        }

        public string GetByIdQuery(string Table, int id)
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table));
            }

            string query = "SELECT * FROM TABLE WHERE Id = " + id;

            return query;
        }

        public string GetSaveDataQuery(string Table, List<KeyValuePair<string, string[]>> Data)
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table));
            }

            string query = "INSERT INTO " + Table + "(";
            var last = Data[Data.Count - 1];
            List<string[]> values = new List<string[]>();
            foreach (var element in Data)
            {
                if (element.Equals(last))
                    query += element.Key + ")";
                else
                    query += element.Key + ",";
                values.Add(element.Value);
            }
            query += " VALUES(";
            string[] LastValue = values[values.Count - 1];
            foreach (var value in values)
            {
                if (value == LastValue)
                {
                    if (value[0] == "number")
                        query += value[1] + ");";
                    else
                        query += "'" + value[1] + "');";
                }
                else
                {
                    if (value[0] == "number")
                        query += value[1] + ",";
                    else
                        query += "'" + value[1] + "',";
                }
            }

            return query;
        }

        public string GetLastInsertedRowQuery(string Table)
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table));
            }

            string query = "SELECT * FROM " + Table;

            query += " WHERE Id = " + GetMaxIdQuery(Table);

            return query;
        }

        public string GetMaxIdQuery(string Table)
        {
            if (Table is null)
            {
                throw new ArgumentNullException(nameof(Table));
            }

            string query = "SELECT MAX(Id) FROM " + Table;

            return query;
        }
    }
}
