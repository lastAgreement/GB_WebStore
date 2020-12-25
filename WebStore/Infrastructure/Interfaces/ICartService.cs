using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int Id);
        void DecrementFromCart(int Id);
        void RemoveFromCart(int Id);
        void Clear();
        CartViewModel TransformFromCart();
    }
}
