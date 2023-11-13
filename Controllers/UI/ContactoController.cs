using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryPrimary2.Models;
using LibraryPrimary2.Data;

namespace LibraryPrimary2.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactoController(ILogger<ContactoController> logger,ApplicationDbContext context)
        {
            _logger = logger;
             _context= context;
        }

        public IActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult Create(Contacto objContacto)
        {   
            _context.Add(objContacto);
            _context.SaveChanges();
            ViewData["Message"] = string.Concat("Estimado " , objContacto.Nombre, " te estaremos contactando pronto.");
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}