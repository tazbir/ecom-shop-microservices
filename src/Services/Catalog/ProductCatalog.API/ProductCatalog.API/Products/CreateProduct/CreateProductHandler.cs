using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.CQRS;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, string Description, List<string> Category, decimal Price, string ImageFile)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler: ICommandHandler<CreateProductCommand, CreateProductResult>
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
        
        //TODO: Save product to database
        
        return new CreateProductResult(Guid.NewGuid());
    }
}