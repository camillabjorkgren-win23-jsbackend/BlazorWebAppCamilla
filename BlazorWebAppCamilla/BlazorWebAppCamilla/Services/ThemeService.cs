
using BlazorWebAppCamilla.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;

namespace BlazorWebAppCamilla.Services;
public class ThemeService(UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider, IJSRuntime jsRuntime)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public async Task SetThemeAsync(bool isDarkMode)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = await _userManager.GetUserAsync(authState.User);

        if (user != null)
        {
            user.DarkMode = isDarkMode;
            await _userManager.UpdateAsync(user);
        }
    }

    public async Task<bool> GetThemeAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = await _userManager.GetUserAsync(authState.User);

        return user?.DarkMode ?? false;
    }

    public async Task ApplyDarkModeStyles()
    {
        await _jsRuntime.InvokeVoidAsync("updateTheme", "dark");
    }

    public async Task ApplyLightModeStyles()
    {
        await _jsRuntime.InvokeVoidAsync("updateTheme", "light");
    }

}
