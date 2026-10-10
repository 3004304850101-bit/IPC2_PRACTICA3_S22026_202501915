using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Mascotas.Models;
using TDA;
namespace Mascotas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ListaMascota _lista;
         public HomeController(ListaMascota lista)   // ASP.NET te da la misma instancia siempre
        {
            _lista = lista;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Consulta(string? idBuscar)
        {
            if (!string.IsNullOrWhiteSpace(idBuscar))
            {
                Mascota? m = _lista.BuscarMascota(idBuscar);
                if (m == null)
                    TempData["Error"] = $"No existe una mascota con ID {idBuscar}.";
                else
                    ViewBag.Seleccionada = m;
            }
            return View(_lista);
        }
        
        [HttpPost]
        public IActionResult Registrar(MascotaFormViewModel mascota)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Datos inválidos.";
                return RedirectToAction(nameof(Index));
            }

            Mascota nueva = mascota.Tipo switch
            {
                "Ave"     => new Ave(mascota.Nombre, mascota.Edad, mascota.Peso, mascota.Sexo, mascota.Propietario, mascota.Salud, mascota.Especie, mascota.Color, mascota.Envergadura, mascota.PuedeVolar),
                "Gato"    => new Gato(mascota.Nombre, mascota.Edad, mascota.Peso, mascota.Sexo, mascota.Propietario, mascota.Salud,mascota.Raza,mascota.Color,mascota.Esterilizado),
                "Perro"   => new Perro(mascota.Nombre, mascota.Edad, mascota.Peso, mascota.Sexo, mascota.Propietario, mascota.Salud,mascota.Raza,mascota.Tamano,mascota.Color),
                "Tortuga" => new Tortuga(mascota.Nombre, mascota.Edad, mascota.Peso, mascota.Sexo, mascota.Propietario, mascota.Salud,mascota.Especie,mascota.Color,mascota.Acuatico),
                _ => throw new ArgumentException("Tipo inválido")
            };

            _lista.AgregarMascota(nueva);
            TempData["Mensaje"] = $"Mascota registrada con ID {nueva.Id}.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Actualizar(MascotaFormViewModel mascota)
        {
            Mascota? m = _lista.BuscarMascota(mascota.Id);

            if (m == null)
            {
                TempData["Error"] = "La mascota ya no existe.";
                return RedirectToAction(nameof(Index));
            }

            m.ActualizarNombre(mascota.Nombre);
            m.ActualizarPeso(mascota.Peso);
            m.ActualizarEdad(mascota.Edad);
            m.CambiarEstado(mascota.Salud);

            switch (m)
            {
                case Ave a:  
                       a.CambiarVolar(mascota.PuedeVolar);
                break;
                case Gato g: 
                    g.CambiarEsterilizado(mascota.Esterilizado);
                break;
            }


            TempData["Mensaje"] = "Mascota actualizada.";
            return RedirectToAction(nameof(Index), new { idBuscar = m.Id });
        }

        [HttpGet]
        public IActionResult BuscarID(string? idBuscar)
        {
            if (string.IsNullOrWhiteSpace(idBuscar))
            {
                TempData["Error"] = "Ingrese un ID para buscar.";
                return View("Index");
            }

            Mascota? mascota = _lista.BuscarMascota(idBuscar);
            if (mascota == null)
            {
                TempData["Error"] = $"No existe una mascota con ID {idBuscar}.";
                return View("Index");
            }
            

            return View("Index", Convertir(mascota));
        }

        [HttpPost]
        public IActionResult CalcularDosis(string id, double dosis_kg)
        {
            Mascota? m = _lista.BuscarMascota(id);

            if (m == null)
            {
                TempData["Error"] = "La mascota ya no existe";
                return RedirectToAction(nameof(Consulta));
            }

            if (dosis_kg <=0)
            {
                TempData["Error"] = "Ingrese dosis para calcular";
                return View("Consulta");
            }

            TempData["Dosis"] = m.CalcularDosis(dosis_kg).ToString("F2");
            return RedirectToAction(nameof(Consulta), new {idBuscar = id});

        }

        [HttpPost]
        public IActionResult Eliminar(string id)
        {
            Mascota? m = _lista.BuscarMascota(id);

            if (m == null)
            {
                TempData["Error"] = "La mascota ya no existe";
                return RedirectToAction(nameof(Consulta));
            }

            _lista.EliminarMascota(m.Id);
            TempData["Mensaje"] = "Mascota eliminada";
            return RedirectToAction(nameof(Consulta));
        }

        private MascotaFormViewModel Convertir(Mascota m)
        {
            var vm = new MascotaFormViewModel
            {
                Id= m.Id,
                Nombre = m.Nombre, 
                Peso = m.Peso, 
                Sexo = m.Sexo,
                Edad = m.Edad, 
                Propietario = m.Propietario, 
                Salud = m.Estable
            };

            switch (m)
            {
                case Ave a:
                vm.Tipo= "Ave"; vm.Especie = a.Especie; vm.Color = a.Color; vm.Envergadura = a.Envergadura; vm.PuedeVolar = a.Volar;
                break;
                case Perro p:
                vm.Tipo= "Perro"; vm.Raza = p.Raza; vm.Tamano = p.Tamaño; vm.Color = p.Color;
                break;
                case Gato g:
                vm.Tipo= "Gato"; vm.Raza=g.Raza; vm.Color=g.Color; vm.Esterilizado= g.Estirilizado;
                break;
                case Tortuga t:
                vm.Tipo = "Tortuga"; vm.Especie= t.Especie; vm.Color=t.Color; vm.Acuatico= t.Acuatico;
                break;
            }

            return vm;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
