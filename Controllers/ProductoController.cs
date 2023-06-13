using Microsoft.AspNetCore.Mvc;
using RepositorioDTO.DTO;
using RepositorioDTO.Models;
using RepositorioDTO.Repository;

namespace RepositorioDTO.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoRepository _repo;
        public ProductoController(ProductoRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var productos = _repo.ObtenerProductos();
            var datos = productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio
            }).ToList();
            return View(datos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ProductoDTO dto)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    Nombre = dto.Nombre,
                    Precio = dto.Precio
                };

                _repo.AgregarProducto(producto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        public IActionResult Editar(int id)
        {
            var producto = _repo.ObtenerProductos().FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            var productodto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            };
            return View(productodto);
        }

        [HttpPost]
        public IActionResult Editar(ProductoDTO dto)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Precio = dto.Precio
                };

                _repo.ActualizarProducto(producto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        public IActionResult Eliminar(int id)
        {
            var producto = _repo.ObtenerProductos().FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            var productodto = new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            };
            return View(productodto);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            _repo.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}