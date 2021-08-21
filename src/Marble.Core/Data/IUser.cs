using System.Collections.Generic;
using System.Security.Claims;

namespace Marble.Core.Data
{
    public interface IUser
    {
        string Name { get; }

        bool IsAuthenticated();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}