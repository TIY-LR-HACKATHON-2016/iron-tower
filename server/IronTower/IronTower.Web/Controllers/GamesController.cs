using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IronTower.Web.Models;

namespace IronTower.Web.Controllers
{
    public class GamesController : Controller
    {
        private IronTowerDBContext db = new IronTowerDBContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }
        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            var game = db.Games.First();
            game.Tower.ToList().Add(new Floor(FloorType.Empty));
            db.SaveChanges();
            return Json(db.Games.ToList());
        }

        //GET: Games/AddPerson/{id}
        public ActionResult AddPerson(int id)
        {
            var tower = db.Games.First().Tower.ToList();
            foreach (var f in tower)
            {
                foreach(var p in f.People)
                {
                    if(p.Work == null)
                    {
                        p.Work = f;

                        db.SaveChanges();
                        return Json(db.Games.ToList());
                    }
                }
            }
                db.SaveChanges();
            return Json(db.Games.ToList());
        }

        //GET: Games/RemoveFloor/{id}
        public ActionResult RemoveFloor(int id)
        {
            return Json(db.Games.ToList());
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return Json(game);
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