using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region Properties
        private IMemoryCache cache;

        public IEnumerable<int> Numbers
        {
            get
            {
                return cache.Get<IEnumerable<int>>("numbers");
            }
            set
            {
                if(value != null)
                {
                    cache.Set("numbers", value);
                }
            }
        }

        private string Padding
        {
            get
            {
                return cache.Get<string>("padding");

            }
            set
            {
                if(value != null)
                {
                    cache.Set("padding", value);
                }
            }
        }
        #endregion

        #region Constructor
        public HomeController()
        {
            cache = new MemoryCache(new MemoryCacheOptions());
            EnsureNumberPopulated();
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPin()
        {
            var success = true;
            var pin = GetRandomPin();
            var message = default(string);

            if (string.IsNullOrEmpty(pin))
            {
                success = false;
                message = "All pins generated have been displayed.";
            }

            return Json(new
            {
                success,
                pin,
                message
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        public string GetRandomPin()
        {
            if (TryGetUniqueNumberFromEnumerable(out int number))
            {
                return number.ToString(Padding);
            }

            return default;
        }

        #region private helpers
        private void EnsureNumberPopulated()
        {
            if (Numbers == null)
            {
                var start = 0;
                var count = 10000;
                Numbers = Enumerable.Range(start, count);
                Padding = "D4";
            }
        }

        private bool TryGetUniqueNumberFromEnumerable(out int number)
        {
            var random = new Random();
            var numberList = Numbers.ToList();

            if (numberList.Any())
            {
                var index = random.Next(0, numberList.Count());
                number = numberList.ElementAt(index);
                numberList.RemoveAt(index);
                Numbers = numberList;
                return true;
            }

            number = 0;
            return false;
        }
        #endregion
    }
}