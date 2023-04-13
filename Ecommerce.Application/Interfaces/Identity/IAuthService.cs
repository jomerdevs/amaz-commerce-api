using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();
        string CreateToken(Usuario usuario, IList<string>? roles);
    }
}
