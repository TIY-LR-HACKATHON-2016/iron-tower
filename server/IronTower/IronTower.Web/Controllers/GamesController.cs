using System;
using System.Linq;
using System.Web.Mvc;
using IronTower.Business;
using IronTower.Data2;

namespace IronTower.Web.Controllers
{
    public class GamesController : Controller
    {
        private GameManager mgr = new GameManager();

        [HttpPost]
        public ActionResult PlayerName(string name)
        {
            mgr.SetPlayerName(name);
            return JsonGame();
        }

        // GET: Games
        public ActionResult GameState()
        {
            mgr.UpdateGameState();
            return JsonGame();


        }

        private ActionResult JsonGame()
        {
            var CurrentGame = mgr.CurrentGame;
            var model = new
            {
                CurrentGame.Id,
                CurrentGame.Name,
                CurrentGame.Money,
                MoneyPerMin = mgr.CalculateMPM(),
                CurrentGame.NextFloorCost,
                Unemployed = CurrentGame.People.Count(x => x.Work == null),
                Tower = CurrentGame.Tower.Select(x => new { FloorId = x.Id, FloorName = x.FloorType.Name, FloorTypeId = x.FloorType.Id, x.FloorType.PeopleLimit, x.NumPeople }).ToList()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }





        //GET: Games/PossibleFloors
        public ActionResult PossibleFloors()
        {
            var model = mgr.GetPossibleFloorTypes()
                   .Select(x => new { x.Id, x.Name, x.PeopleLimit, x.Category, x.BuildCost, x.Earning });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //GET: Games/AddFloor 
        public ActionResult AddFloor()
        {
            var result = mgr.CreateNewFloor();
            return JsonGame();
        }

        //GET: Games/AddEmployee/{id}
        public ActionResult AddEmployee(int id)
        {

            bool result = mgr.AssignPersonToStore(id);

            if (!result)
                return HttpNotFound();

            return JsonGame();
        }

        //GET: Games/ChangeFloor/{id}
        public ActionResult ChangeFloor(int floorid, int floorTypeId)
        {

            bool result = mgr.UpgradeFloor(floorid, floorTypeId);
            return JsonGame();
        }


        // GET: Games/Delete
        public ActionResult Delete()
        {
           mgr.Restart();
            return Content("Done!");
        }
    }
}