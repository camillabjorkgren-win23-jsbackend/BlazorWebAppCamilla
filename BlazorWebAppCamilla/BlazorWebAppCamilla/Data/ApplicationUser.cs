using Microsoft.AspNetCore.Identity;

namespace BlazorWebAppCamilla.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string? UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }

    public DateTime? Created { get; set; }
    public DateTime? Modified { get; set; }

    public string? UserAddressId { get; set; }
    public UserAddress? UserAddress { get; set; }
}
