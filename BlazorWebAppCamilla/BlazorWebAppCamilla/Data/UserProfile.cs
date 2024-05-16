using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppCamilla.Data;

public class UserProfile
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string? ProfileImg { get; set; } = "profile-image.svg";

    //Behöver inte fylla i vid create
    public string? Bio { get; set; }

}
