using Dapper;
using DataAccess.Authentication.Helpers;
using DataAccess.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.DataAccess
{
    public class AuthDataAccess
    {
        private static readonly AuthQueryBuilder queryBuilder = new AuthQueryBuilder();

        public static bool SaveNewToken(UserToken userToken)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.SaveUserTokenQuery(userToken);

                    cnn.Execute(query, new DynamicParameters());

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static UserToken GetUserToken(int userId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.GetTokenByUserIdQuery(userId);

                    var userToken = cnn.Query<UserToken>(query, new DynamicParameters()).ToList();

                    return userToken[0];
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool DestroyUserToken(UserToken userToken)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.DestoryUserTokenQuery(userToken);

                    cnn.Execute(query, new DynamicParameters());

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static UserModel AttemptLogin(string username, string password)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.LoginQuery(username, password);

                    var user = cnn.Query<UserModel>(query, new DynamicParameters()).ToList();

                    return user[0];
                }
            }
            catch
            {
                throw new Exception("Username or Password are Wrong");
            }
        }

        public static dynamic SaveNewData(string tablename, List<KeyValuePair<string, string[]>> data)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.GetSaveDataQuery(tablename, data);

                    cnn.Execute(query);

                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public static bool CheckIfUsernameExist(string username)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(ConnectionString()))
                {
                    string query = queryBuilder.GetByUsername(username);

                    var user = cnn.Query<UserModel>(query, new DynamicParameters()).ToList();

                    if(user.Count > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private static string ConnectionString(string id = "Authentication")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
