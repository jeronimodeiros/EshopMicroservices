// esta representa nuestra capa de presentacion


namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile,
        decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {   
            /*
             * en esta funcion vamos a definir nuestro HTTP Post endpoint utilizando
             * Carter y Mapster. Y luego vamos a asignar nuestra request a un objeto
             * de command y despues lo enviaremos a traves del mediatr. luego mapearemos
             * el resultado de vuelta al modelo de response.
             */
            app.MapPost("/products",
                    async (CreateProductRequest request, ISender sender) =>
                    {
                        // aca para crear un objeto de command desde la request uso Mapster para mapearlo
                        // y enviarlo al command handler que es lo que recibe (un productCommand).

                        var command = request.Adapt<CreateProductCommand>();
                        //el metodo send iniciara el mediatr y triggeriara nuestro handler y le enviamos el 
                        //comando de create product command object (el objeto para crear el producto)
                        var result = await sender.Send(command);
                        //despues de obtener el resultado, volvemos a convertir el tipo de respuesta
                        // del resultado usando Mapster
                        var response = result.Adapt<CreateProductResponse>();

                        return Results.Created($"/products/{response.Id}", response);
                    }) //aca voy a colocar toda la configuracion de Carter para el post
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create product")
                .WithDescription("Create Product");
        }
    }
}
