using ProductCatalog.API.Models;

namespace ProductCatalog.API.Products.GetProducts;

public record GetProductResponse(IEnumerable<Product> Products);
public class GetProductsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products",
            async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
            .Produces<GetProductResponse>(200)
            .WithName("GetProducts")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all products")
            .WithDescription("Get all products in the catalog");
    }
}