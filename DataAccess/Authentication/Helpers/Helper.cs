using DataAccess.Authentication.DataAccess;
using DataAccess.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Authentication.Helpers
{
    public static class Helper
    {
        public static bool CheckUsername(string username)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(username))
            {
                return true;
            }

            return false;
        }

        public static string GetUserToken(int userId)
        {
            string token = GetUserTokenIfValidated(userId);

            if (token == null)
            {
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                byte[] key = Guid.NewGuid().ToByteArray();
                token = Convert.ToBase64String(time.Concat(key).ToArray());
                AuthDataAccess.SaveNewToken(new UserToken()
                {
                    User_Id = userId,
                    Token = token
                });
            }
            
            return token;
        }

        public static bool ValidateToken(string token)
        {
            byte[] data = Convert.FromBase64String(token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            if (when < DateTime.UtcNow.AddHours(-24))
            {
                return false;
            }

            return true;
        }

        private static string GetUserTokenIfValidated(int userId)
        {
            UserToken userToken = AuthDataAccess.GetUserToken(userId);

            if (ValidateToken(userToken.Token))
            {
                return userToken.Token;
            }

            DestoryUserToken(userToken);

            return null;
        }

        private static void DestoryUserToken(UserToken userToken)
        {
            AuthDataAccess.DestroyUserToken(userToken);
        }
    }
}
