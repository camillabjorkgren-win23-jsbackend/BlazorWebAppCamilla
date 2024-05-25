namespace BlazorWebAppCamilla.Models;

public class CourseCardModel
{
    public string? Id { get; set; }
    public bool IsBestseller { get; set; }
    public string? ImageUri { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public decimal? Price { get; set; }
    public decimal? DiscountPrice { get; set; }

    public string? Hours { get; set; }
    public string? LikesInProcent { get; set; }
    public string? LikesInNumbers { get; set; }
    public bool? IsDigital { get; set; }
}
