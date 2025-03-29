namespace Basket.API.Data;
/*
 * estamos implementando 2 patrones, el Proxy y Decorator.
 *
 * Proxy: el CachedBasketRepository actua como proxy y reenvía las llamadas
 * al repository de Basket (IBasketRepository) injectado. Ej:
 * return await repository.GetBasket(userName, cancellationToken);
 *
 * Decorator: ampliaremos también la funcionalidad de BasketRepository
 * añadiendo logica de almacenamiento en caché, para lo cual utilizaremos Redis.
 *
 */
public class CachedBasketRepository(IBasketRepository repository) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await repository.GetBasket(userName, cancellationToken);
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        return await repository.StoreBasket(basket, cancellationToken);
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        return await repository.DeleteBasket(userName, cancellationToken);
    }
}
