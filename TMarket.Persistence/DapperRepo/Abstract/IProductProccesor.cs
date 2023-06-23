using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.DapperRepo.Abstract
{
    public interface IProductProcessor
    {
        ProductDTO get(object id);

        void Create(ProductDTO productDTO);

        void Update(ProductDTO productDTO);
        void Delete(int ProductDTOId);
    }
}
