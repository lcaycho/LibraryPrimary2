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
using OfficeOpenXml.Table;
using OfficeOpenXml;
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

        public IActionResult ExportarExcel() 
        {
            string excelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var pedidos = _context.DataPedido.AsNoTracking().ToList();
            using (var libro = new ExcelPackage())
            {
                var worksheet = libro.Workbook.Worksheets.Add("Pedidos");
                worksheet.Cells["A1"].LoadFromCollection(pedidos, PrintHeaders: true);
                for (var col = 1; col < pedidos.Count + 1; col++)
                {
                    worksheet.Column(col).AutoFit();
                }
                // Agregar formato de tabla
                var tabla = worksheet.Tables.Add(new ExcelAddressBase(fromRow: 1, fromCol: 1, toRow: pedidos.Count + 1, toColumn: 2), "Pedidos");
                tabla.ShowHeader = true;
                tabla.TableStyle = TableStyles.Light6;
                tabla.ShowTotal = true;

                return File(libro.GetAsByteArray(), excelContentType, "Pedidos.xlsx");
            }
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