using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JogosDeGuerraWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public JogosDeGuerraModel.ModelJogosDeGuerra ctx { get; set; } = new JogosDeGuerraModel.ModelJogosDeGuerra();

        public ActionResult Index()
        {
            ViewBag.Title = "Página Inicial";
            ViewBag.Usuarios = ctx.Usuarios.Count();

            var batalhas = ctx.Batalhas.ToList();

            return View(batalhas);
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult Cadastro()
        {
            ViewBag.Title = "Cadastro";
            return View();
        }


        public ActionResult Deslogar()
        {
            Utils.Utils u = new Utils.Utils(); 
            u.DeslogarUsuario(Request.GetOwinContext());
            //Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}
