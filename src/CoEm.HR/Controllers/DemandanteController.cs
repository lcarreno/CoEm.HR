using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoEm.HR.Models;
using CoEm.HR.Data;
using System.Security.Cryptography;
using System.Text;

namespace CoEm.HR.Controllers;

public class DemandanteController(DataContext dataContext)  : Controller
{
    private readonly DataContext _dataContext = dataContext;

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Registrar(DemandanteViewModel model)
    {
        if (ModelState.IsValid)
        {
            var usuario = new Usuario { Email = model.Email, Clave = SHA512.HashData(Encoding.UTF8.GetBytes(model.Clave)) };
            var perfil = new Perfil { Usuario = usuario, Nombre = model.Nombre, Telefono = model.Telefono, Direccion = model.Direccion, EsEmpleador = false };
            var demandante = new Demandante { Perfil = perfil, FechaNacimiento = model.FechaNacimiento, NivelEducacion = model.NivelEducacion, ExperienciaLaboral = model.ExperienciaLaboral};
            _dataContext.Usuarios.Add(usuario);
            _dataContext.Perfiles.Add(perfil);
            _dataContext.Demandantes.Add(demandante);

            _dataContext.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        return View(model);
    }
}