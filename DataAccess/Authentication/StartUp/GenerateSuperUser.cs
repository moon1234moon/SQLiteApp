using DataAccess.Authentication.DataAccess;
using DataAccess.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.StartUp
{
    public class GenerateSuperUser
    {
        private readonly AuthenticationEndpoint authenticationEndpoint;

        public GenerateSuperUser(AuthenticationEndpoint authenticationEndpoint)
        {
            this.authenticationEndpoint = authenticationEndpoint;

            UserModel user = new UserModel()
            {
                Username = "admin",
                FirstName = "Admin",
                LastName = "Admin",
                CreatedDate = DateTime.Now,
                UpdatedDate  =DateTime.Now
            };

            if (!Exists(user))
            {
                this.authenticationEndpoint.Register(user, "admin");
            }
        }

        private bool Exists(UserModel user)
        {
            return AuthDataAccess.CheckIfUsernameExist(user.Username);
        }
    }
}
