using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NIMAP2.Models
{
    public class UserRepository
    {
        ProductContext context = new ProductContext();

        public SignUp validateUser(string Username, string Password)
        {
            return context.SignupTbl.FirstOrDefault(user =>
            user.UserName.Equals(Username, StringComparison.OrdinalIgnoreCase)
            && user.Password == Password);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}