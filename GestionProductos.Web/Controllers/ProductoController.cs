using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using GestionProductos.BLL;
using GestionProductos.Entidades;

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
    }
}