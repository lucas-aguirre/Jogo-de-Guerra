using JogosDeGuerraModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace JogosDeGuerraWebAPI.Controllers
{
    [Authorize]
    public class AccountMVCController : Controller
    {
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        // GET: AccountMVC
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Historico()
        {
            var usuarioLogado = Utils.Utils.ObterUsuarioLogado(db);

            var batalhas = db.Batalhas
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Include(b => b.Vencedor)
                .Include(b => b.Vencedor.Usuario)
                .Where(b => b.ExercitoBranco.Usuario.Email == usuarioLogado.Email || b.ExercitoPreto.Usuario.Email == usuarioLogado.Email)
                .ToList();

            return View(batalhas);
        }
    }
}
