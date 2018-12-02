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
            bool usuarioAutenticado = 
                Utils.Utils.ObterUsuarioLogado(
                    new JogosDeGuerraModel.ModelJogosDeGuerra()
                    ) != null;

            //if (!usuarioAutenticado)
            //{
            //    return RedirectToAction("Login");
            //}

            return View();
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
