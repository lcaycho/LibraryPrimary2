using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryPrimary2.Data;
using LibraryPrimary2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LibraryPrimary2.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager <IdentityUser> _userManager;
        public PedidoController(ILogger<PedidoController> logger,
        ApplicationDbContext context,
        UserManager <IdentityUser> userManager)
        {
            _logger = logger;
            _context= context;
            _userManager=userManager;
        }

        public IActionResult Index()
        {
            var pedidos = from o in _context.DataPedido select o;
            pedidos = pedidos.Where(s => s.Status.Contains("PENDIENTE"));            
            return View(pedidos.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}