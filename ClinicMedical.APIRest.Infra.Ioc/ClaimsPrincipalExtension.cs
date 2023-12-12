using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClinicMedical.APIRest.Infra.Ioc
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirstValue("id"));
        }

        public static string GetLogin(this ClaimsPrincipal user)
        {
            return user.FindFirstValue("login");
        }
    }
}
