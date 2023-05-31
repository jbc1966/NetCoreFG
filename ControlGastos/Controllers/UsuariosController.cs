using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlGastos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControlGastos.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public UsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }

            var usuario = new Usuario() { Email = modelo.Email };

            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

            if(resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario, isPersistent: true); // Quiere decir que aunque el usuario cierre el navegador estará autenticado
                return RedirectToAction("Index", "Transacciones");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
            
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.Email,
                            modelo.Password, modelo.Recuerdame, lockoutOnFailure: false); //lockoutOnFailure es para bloquear al usuario si se equivoca muchas veces con el pwd

            if(resultado.Succeeded)
            {
                return RedirectToAction("Index", "Transacciones");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o Password incorrecto");
                return View(modelo);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }










    }
}