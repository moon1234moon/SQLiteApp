using DataAccess.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.Helpers
{
    public class AuthQueryBuilder
    {
        public string GetByUsername(string username)
        {
            string query = "SELECT * FROM Users WHERE Username = '" + username + "'";

            return query;
        }

        public string LoginQuery(string username, string password)
        {
            string query = "SELECT * FROM Users Where Username = '" + username + "' AND Password = '" + Hasher.HashPassword(password) + "'";

            return query;
        }

        public string GetTokenByUserIdQuery(int userId)
        {
            string query = "SELECT * FROM UserToken WHERE User_Id " + userId;

            return query;
        }

        public string SaveUserTokenQuery(UserToken userToken)
        {
            string query = "INSERT INTO UserToken(User_Id, Token) VALUES(User_Id = " + userToken.User_Id + " And Token = " + userToken.Token + ")";

            return query;
        }

        public string DestoryUserTokenQuery(UserToken userToken)
        {
            string query = "DELETE FROM UserToken WHERE User_Id = " + userToken.User_Id + " And Token = " + userToken.Token;

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
