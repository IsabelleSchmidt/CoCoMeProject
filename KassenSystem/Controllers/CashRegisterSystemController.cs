using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using KassenSystem.Data;
using KassenSystem.Models;

namespace KassenSystem.Controllers
{
    public class CashRegisterSystemController : Controller
    {
        private readonly ItemContext _context;
        private readonly ILogger<CashRegisterSystemController> _logger;

        public CashRegisterSystemController(ILogger<CashRegisterSystemController> logger, ItemContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.CheckoutItemModels.ToListAsync());
        }


        







        public IActionResult CashDisplay()
        {
            return View();
        }

        
        public void DeleteCurrentItem()
        {
            //letztes Element der gescannten Items löschen
        }

        public void CurrentItemPlusOne()
        {
            //letztes Element der gescannten Items Anzahl + 1
        }

        public void StopExpressCheckout()
        {
            //Express-Modus der Kasse beenden
            _logger.LogInformation("stop express");
        }

        public IActionResult ChoosePaymentMethod()
        {
            return View();
        }

        public IActionResult PayCash()
        {
            //open Cash Register
            return RedirectToAction("Index");
        }

        public IActionResult PayCard()
        {
            //activate Card Reader
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
