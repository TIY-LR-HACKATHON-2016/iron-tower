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

        //Get: New
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
        public ActionResult GameState()
        {
            var game = db.Games.First();

            //initialize each game state
            game.Message = "";
            game.MessageType = 0;

            //add money?
            if ((DateTime.Now - game.LastPaid).Minutes >= 1)
            {
                game.Money += game.MoneyPerMin;
                game.LastPaid = DateTime.Now;
            }

            //add tennant?
            var floorid = CanAddTennant(game);
            if (floorid >= 0 && (DateTime.Now - game.LastTenant).Seconds > game.TennantInterval)
            {
                game.Tower.ToList()[floorid].People.ToList().Add(new Person());
                game.Tower.ToList()[floorid].NumPeople++;

                game.LastTenant = DateTime.Now;
                game.Message = "New Tenant has arrived!";
                game.MessageType = 1;
            }

            db.SaveChanges();
            return Json(db.Games.ToList());
        }

        private int CanAddTennant(Game game)
        {
            foreach (var f in game.Tower)
            {
                if (f.isApartment && f.PeopleLimit > f.NumPeople)
                {
                    return f.Id;
                }
            }
            return -1;
        }

        //GET: Games/PossibleFloors
        public ActionResult PossibleFloors()
        {
            var game = db.Games.First();
            var PossibleFloors = new List<Floor>();
            for(int i = 1; i < game.TotalFloorTypes; i++)
            {
                var floor = new Floor(i);
                if(game.Money < floor.BuildCost)
                {
                    break; 
                }

                PossibleFloors.Add(floor);
            }

            return Json(PossibleFloors);
        }
        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            var game = db.Games.First();

            if (game.Money < game.NextFloorCost)
            {
                game.Message = "Not enough money";
                game.MessageType = 2;
                return Json(db.Games.ToList());
            }

            game.Tower.ToList().Add(new Floor(0));

            game.Money -= game.NextFloorCost;
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

            game.Message = "No unemployed tenants!";
            game.MessageType = 2;
            return Json(db.Games.ToList());
        }

        //GET: Games/ChangeFloor/{id}
        public ActionResult ChangeFloor(int id)
        {
            var game = db.Games.First();
            var floor = new Floor(id);

            if(game.Money >= floor.BuildCost)
            {
                game.Tower.ToList().Add(floor);
                game.Money -= floor.BuildCost;
                
                //check new PeopleLimit
                game.PeopleLimit = PossibleTenatTotal(game);
            }
            else
            {
                game.Message = "Not enough money!";
                game.MessageType = 2;
            }

            db.SaveChanges();
            return Json(db.Games.ToList());
        }

        public int PossibleTenatTotal(Game game)
        {
            var total = 0;
            foreach(var f in game.Tower)
            {
                if(f.isApartment)
                {
                    total += f.PeopleLimit;
                }
            }

            return total;
        }

        // GET: Games/Delete/5
        public ActionResult Delete()
        {
            //Delete all stuff
            return Content("Done!");
        }
    }
}