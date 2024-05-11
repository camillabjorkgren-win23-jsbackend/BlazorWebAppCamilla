using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppCamilla.Data;

public class UserAddress
{
    public int Id { get; set; }
    public string AddressType { get; set; } = null!;
    public string AddressLine_1 { get; set; } = null!;
    public string? AddressLine_2 { get; set; }

    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;

    [ForeignKey(nameof(User))]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
