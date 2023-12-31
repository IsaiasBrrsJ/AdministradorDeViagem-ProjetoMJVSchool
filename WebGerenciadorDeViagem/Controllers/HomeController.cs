﻿using GerenciadorDeViagem.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebGerenciadorDeViagem.Models;

namespace WebGerenciadorDeViagem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(UsuarioLogin usuarioLogin)
        {
            if (usuarioLogin.Matricula == 0)
                return RedirectToAction("Login", "Login");

            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}