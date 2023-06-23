using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TMarket.Application.Services.Abstract;
using TMarket.Persistence.DbModels;
using TMarket.WEB.Helpers.Constants;
using TMarket.WEB.RequestModels.Products;


namespace TMarket.WEB.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseService<ProductDTO> _productService;
        private readonly IMapper _mapper;
        private readonly IProductService _productServiceDapper;

        public ProductsController(IBaseService<ProductDTO> productService, IMapper mapper,
                                  IProductService productConstructor)
        {
            _productService = productService;
            _mapper = mapper;
            _productServiceDapper = productConstructor;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductRespond>> GetProducts(
            [FromQuery] string name, [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {

            //Expression<Func<ProductDTO, bool>> predicate = p => p.Name.ToUpper().StartsWithOrNull(name.ToUpper()) &&
            //    p.Price >= minPrice && p.Price.LessOrEmptyInput(maxPrice);
            Expression<Func<ProductDTO, bool>> predicate = Predicate(minPrice, maxPrice, name);

            var products = _productService.FindAllAsyncWithNoTracking(predicate, source => source.Include(x => x.Category));

            return _mapper.Map<List<ProductRespond>>(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductRespond> GetProduct(int id)
        {
            //var product = await _productService.GetByIdAsync(id);
            var product = _productServiceDapper.get(id);

            if (product == null)
            {
                return NotFound(string.Format(ModelConstants.PropertyNotFoundFromController, "პროდუქტი"));
            }

            return _mapper.Map<ProductRespond>(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductRequest product)
        {
            var products = await _productService.GetAllAsyncWithNoTracking();
            if (!products.Any(x => x.Id == id))
            {
                return BadRequest(string.Format(ModelConstants.PropertyNotFoundFromController, "პროდუქტი"));
            }

            _productServiceDapper.Update(_mapper.Map<ProductDTO>(product),id);

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<ProductRespond> PostProduct(ProductRequest product)
        {
            //await _productService.InsertAsync(_mapper.Map<ProductDTO>(product));
            _productServiceDapper.Create(_mapper.Map<ProductDTO>(product));
            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductRespond>> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest(string.Format(ModelConstants.PropertyNotFoundFromController, "პროდუქტი"));
            }

            _productServiceDapper.Delete(id);
            return _mapper.Map<ProductRespond>(product);
        }

        [HttpGet("GetPaginatedResult")]
        public async Task<ActionResult<IEnumerable<ProductRespond>>> GetPaginatedResult
            (int currentPage = 1, int pageSize = 5, string sortBy = "Id", bool isAsc = true)
        {
            if (currentPage < 1 || pageSize < 1 || typeof(ProductRespond).GetProperty(sortBy) == null)
            {
                return BadRequest(ModelConstants.InvalidQuery);
            }

            IEnumerable<ProductDTO> products = await _productService
                .GetPaginatedResultAsyncAsNoTracking(currentPage, pageSize, sortBy, isAsc);
            return _mapper.Map<List<ProductRespond>>(products);
        }

        private Expression<Func<ProductDTO, bool>> Predicate(decimal? minPrice, decimal? maxPrice, string name)
        {
            ParameterExpression pe = Expression.Parameter(typeof(ProductDTO), "predicate");

            Expression left = Expression.Property(pe, "Price");
            Expression right = Expression.Constant(minPrice ?? 0, typeof(decimal));
            Expression e1 = Expression.GreaterThanOrEqual(left, right);

            right = Expression.Constant(maxPrice ?? decimal.MaxValue, typeof(decimal));
            Expression e2 = Expression.LessThanOrEqual(left, right);

            left = Expression.Property(pe, "Name");
            right = Expression.Constant(name ?? "", typeof(string));
            MethodInfo mi = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            Expression e3 = Expression.Call(left, mi, right);

            Expression predicateBody = Expression.AndAlso(Expression.AndAlso(e1, e2), e3);

            var expressionTree = Expression.Lambda<Func<ProductDTO, bool>>(predicateBody, new[] { pe });

            return expressionTree;
        }

    }
}
