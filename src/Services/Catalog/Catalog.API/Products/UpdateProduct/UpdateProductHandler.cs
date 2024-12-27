
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile,
    decimal Price) : ICommand<UpdateCommandResult>;

public record UpdateCommandResult(bool IsSuccess);
internal class UpdateProductHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateCommandResult>
{
    public async Task<UpdateCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateCommandResult(true);
    }
}
