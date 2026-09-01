using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GestionProductos.BLL;
using GestionProductos.Entidades;
using System.Text.Json;

namespace GestionProductos.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoBLL _productoBLL;

        public ProductoController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ConexionSQL");
            _productoBLL = new ProductoBLL(connectionString);
        }

        // GET: /Producto/
        public IActionResult Index()
        {
            var productos = _productoBLL.ObtenerTodos();
            return View(productos);
        }

        // GET: /Producto/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Producto/Crear
        [HttpPost]
        public IActionResult Crear(Producto producto)
        {
            try
            {
                _productoBLL.Insertar(producto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(producto);
            }
        }

        // GET: /Producto/Editar/5
        public IActionResult Editar(int id)
        {
            var producto = _productoBLL.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: /Producto/Editar/5
        [HttpPost]
        public IActionResult Editar(Producto producto)
        {
            try
            {
                _productoBLL.Actualizar(producto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(producto);
            }
        }

        // GET: /Producto/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            _productoBLL.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        // =======================================================
        // ACCIONES HABILITADAS PARA PRUEBAS GET
        // =======================================================

        // GET: /Producto/AjustarStock?id=1&ajuste=5 (Stateful 1)
        [HttpGet]
        public IActionResult AjustarStock(int id, int ajuste)
        {
            try
            {
                _productoBLL.ModificarStockStateful(id, ajuste);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Producto/AgregarCarrito?id=1 (Stateful 2)
        [HttpGet]
        public IActionResult AgregarCarrito(int id)
        {
            var sessionData = HttpContext.Session.GetString("Carrito");
            var carrito = string.IsNullOrEmpty(sessionData)
                ? new List<Producto>()
                : JsonSerializer.Deserialize<List<Producto>>(sessionData) ?? new List<Producto>();

            carrito = _productoBLL.AgregarAlCarritoStateful(carrito, id);

            HttpContext.Session.SetString("Carrito", JsonSerializer.Serialize(carrito));
            return RedirectToAction(nameof(Index));
        }

        // GET: /Producto/CalcularPromocion?id=1 (Stateless 1 y 2)
        [HttpGet]
        public IActionResult CalcularPromocion(int id)
        {
            var producto = _productoBLL.ObtenerPorId(id);
            if (producto == null) return NotFound($"Producto con ID {id} no encontrado.");

            decimal precioOferta = _productoBLL.CalcularPrecioConDescuentoStateless(producto.Precio, 15);
            string estatusStock = _productoBLL.EvaluarNivelStockStateless(producto.Stock);

            return Content($"PRODUCTO: {producto.Nombre}\n" +
                           $"PRECIO ORIGINAL: S/ {producto.Precio}\n" +
                           $"PRECIO 15% DESC (Stateless 1): S/ {precioOferta}\n" +
                           $"STOCK ACTUAL: {producto.Stock}\n" +
                           $"EVALUACION STOCK (Stateless 2): {estatusStock}");
        }
    }
}
