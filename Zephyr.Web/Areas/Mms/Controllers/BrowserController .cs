using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Zephyr.Core;
using Zephyr.Models;
using Zephyr.Web;

namespace Zephyr.Areas.Mms.Controllers
{

    public class BrowserController : Controller
    {
       
        public ActionResult Index()
        {
           
            return View();
        }

      
    }

    public class BrowserApiController : ApiController
    {
       
    }
}
