using KassenSystem.Data;
using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using Tecan.Sila2;
using Tecan.Sila2.Client;
using Tecan.Sila2.Client.ExecutionManagement;
using Tecan.Sila2.Discovery;


namespace KassenSystem.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutItemController : ControllerBase
    {
        private readonly ILogger<CheckoutItemController> _logger;
        private const int ExpressMaxItem = 8;
        private bool scanning;
        private readonly ItemContext _context;

        public CheckoutItemController(ItemContext context, ILogger<CheckoutItemController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("init");
            scanning = false;



        }
       
        [HttpGet]
        public async Task<IEnumerable<CheckoutItem>> GetAllItems()
        {
            _logger.LogInformation("lalalla");
            return await _context.CheckoutItemModels0.ToListAsync();
        }
        [HttpGet("sum")]
        public async Task<Decimal> GetSum()
        {
            Decimal sum = 0;
            foreach(CheckoutItem i in await _context.CheckoutItemModels0.ToListAsync())
            {
                sum += i.PriceFull;
            }
            return sum;
        }
        [HttpGet("deletelast")]
        public async Task DeleteLast()
        {
            CheckoutItem lastItem = await _context.CheckoutItemModels0.FindAsync(_context.CheckoutItemModels0.Max(p => p.Id));
            if (lastItem != null)
            {

                if (lastItem.Amount > 1)
                {
                    lastItem.Amount -= 1;
                    lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
                    _context.CheckoutItemModels0.Update(lastItem);


                }
                else
                {
                    _context.CheckoutItemModels0.Remove(lastItem);
                }
                await _context.SaveChangesAsync();

            }
        }
        [HttpGet("plusone")]
        public async Task LastPlusOne()
        {
            if (_context.CheckoutItemModels0.Any())
            {

                CheckoutItem lastItem = await _context.CheckoutItemModels0.FindAsync(_context.CheckoutItemModels0.Max(p => p.Id));
                if (lastItem != null)
                {

                    lastItem.Amount += 1;
                    lastItem.PriceFull = lastItem.Amount * lastItem.PriceSingle;
                    _context.CheckoutItemModels0.Update(lastItem);
                    await _context.SaveChangesAsync();

                }
            }
        }
        [HttpGet("clear")]
        public async Task Clear()
        {
            if(_context.CheckoutItemModels0.Any<CheckoutItem>())
            {
                _context.CheckoutItemModels0.RemoveRange(_context.CheckoutItemModels0);
                await _context.SaveChangesAsync();
            }
            

            
        }
        [HttpGet("finish")]
        public async Task Finish()
        {
            //var jsonRequest = Json(new { ServerId = "1", ServerPort = "27015" }).Value.ToString();
            HttpClient client;
            foreach (var item in _context.CheckoutItemModels0)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7071/api/delete/"+item.ItemId+"/"+item.Amount);
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "relativeAddress");


                _logger.LogInformation(request.RequestUri.ToString());
                await client.SendAsync(request)
                      .ContinueWith(responseTask =>
                      {
                      //here your response 
                      _logger.LogInformation("Response: {0}", responseTask.Result);
                      });
            }
            
        }

        [HttpPost]
        public async Task PostItem([FromBody] Item item)
        {
            _logger.LogInformation("item: " + item.Name);

            var checkoutItemModel = _context.CheckoutItemModels0
            .Where(b => b.ItemId == item.Id)
            .FirstOrDefault();
            _logger.LogInformation("found");

            if (checkoutItemModel != null)
            {
                _logger.LogInformation("add +1");
                checkoutItemModel.Amount += 1;
                checkoutItemModel.PriceFull = checkoutItemModel.Amount * checkoutItemModel.PriceSingle;
                _context.CheckoutItemModels0.Update(checkoutItemModel);
                await _context.SaveChangesAsync();

            }
            else
            {
                checkoutItemModel = new CheckoutItem { Name = item.Name, PriceSingle = item.Price, PriceFull = item.Price, Amount = 1, ItemId = item.Id };
                _logger.LogInformation(checkoutItemModel.Name + " : " + checkoutItemModel.Id);

                _context.CheckoutItemModels0.Add(checkoutItemModel);
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost("{id}")]
        public async Task PostById(int id)
        {
            _logger.LogInformation("item: " + id);
            Item item = await _context.ItemModels.FindAsync(id);
            if (item != null)
            {
                var checkoutItemModel = _context.CheckoutItemModels0
            .Where(b => b.ItemId == item.Id)
            .FirstOrDefault();
                _logger.LogInformation("found");

                if (checkoutItemModel != null)
                {
                    _logger.LogInformation("add +1");
                    checkoutItemModel.Amount += 1;
                    checkoutItemModel.PriceFull = checkoutItemModel.Amount * checkoutItemModel.PriceSingle;
                    _context.CheckoutItemModels0.Update(checkoutItemModel);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    checkoutItemModel = new CheckoutItem { Name = item.Name, PriceSingle = item.Price, PriceFull = item.Price, Amount = 1, ItemId = item.Id };

                    _context.CheckoutItemModels0.Add(checkoutItemModel);
                    await _context.SaveChangesAsync();
                }

            }

        }




    }
}
