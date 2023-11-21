using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Services.Authentication
{
    public record AuthenticationResult(
        Guid Id,
        string FirstName,
        string Lastname,
        string Email,
        string Token);
}
