using Elixir.DATA.DTOs.Product;
using Elixir.Extensions.StoreAuthoeization;
using Elixir.Services;
using Elixir.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elixir.Controllers
{

    public class ProductController : BaseController
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Respons<ProductDto>>> GetAll([FromQuery] ProductFilter filter) => OkPaginated(await _service.GetAll(filter), filter.PageNumber, filter.PageSize);

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductForm form) => Ok(await _service.Add(form));

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Respons<ProductDto>>> GetById(Guid id) => Ok(await _service.GetyById(id));

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await _service.Delete(id));

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ProductUpdate update ,Guid id) => Ok(await _service.Update(id,update));

    }
}
