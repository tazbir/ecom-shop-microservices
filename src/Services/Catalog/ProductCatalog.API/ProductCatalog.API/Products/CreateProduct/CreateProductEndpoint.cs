
namespace ProductCatalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, string Description, List<string> Category, decimal Price, string ImageFile);
    
public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            })
            .Produces<CreateProductResponse>(201)
            .WithName("CreateProduct")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status503ServiceUnavailable)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product in the catalog");
    }
}