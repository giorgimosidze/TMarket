using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TMarket.Persistence.DapperRepo.Abstract;
using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.DapperRepo.Concrete
{
    public class ProductProccesor : IProductProcessor

    {
        private readonly string connectionString;
        public ProductProccesor(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public ProductDTO get(object id)
        {
            IEnumerable<ProductDTO> ProductDtOs = null;
            using (var connection = new SqlConnection(connectionString))
            {
                ProductDtOs = connection.Query<ProductDTO>($"SELECT [Id], [Name], [Price], [IsAvailable], [UsefulnessTerm], [AvailableCount] FROM[MarketDb].[dbo].[Products] Where dbo.Products.Id = {id}");
            }
            return ProductDtOs.FirstOrDefault();
        }
        public void Create(ProductDTO productDTO)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                productDTO.InsertDate = productDTO.UpdateDate = DateTime.Now;
                productDTO.DeleteDate = new DateTime(2018,10,10);
                connection.Execute($"INSERT INTO [MarketDb].[dbo].[Products] ([Name], [Price], [CategoryId], [IsAvailable], [UsefulnessTerm], [AvailableCount], [IsDeleted], [InsertDate], [UpdateDate], [DeleteDate])" +
                      $" VALUES (@Name, @Price, @CategoryId, @IsAvailable, @UsefulnessTerm, @AvailableCount, @IsDeleted, @InsertDate, @UpdateDate, @DeleteDate)",
                new
                {
                    productDTO.Name,
                    productDTO.Price,
                    productDTO.CategoryId,
                    productDTO.IsAvailable,
                    productDTO.UsefulnessTerm,
                    productDTO.AvailableCount,
                    productDTO.IsDeleted,
                    productDTO.InsertDate,
                    productDTO.UpdateDate,
                    productDTO.DeleteDate
                });
            }
           
        }


        public void Update(ProductDTO productDTO)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                productDTO.UpdateDate = DateTime.Now;
                connection.Execute($"UPDATE  [MarketDb].[dbo].[Products] SET [Name]=@Name, [Price]=@Price, [CategoryId]=@CategoryId, [IsAvailable]=@IsAvailable," +
                    $" [UsefulnessTerm]=@UsefulnessTerm, [AvailableCount]=@AvailableCount, [IsDeleted]=@IsDeleted,[UpdateDate]=@UpdateDate WHERE [Id]=@Id",

                new
                {
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Price,
                    productDTO.CategoryId,
                    productDTO.IsAvailable,
                    productDTO.UsefulnessTerm,
                    productDTO.AvailableCount,
                    productDTO.IsDeleted,
                    productDTO.UpdateDate
                });
            }
        }
        public void Delete(int Id)
        {
            IEnumerable<ProductDTO> ProductDtOs = null;
            using (var connection = new SqlConnection(connectionString))
            {

                ProductDtOs = connection.Query<ProductDTO>($"UPDATE [MarketDb].[dbo].[Products] SET [IsDeleted]=@IsDeleted WHERE [Id]=@Id",

               new
               {
                   Id,
                   IsDeleted = true,
               });
            }
                
        }

       

       
    }
}
