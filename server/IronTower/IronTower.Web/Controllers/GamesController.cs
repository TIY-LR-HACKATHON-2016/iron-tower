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
        private IronTowerWebContext db = new IronTowerWebContext();

        // GET: Games
        public ActionResult Game()
        {
            return Json(db.Games.ToList());
        }

        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            return Json(db.Games.ToList());
        }

        //GET: Games/AddPerson/{id}
        public ActionResult AddPerson(int id)
        {
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
