using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using CoEm.HR.Data;
using CoEm.HR.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CoEm.HR.Controllers;

public class AuthController(DataContext dataContext) : Controller
{
    private readonly DataContext _dataContext = dataContext;

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UsuarioViewModel model)
    {
        if (ModelState.IsValid)
        {
            var usuario = _dataContext.Usuarios
                        .Include(u => u.Perfil)
                        .FirstOrDefault(u => u.Email == model.Email && u.Clave == SHA512.HashData(Encoding.UTF8.GetBytes(model.Clave)));
            if (usuario != null)
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Email, usuario.Email),
                    new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new(ClaimTypes.Role, usuario.Perfil.EsEmpleador ? "Empleador" : "Demandante"),
                    new(ClaimTypes.Name, usuario.Perfil.Nombre)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), 
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
