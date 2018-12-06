using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JogosDeGuerraModel;

namespace JogosDeGuerraWebAPI.Controllers
{
    [Authorize]
    public class BatalhasMVCController : Controller
    {
        private ModelJogosDeGuerra db = new ModelJogosDeGuerra();

        public ActionResult Lobby()
        {
            ViewBag.Title = "Lobby";

            return View();
        }

        // GET: BatalhasMVC
        public ActionResult Index()
        {
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
                .ToList();

            return View(batalhas);
        }

        public ActionResult Iniciar(int id)
        {
            var batalhaController = new BatalhasController();
            var batalha = batalhaController.IniciarBatalha(id);
            return RedirectToAction("Tabuleiro", new { BatalhaId = batalha.Id });
        }

        public ActionResult Tabuleiro (int BatalhaId)
        {
            var batalha = db.Batalhas
                .Where(x => x.Id.Equals(BatalhaId))
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Include(b => b.Vencedor)
                .Include(b => b.Vencedor.Usuario)
                .FirstOrDefault();

            ViewBag.Id = batalha.Id;
            return View(batalha);
        }

        // GET: BatalhasMVC/Create
        public ActionResult Create(int id)
        {
            AbstractFactoryExercito.Nacao nacao = (AbstractFactoryExercito.Nacao)id;
            var batalhaController = new BatalhasController();
            var batalha = batalhaController.CriarNovaBatalha(nacao);
            //return RedirectToAction("Lobby");
            return RedirectToAction("Index");
        }

        //// POST: BatalhasMVC/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Batalhas.Add(batalha);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
        //    ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
        //    ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
        //    ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
        //    ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
        //    return View(batalha);
        //}

        // GET: BatalhasMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // POST: BatalhasMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TabuleiroId,ExercitoBrancoId,ExercitoPretoId,VencedorId,TurnoId,Estado")] Batalha batalha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batalha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExercitoBrancoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoBrancoId);
            ViewBag.ExercitoPretoId = new SelectList(db.Exercitos, "Id", "Id", batalha.ExercitoPretoId);
            ViewBag.TabuleiroId = new SelectList(db.Tabuleiroes, "Id", "Id", batalha.TabuleiroId);
            ViewBag.TurnoId = new SelectList(db.Exercitos, "Id", "Id", batalha.TurnoId);
            ViewBag.VencedorId = new SelectList(db.Exercitos, "Id", "Id", batalha.VencedorId);
            return View(batalha);
        }

        // GET: BatalhasMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batalha batalha = db.Batalhas.Find(id);
            if (batalha == null)
            {
                return HttpNotFound();
            }
            return View(batalha);
        }

        // POST: BatalhasMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batalha batalha = db.Batalhas.Find(id);
            db.Batalhas.Remove(batalha);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
