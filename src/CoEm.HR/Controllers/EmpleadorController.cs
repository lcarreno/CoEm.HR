using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoEm.HR.Models;
using CoEm.HR.Data;
using System.Text;
using System.Security.Cryptography;

namespace CoEm.HR.Controllers;

public class EmpleadorController(DataContext dataContext) : Controller
{
    private readonly DataContext _dataContext = dataContext;

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Registrar(EmpleadorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var usuario = new Usuario { Email = model.Email, Clave = SHA512.HashData(Encoding.UTF8.GetBytes(model.Clave)) };
            var perfil = new Perfil { Usuario = usuario, Nombre = model.Nombre, Telefono = model.Telefono, Direccion = model.Direccion, EsEmpleador = true };
            var empleador = new Empleador { Perfil = perfil, Industria = model.Industria, NumeroEmpleados = model.NumeroEmpleados, Ubicacion = model.Ubicacion };
            _dataContext.Usuarios.Add(usuario);
            _dataContext.Perfiles.Add(perfil);
            _dataContext.Empleadores.Add(empleador);

            _dataContext.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        return View(model);
    }
}