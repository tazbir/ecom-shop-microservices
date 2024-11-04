using Base.CQRS;

using ProductCatalog.API.Models;

namespace ProductCatalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler(IDocumentSession session): ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Category = command.Category
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        
        return new CreateProductResult(product.Id);
    }
}