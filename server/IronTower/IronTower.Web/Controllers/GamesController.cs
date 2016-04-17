using System;
using System.Linq;
using System.Web.Mvc;
using IronTower.Web.Models;

namespace IronTower.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IronTowerDBContext db = new IronTowerDBContext();

        private Game _game = null;
        protected Game CurrentGame
        {
            get
            {
                _game = db.Games.FirstOrDefault();
                if (_game == null)
                {
                    _game = new Game();
                    db.Games.Add(_game);
                    db.SaveChanges();
                }
                return _game;
            }
        }

        [HttpPost]
        public void PlayerName(string name)
        {
            CurrentGame.Name = name ?? "Player";

            db.SaveChanges();
        }

        // GET: Games
        public ActionResult GameState()
        {

            //add money?
            var minutespassed = (DateTime.Now - CurrentGame.LastPaid).TotalMinutes;
            if (minutespassed >= 1)
            {
                CurrentGame.Money += (int)(CalculateMPM()*minutespassed);
                CurrentGame.LastPaid = DateTime.Now;
            }

            //add tennant?
            if ((DateTime.Now - CurrentGame.LastTenant).Seconds > CurrentGame.TennantInterval)
            {
                var floor =
                    CurrentGame.Tower.Where(x => x.FloorType.IsApartment)
                        .FirstOrDefault(x => x.NumPeople < x.FloorType.PeopleLimit);
                if (floor != null)
                {
                    floor.People.Add(new Person { Game = CurrentGame, Home = floor });
                    CurrentGame.LastTenant = DateTime.Now;
                }
            }
            db.SaveChanges();
            return JsonGame();
        }

        private ActionResult JsonGame()
        {

            var model = new
            {
                CurrentGame.Id,
                CurrentGame.Name,
                CurrentGame.Money,
                MoneyPerMin = CalculateMPM(),
                CurrentGame.NextFloorCost,
                Unemployed = CurrentGame.People.Count(x => x.Work == null),
                Tower = CurrentGame.Tower.Select(x => new { FloorId = x.Id, FloorName = x.FloorType.Name, FloorTypeId = x.FloorType.Id, x.FloorType.PeopleLimit, x.NumPeople}).ToList()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private int CalculateMPM()
        {
            var businessFloors = CurrentGame.Tower.Where(x => x.FloorType.Category == FloorCategory.Business).Where(x=>x.NumPeople > 0);

            var mpm =
                businessFloors.Sum(
                    floor => floor.FloorType.Earning + (floor.FloorType.EarningIncrease*(floor.NumPeople - 1)));
            return mpm;
        }

      

        //GET: Games/PossibleFloors
        public ActionResult PossibleFloors()
        {
            var model = db.FloorTypes.ToList().Where(x => x.BuildCost < CurrentGame.Money)
                .Select(x => new { x.Id, x.Name, x.PeopleLimit, x.Category, x.BuildCost, x.Earning });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {

            if (CurrentGame.Money < CurrentGame.NextFloorCost)
            {
                return JsonGame();
            }

            var f = new Floor { FloorType = db.FloorTypes.FirstOrDefault(x => x.Category == FloorCategory.Empty) };

            CurrentGame.Tower.Add(f);

            CurrentGame.Money -= CurrentGame.NextFloorCost;

            db.SaveChanges();
            return JsonGame();
        }

        //GET: Games/AddEmployee/{id}
        public ActionResult AddEmployee(int id)
        {

            var businessFloor = CurrentGame.Tower.FirstOrDefault(x => x.Id == id && x.NumPeople < x.FloorType.PeopleLimit);
            if (businessFloor == null)
            {
                return HttpNotFound("Can't find business");
            }

            var unemployedGuy = CurrentGame.People.FirstOrDefault(x => x.Work == null);
            if (unemployedGuy == null)
            {
                return Content("Nobody available");
            }

            unemployedGuy.Work = businessFloor;
            businessFloor.People.Add(unemployedGuy);

           
            db.SaveChanges();

            return JsonGame();
        }

        //GET: Games/ChangeFloor/{id}
        public ActionResult ChangeFloor(int floorid, int floorTypeId)
        {

            var floor = db.Floors.Find(floorid);
            var floorType = db.FloorTypes.Find(floorTypeId);

            if (floorType.BuildCost >= CurrentGame.Money)
            {
                return Content("Not Enough Money");
            }


            //change floor 
            floor.FloorType = floorType;
            CurrentGame.Money -= floorType.BuildCost;
            db.SaveChanges();

            return JsonGame();
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