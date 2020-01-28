using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Authentication.Models
{
    public class UserToken
    {
        public int User_Id { get; set; }
        public string  Token { get; set; }
    }
}
