using Microsoft.AspNetCore.Identity;

namespace BLG.Infrastructure.customRepository;

public interface ITokenReposetory
{
    string createjwt(IdentityUser user, List<string> Roles);
}