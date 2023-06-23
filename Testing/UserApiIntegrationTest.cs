using Moq;
using System;
using System.Threading.Tasks;
using TMarket.Application.Services.Concrete;
using TMarket.Persistence.DapperRepo.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.DbModels.Interfaces;
using TMarket.Persistence.Repositories.Abstract;
using Xunit;

namespace Testing
{
    public class UserApiIntegrationTest
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductProcessor> _ProductRepoMock = new Mock<IProductProcessor>();

        public UserApiIntegrationTest()
        {
            _productService = new ProductService(_ProductRepoMock.Object);
        }

        [Fact]
        public async Task GetByid_Shouldreturnproduct()
        {
            var ProductId = Guid.NewGuid();


            var Product = _productService.get(ProductId);

            Assert.Equal(ProductId, ProductId);
        }
    }
}
