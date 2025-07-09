using Section29.Core.DTO;
using Section29.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Section29.Core.ServiceContracts.Interface
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJWTToken(ApplicationUser user);
        ClaimsPrincipal? GetPrincipleFromJwtToken(string?  jwtToken);
    }
}
