using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDKDotNetCore.PizzaApi.Database;
using static SDKDotNetCore.PizzaApi.Database.OrderResponse;

namespace SDKDotNetCore.PizzaApi.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDBContext _appDBContext;

        public PizzaController()
        {
            _appDBContext = new AppDBContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var lst = await _appDBContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> Get()
        {
            var lst = await _appDBContext.PizzaExtras.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrder(string invoiceNo)
        {
            var item = await _appDBContext.PizzaOrders.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
            var lst = await _appDBContext.PizzaOrderDetails.FirstOrDefaultAsync(x => x.PizzaOrderInvoiceNo == invoiceNo);
            return Ok(new
            {
                order = item,
                details = lst
            });
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
        {
            var pizzaItem = await _appDBContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            var total = pizzaItem.Price;

            if( orderRequest.Extras.Length > 0)
            {
               var lstExtra = await _appDBContext.PizzaExtras.Where(x=> orderRequest.Extras.Contains(x.Id)).ToListAsync();   
                total += lstExtra.Sum(x => x.Price);
            }

            var invoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel()
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceNo = invoiceNo,
                TotalAmount = total
            };
            List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceNo = invoiceNo
            }).ToList();

            await _appDBContext.PizzaOrders.AddAsync(pizzaOrderModel);
            await _appDBContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
            await _appDBContext.SaveChangesAsync();

            OrderResponse response = new OrderResponse()
            {
                InvoiceNo = invoiceNo,
                Message = "Thank You for your order. Enjoy your Pizza!",
                TotalAmount = total
            };
            return Ok(response);
        }
    }
}
