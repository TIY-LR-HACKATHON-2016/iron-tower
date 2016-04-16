using System;
using System.Linq;
using System.Web.Mvc;
using IronTower.Web.Models;

namespace IronTower.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IronTowerDBContext db = new IronTowerDBContext();

        [HttpPost]
        public void PlayerName(string name)
        {
            db.Games.First().Name = name ?? "Player";

            db.SaveChanges();
        }

        // GET: Games
        public ActionResult GameState()
        {
            var game = db.Games.FirstOrDefault();
            if (game == null)
            {
                game = new Game();
                db.Games.Add(game);
            }


            //initialize each game state
            game.Message = "";
            game.MessageType = 0;

            //add money?
            if ((DateTime.Now - game.LastPaid).Seconds >= 30)
            {
                game.Money += game.MoneyPerMin;
                game.LastPaid = DateTime.Now;
            }

            //add tennant?
            if ((DateTime.Now - game.LastTenant).Seconds > game.TennantInterval)
            {
                var floor =
                    game.Tower.Where(x => x.FloorType.IsApartment)
                        .FirstOrDefault(x => x.NumPeople < x.FloorType.PeopleLimit);
                if (floor != null)
                {
                    floor.People.Add(new Person {Game = game, Home = floor});
                    game.LastTenant = DateTime.Now;
                    game.Message = "New Tenant has arrived!";
                    game.MessageType = 1;
                }
            }
            db.SaveChanges();
            return Json(db.Games.ToList(), JsonRequestBehavior.AllowGet);
        }


        //GET: Games/PossibleFloors
        public ActionResult PossibleFloors()
        {
            var game = db.Games.First();
            var model = db.FloorTypes.ToList().Where(x => x.BuildCost < game.Money)
                .Select(x => new {x.Name, x.PeopleLimit, x.Category, x.BuildCost, x.Earning});

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            var game = db.Games.First();

            if (game.Money < game.NextFloorCost)
            {
                game.Message = "Not enough money";
                game.MessageType = 2;
                return Json(db.Games.ToList(), JsonRequestBehavior.AllowGet);
            }

            var f = new Floor {FloorType = db.FloorTypes.FirstOrDefault(x => x.Category == FloorCategory.Empty)};

            game.Tower.Add(f);

            game.Money -= game.NextFloorCost;
            game.NextFloorCost *= game.NextFloorCostIncrease;

            db.SaveChanges();
            return Json(db.Games.ToList(), JsonRequestBehavior.AllowGet);
        }

        //GET: Games/AddEmployee/{id}
        public ActionResult AddEmployee(int id)
        {
            var game = db.Games.First();

            var businessFloor = game.Tower.FirstOrDefault(x => x.Id == id);
            if (businessFloor == null)
            {
                return HttpNotFound("Can't find business");
            }


            var unemployedGuy = game.People.FirstOrDefault(x => x.Work == null);
            if (unemployedGuy == null)
            {
                return Content("Nobody available");
            }

            unemployedGuy.Work = businessFloor;

            db.SaveChanges();

            return Content("OK");
        }

        //GET: Games/ChangeFloor/{id}
        public ActionResult ChangeFloor(int floorid, int floorTypeId)
        {
            var game = db.Games.First();

            var floor = db.Floors.Find(floorid);
            var floorType = db.FloorTypes.Find(floorTypeId);

            if (floorType.BuildCost >= game.Money)
            {
                return Content("Not Enough Money");
            }


            //change floor 
            floor.FloorType = floorType;
            game.Money -= floorType.BuildCost;
            db.SaveChanges();

            return Json(db.Games.ToList(), JsonRequestBehavior.AllowGet);
        }


        // GET: Games/Delete
        public ActionResult Delete()
        {
            //Delete all stuff
            var game = db.Games.FirstOrDefault();
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();
            }

            return Content("Done!");
        }
    }
}