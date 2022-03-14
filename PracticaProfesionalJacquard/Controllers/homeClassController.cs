using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaProfesionalJacquard.Controllers
{
    public class homeClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
