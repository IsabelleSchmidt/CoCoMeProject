using KassenSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KassenSystem.Controllers
{
    public class CashRegisterSystemController : Controller
    {
        private readonly ILogger<CashRegisterSystemController> _logger;
        private CustomerDisplayController customerDisplay;

        public CashRegisterSystemController(ILogger<CashRegisterSystemController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("index");
            return View();
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

        public IActionResult ChoosepaymentMethod()
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
