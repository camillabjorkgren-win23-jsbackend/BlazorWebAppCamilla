using BlazorWebAppCamilla.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppCamilla.Services;

public class UserDataService(UserManager<ApplicationUser> userManager, ApplicationUserRepository applicationUserRepository)
{

    private readonly UserManager<ApplicationUser> UserManager = userManager;
    private readonly ApplicationUserRepository ApplicationUserRepository = applicationUserRepository;

    public async Task<ApplicationUser> GetUserDataAsync(HttpContext httpContext)
    {
        try
        {
            var user = await UserManager.GetUserAsync(httpContext.User);
            if (user != null)
            {
                user = await ApplicationUserRepository.GetUserById(user.Id);
                return user;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null!;
    }
}

