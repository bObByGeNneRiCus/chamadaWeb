using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChamadaWeb.Models;

namespace ChamadaWeb.Controllers
{
    public class TurmasController : Controller
    {
        private dbChamadaEntities db = new dbChamadaEntities();

        // GET: Turmas
        public ActionResult Index()
        {
            return View(db.Turma.ToList());
        }

        // GET: Turmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // GET: Turmas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataInicio,DataFim")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                turma.DataAlteracao = DateTime.Now.Date;
                db.Turma.Add(turma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turma);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }

            turma.vwTurmaPessoa = this.GetListagemVwTurmaPessoa(turma.Id).Data as List<ChamadaWeb.Models.ViewModels.vwTurmaPessoa>;
            return View(turma);
        }

        // POST: Turmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataInicio,DataFim")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                turma.DataAlteracao = DateTime.Now.Date;
                db.Entry(turma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = db.Turma.Find(id);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turma turma = db.Turma.Find(id);
            db.Turma.Remove(turma);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListagemVwTurmaPessoa(int idTurma)
        {
            var lstvwTurmaPessoa =
                from
                    turmaPessoa in db.TurmaPessoa
                join turma in db.Turma on turmaPessoa.IdTurma equals turma.Id
                join pessoa in db.Pessoa on turmaPessoa.IdPessoa equals pessoa.Id
                where
                    turmaPessoa.Id == idTurma
                select new ChamadaWeb.Models.ViewModels.vwTurmaPessoa
                {
                    Id = turmaPessoa.Id,
                    IdTurma = turmaPessoa.IdTurma,
                    IdPessoa = turmaPessoa.IdPessoa,
                    NomePessoa = pessoa.Nome,
                    RG = pessoa.RG
                };

            return new JsonResult { Data = lstvwTurmaPessoa, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult GetListagemPessoas()
        {
            List<Pessoa> lstPessoa = db.Pessoa.ToList();
            return new JsonResult { Data = lstPessoa, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult IncluirTurmaPessoa(int idTurma, int idPessoa)
        {
            bool ret = false;
            TurmaPessoa entity = new TurmaPessoa();
            entity.IdTurma = idTurma;
            entity.IdPessoa = idPessoa;
            entity.DataAlteracao = DateTime.Now;

            db.TurmaPessoa.Add(entity);
            db.SaveChanges();

            return new JsonResult { Data = ret, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
