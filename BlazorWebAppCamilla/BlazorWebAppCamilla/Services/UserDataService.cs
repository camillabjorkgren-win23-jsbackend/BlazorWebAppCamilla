using BlazorWebAppCamilla.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppCamilla.Services;

public class UserDataService(UserManager<ApplicationUser> userManager, ApplicationUserRepository applicationUserRepository, IHttpContextAccessor httpContextAccessor)
{

    private readonly UserManager<ApplicationUser> UserManager = userManager;
    private readonly ApplicationUserRepository ApplicationUserRepository = applicationUserRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<ApplicationUser> GetUserDataAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
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
        }
        return null!;
    }

    public async Task<bool> UpdatePasswordAsync(ApplicationUser user, string newPassword)
    {
        try
        {
            var exisitingUser = await UserManager.FindByEmailAsync(user.Email!);
            if (exisitingUser != null)
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, newPassword);
                var result = await UserManager.UpdateAsync(user);
                if (result != null)
                {
                    user.Modified = DateTime.Now;
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> DeleteAccount(ApplicationUser user, bool checkDelete)
    {
        try
        {
            var exisitingUser = await UserManager.FindByEmailAsync(user.Email!);
            if (exisitingUser != null)
            {
                if (checkDelete)
                {
                    var result = await UserManager.DeleteAsync(exisitingUser);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }

    }
}

