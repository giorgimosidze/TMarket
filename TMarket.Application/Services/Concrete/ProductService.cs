using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DapperRepo.Abstract;
using TMarket.Persistence.DbModels;

namespace TMarket.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductProcessor productProccesor;
        public ProductService (IProductProcessor productProccesor)
        {
            this.productProccesor = productProccesor;
        }
        public ProductDTO get(object id)
        {
            return productProccesor.get(id);
        }
        public void Create(ProductDTO productDTO)
        {
            productProccesor.Create(productDTO);
        }

        public void Delete(int Id)
        {
            productProccesor.Delete(Id);
        }
       
        public void Update(ProductDTO productDTO, int id)
        {
            productDTO.Id = id;
            productProccesor.Update(productDTO);
        }
    }
}
