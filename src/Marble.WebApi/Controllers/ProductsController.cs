using System;
using System.Threading.Tasks;
using Marble.Application.Services;
using Marble.Application.ViewModels;
using Marble.Core.Bus;
using Marble.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marble.WebApi.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
            : base(mediator, notifications)
        {
            _productService = productService;
        }

        // GET api/Products
        [HttpGet]
        public IActionResult Get()
        {
            return Response(_productService.GetPublishedProducts());
        }
        
        [HttpGet]
        [Route("filter")]
        public IActionResult Get([FromQuery] ProductRequestModel parameters)
        {
            return Response(_productService.GetFilteredProducts(parameters.Keyword,
                parameters.MinStockQuantity, parameters.MaxStockQuantity));
        }

        // GET api/Products/1
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _productService.GetByIdAsync(id);
            return Response(result);
        }

        // POST api/Products
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }
            
            _productService.Create(model);

            return Response(model);
        }

        // PUT api/Products/1
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditProduct(Guid id, [FromBody] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            _productService.Update(model);

            return Response(model);
        }

        // DELETE api/Products/1
        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveProduct(Guid id)
        {
            _productService.Remove(id);
            return Ok();
        }
    }
}