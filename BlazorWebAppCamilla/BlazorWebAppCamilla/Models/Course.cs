
namespace BlazorWebAppCamilla.Models;
public class Course
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Image { get; set; } = null!;
    public string PartitionKey { get; set; } = "course";
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public string[]? Categories { get; set; }
    public string Title { get; set; } = null!;
    public string? Ingress { get; set; }
    public int StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? Likes { get; set; }
    public string? LikesInProcent { get; set; }
    public int Hours { get; set; }
    public Author[] Authors { get; set; } = null!;
    public Prices Prices { get; set; } = null!;
    public Content? Content { get; set; }
}


public class Author
{
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}
public class Prices
{
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
}

public class Content
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public Programdetail[]? Programdetails { get; set; }
}

public class Programdetail
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
