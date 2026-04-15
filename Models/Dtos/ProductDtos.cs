namespace GameHub.Api.Models.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class CreateProductDto
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class UpdateProductDto
{
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
