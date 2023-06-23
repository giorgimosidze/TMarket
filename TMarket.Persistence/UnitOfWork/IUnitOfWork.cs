using System.Threading.Tasks;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.Repositories.Abstract;

namespace TMarket.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<ProductDTO> ProductRepository { get; }
        IBaseRepository<OrderDTO> OrderRepository { get; }
        IBaseRepository<OrderProductDTO> OrderProductRepository { get; }
        IBaseRepository<UserDTO> UserRepository { get; }
        IBaseRepository<CartDTO> CartRepository { get; }
        IBaseRepository<CartProductDTO> CartProductRepository { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
