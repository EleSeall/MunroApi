﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunroApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
