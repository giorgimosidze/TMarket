using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.Repositories.Abstract;

namespace TMarket.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MarketDbContext _context;
        private IDbContextTransaction _transaction;
        private Lazy<IBaseRepository<ProductDTO>> _productRepository;
        private Lazy<IBaseRepository<OrderDTO>> _orderRepository;
        private Lazy<IBaseRepository<OrderProductDTO>> _orderProductRepository;
        private Lazy<IBaseRepository<UserDTO>> _userRepository;
        private Lazy<IBaseRepository<CartDTO>> _cartRepository;
        private Lazy<IBaseRepository<CartProductDTO>> _cartProductRepository;

        public UnitOfWork(MarketDbContext context, IBaseRepository<ProductDTO> productRepository,
            IBaseRepository<OrderDTO> orderRepository, IBaseRepository<OrderProductDTO> orderProductRepository,
            IBaseRepository<UserDTO> userRepositoy,
            IBaseRepository<CartDTO> cartRepository,
            IBaseRepository<CartProductDTO> cartProductRepository)
        {
            _context = context;
            _productRepository = new Lazy<IBaseRepository<ProductDTO>>(productRepository);
            _orderProductRepository = new Lazy<IBaseRepository<OrderProductDTO>>(orderProductRepository);
            _orderRepository = new Lazy<IBaseRepository<OrderDTO>>(orderRepository);
            _userRepository = new Lazy<IBaseRepository<UserDTO>>(userRepositoy);
            _cartRepository = new Lazy<IBaseRepository<CartDTO>>(cartRepository);
            _cartProductRepository = new Lazy<IBaseRepository<CartProductDTO>>(cartProductRepository);
        }

        public IBaseRepository<ProductDTO> ProductRepository => _productRepository?.Value;
        public IBaseRepository<OrderDTO> OrderRepository => _orderRepository?.Value;
        public IBaseRepository<OrderProductDTO> OrderProductRepository => _orderProductRepository?.Value;
        public IBaseRepository<UserDTO> UserRepository => _userRepository?.Value;
        public IBaseRepository<CartDTO> CartRepository => _cartRepository?.Value;
        public IBaseRepository<CartProductDTO> CartProductRepository => _cartProductRepository?.Value;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
