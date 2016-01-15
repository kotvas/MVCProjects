using SportsStore.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SportsStore.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {

        public Boolean Authenticate(string userName, string password)
        {
            Boolean result = FormsAuthentication.Authenticate(userName, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
            }

            return result;
        }

    }
}