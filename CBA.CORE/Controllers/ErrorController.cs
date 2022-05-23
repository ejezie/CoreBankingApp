using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            //var statusCodeResult =
            //   HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource could not be found";
                    //ViewBag.Path = statusCodeResult.OriginalPath;
                    //ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
            }


            return View("NotFound");
        }
        public IActionResult ErrorView(int statusCode)
        {


            ViewBag.ErrorMessage = "Sorry, the resource could not be found";
        


            return View("NotFound");
        }
    }
}
