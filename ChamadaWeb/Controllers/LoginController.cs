using ChamadaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChamadaWeb.Controllers
{
    public class LoginController : Controller
    {
        private dbChamadaEntities db = new dbChamadaEntities();
    
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string rg, string senha)
        {
            if (string.IsNullOrEmpty(rg))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoa.Where(i => i.RG == rg && i.Senha == senha).FirstOrDefault();
            if (pessoa == null)
            {
                ViewBag.LoginError = "Usuário e/ou senha inválidos.";
                return View("Index");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}