namespace BlazorWebAppCamilla.Data;

public class UserProfileRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<UserProfile> CreateUserProfileAsync(UserProfile userProfile)
    {
        _context.UserProfiles.Add(userProfile);
        await _context.SaveChangesAsync();
        return userProfile;
    }
}
