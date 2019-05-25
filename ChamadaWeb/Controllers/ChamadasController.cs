using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChamadaWeb.Models;
using ChamadaWeb.Models.ViewModels;

namespace ChamadaWeb.Controllers
{
    public class ChamadasController : Controller
    {
        private dbChamadaEntities db = new dbChamadaEntities();

        // GET: Chamadas
        public ActionResult Index()
        {
            return View(db.Turma.ToList());
        }

        // GET: Chamadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamada.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            return View(chamada);
        }

        // GET: Chamadas/Create
        public JsonResult Chamada(int idTurma)
        {
            ViewBag.IdProfessor = new SelectList(db.Pessoa, "Id", "Nome");
            var chamada = new Chamada();
            chamada.Data = DateTime.Now;
            chamada.IdTurma = idTurma;
            chamada.vwChamadaPessoa = this.GetListagemVwChamadaPessoa(0, idTurma);

            //this.Create(chamada);
            this.RedirectToAction("Create");

            return new JsonResult { Data = true, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Chamadas/Create/5
        public ActionResult Create(int idTurma)
        {
            ViewBag.IdProfessor = new SelectList(db.Pessoa, "Id", "Nome");
            var chamada = new Chamada();
            chamada.Data = DateTime.Now;
            chamada.IdTurma = idTurma;
            chamada.vwChamadaPessoa = this.GetListagemVwChamadaPessoa(0, idTurma);

            return View(chamada);
        }

        private List<vwChamadaPessoa> GetListagemVwChamadaPessoa(int idChamada, int idTurma)
        {
            var lstVwChamadaPessoa =
            (from
                turmaPessoa in db.TurmaPessoa
                join pessoa in db.Pessoa on turmaPessoa.IdPessoa equals pessoa.Id
                where
                    turmaPessoa.IdTurma == idTurma
                select new ChamadaWeb.Models.ViewModels.vwChamadaPessoa
                {
                    Pessoa = pessoa,
                    Pontuacao = 0,
                    IdPessoa = turmaPessoa.IdPessoa,
                    
             }).ToList();

            return lstVwChamadaPessoa;
        }

        //// POST: Chamadas/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,IdTurma,IdProfessor,NomeProfessorConvidado,Data,Licao,PontuacaoGeral,Ativa,DataAlteracao")] Chamada chamada)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Chamada.Add(chamada);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IdProfessor = new SelectList(db.Pessoa, "Id", "Nome", chamada.IdProfessor);
        //    ViewBag.IdTurma = new SelectList(db.Turma, "Id", "Nome", chamada.IdTurma);
        //    return View(chamada);
        //}

        // GET: Chamadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamada.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProfessor = new SelectList(db.Pessoa, "Id", "Nome", chamada.IdProfessor);
            ViewBag.IdTurma = new SelectList(db.Turma, "Id", "Nome", chamada.IdTurma);
            return View(chamada);
        }

        // POST: Chamadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdTurma,IdProfessor,NomeProfessorConvidado,Data,Licao,PontuacaoGeral,Ativa,DataAlteracao")] Chamada chamada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProfessor = new SelectList(db.Pessoa, "Id", "Nome", chamada.IdProfessor);
            ViewBag.IdTurma = new SelectList(db.Turma, "Id", "Nome", chamada.IdTurma);
            return View(chamada);
        }

        // GET: Chamadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamada chamada = db.Chamada.Find(id);
            if (chamada == null)
            {
                return HttpNotFound();
            }
            return View(chamada);
        }

        // POST: Chamadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chamada chamada = db.Chamada.Find(id);
            db.Chamada.Remove(chamada);
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
    }
}
