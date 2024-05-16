namespace BlazorWebAppCamilla.Data;

public class UserAddressRepository(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<UserAddress> CreateUserAddressAsync (UserAddress userAddress)
    {
        _context.UserAddresses.Add(userAddress);
        await _context.SaveChangesAsync();
        return userAddress;
    }
}
