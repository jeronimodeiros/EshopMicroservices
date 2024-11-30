//representa la capa logica de nuestra aplicacion.

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session)
    :ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business logic to create a product.
        /*
         * Tasks:
         * - Create Product entity from command object
         * - Save to database
         * - return CreateProductResult resul
         */

        var product = new Product
        {
            Name = command.Name,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // TODO
        // save to database
        session.Store(product);
        await session.SaveChangesAsync((cancellationToken));


        // return result

        return new CreateProductResult(product.Id);

    }
}

