using Base.CQRS;
using ProductCatalog.API.Models;

namespace ProductCatalog.API.Products.GetProducts;

public record GetProductsQuery(): IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger): IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products with this query: {query}", query);
        var products = await session.Query<Product>().ToListAsync(token: cancellationToken);
        return new GetProductsResult(products);
    }
}