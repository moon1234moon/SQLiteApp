using DataAccess.Authentication.Helpers;
using DataAccess.Authentication.Models;
using DataAccess.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.DataAccess
{
    public class AuthenticationEndpoint : IAuthenticationEndpoint
    {
        public void Login(string username, string password)
        {
            try
            {
                if (Helper.CheckUsername(username))
                {
                    UserModel user = AuthDataAccess.AttemptLogin(username, password);
                    LoggedInUserModel LoggedUser = new LoggedInUserModel()
                    {
                        Id = user.Id,
                        Token = Helper.GetUserToken(user.Id)
                    };
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Register(UserModel user, string password, bool autoLogin = true)
        {
            string hashedPassword = Hasher.HashPassword(password);

            bool res = AuthDataAccess.SaveNewData("Users", ToListOfKeyValuePairs(user, hashedPassword));

            if (res == true && autoLogin == true)
            {
                Login(user.Username, password);
            }
        }

        private List<UserModel> MapVarToList(dynamic data)
        {
            List<UserModel> output = new List<UserModel>();

            foreach (var item in data)
            {
                var row = (IDictionary<string, object>)item;

                UserModel user = new UserModel()
                {
                    Id = Convert.ToInt32(row["Id"].ToString()),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    Username = row["Username"].ToString()
                };

                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    user.CreatedDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                }

                if (row["UpdatedDate"] != null && row["UpdatedDate"].ToString() != "")
                {
                    user.CreatedDate = Convert.ToDateTime(row["UpdatedDate"].ToString());
                }

                output.Add(user);
            }

            return output;
        }

        private List<KeyValuePair<string, string[]>> ToListOfKeyValuePairs(UserModel user, string password = null)
        {
            List<KeyValuePair<string, string[]>> output = new List<KeyValuePair<string, string[]>>();
            string[] value;

            value = new string[2] { "", user.FirstName };
            output.Add(new KeyValuePair<string, string[]>("FirstName", value));

            value = new string[2] { "", user.LastName };
            output.Add(new KeyValuePair<string, string[]>("LastName", value));

            value = new string[2] { "", user.Username };
            output.Add(new KeyValuePair<string, string[]>("Username", value));

            if (password != null)
            {
                value = new string[2] { "", password };
                output.Add(new KeyValuePair<string, string[]>("Password", value));
            }

            value = new string[2] { "", user.CreatedDate.ToString() };
            output.Add(new KeyValuePair<string, string[]>("CreatedDate", value));

            value = new string[2] { "", user.UpdatedDate.ToString() };
            output.Add(new KeyValuePair<string, string[]>("UpdatedDate", value));

            return output;
        }
    }
}
