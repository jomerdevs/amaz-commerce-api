using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public static class AppRoles
    {
        public static readonly string GenericUser = "USER";
        public static readonly string Admin = "ADMIN";
    }
}
