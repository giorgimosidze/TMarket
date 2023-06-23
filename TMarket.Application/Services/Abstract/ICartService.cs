using System.Collections.Generic;
using System.Threading.Tasks;
using TMarket.Application.ServiceModels;
using TMarket.Persistence.DbModels;

namespace TMarket.Application.Services.Abstract
{
    public interface ICartService : IService
    {
        IEnumerable<CartDTO> GetAllWithNoTracking();
        Task<bool> InsertOrderAsync(CartServiceModel cart);
        Task DeleteCart(int id);
    }
}
