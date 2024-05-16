using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppCamilla.Data;

public class UserAddress
{
    public string Id { get; set; }
    public string AddressType { get; set; } = null!;
    public string AddressLine_1 { get; set; } = null!;
    public string? AddressLine_2 { get; set; }

    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}
