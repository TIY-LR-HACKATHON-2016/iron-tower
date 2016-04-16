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

        //Get: New Game
        public void New(string name)
        {
            if (name == null)
            {
                db.Games.Add(new Game("Player"));
            }
            else
            {
                db.Games.Add(new Game(name));
            }

            db.SaveChanges();
        }

        // GET: Games
        public ActionResult Index()
        {
            var game = db.Games.First();

            //initialize new game state
            game.Message = "";
            game.MessageType = 0;

            //add money?
            if ((DateTime.Now - game.LastPaid).Minutes >= 1)
            {
                game.Money += game.MoneyPerMin;
                game.LastPaid = DateTime.Now;
            }

            //add tennant?
            var floorid = canAddTennant(game);
            if (floorid > 0 && (DateTime.Now - game.LastTenant).Seconds > game.TennantInterval)
            {
                game.Tower.ToList()[floorid].People.ToList().Add(new Person());
                game.Tower.ToList()[floorid].NumPeople++;

                game.LastTenant = DateTime.Now;
                game.Message = "New Tenant has arrived!";
                game.MessageType = 1;
            }

            db.SaveChanges();
            return View(db.Games.ToList());
        }

        private int canAddTennant(Game game)
        {
            foreach (var f in game.Tower)
            {
                if (f.isApartment && f.PeopleLimit > f.NumPeople)
                {
                    return f.Id;
                }
            }
            return 0;
        }

        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            var game = db.Games.First();
            game.Tower.ToList().Add(new Floor(0));

            game.NextFloorCost *= game.NextFloorCostIncrease;

            db.SaveChanges();
            return Json(db.Games.ToList());
        }

        //GET: Games/AddEmployee/{id}
        public ActionResult AddEmployee(int id)
        {
            var game = db.Games.First();

            foreach (var f in game.Tower)
            {
                if (f.isApartment == true)
                {
                    foreach (var p in f.People)
                    {
                        if (p.Work == null)
                        {
                            p.Work = game.Tower.ToList()[id];
                            game.Tower.ToList()[id].NumPeople++;

                            //add to tower floor list?

                            //change floors earnings
                            if (f.NumPeople > 1)
                            {
                                db.Games.First().MoneyPerMin -= f.Earning;
                                f.Earning *= f.EarningIncrease;
                            }

                            db.Games.First().MoneyPerMin += f.Earning;
                            db.Games.First().Unemployed--;

                            db.SaveChanges();
                            return Json(db.Games.ToList());
                        }
                    }
                }
            }

            return Json(db.Games.ToList());
        }

        //GET: Games/ChangeFloor/{id}
        public ActionResult ChangeFloor(int id)
        {
            //change a floor logic
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