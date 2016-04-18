using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronTower.Data2;

namespace IronTower.Business
{
    public class GameManager
    {
        private readonly IronTowerDBContext db = new IronTowerDBContext();


        private Game _game = null;

        public Game CurrentGame
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

        public void SetPlayerName(string name)
        {
            CurrentGame.Name = name ?? "Player";

            switch (CurrentGame.Name.ToLower())
            {
                case "nancy":
                case "nick":
                case "james":
                case "kate":
                case "brian":
                case "seth":
                case "trey":
                case "t":
                case "kevin":
                case "john":
                case "john b":
                case "john k":
                case "shaun":
                case "zach":
                case "daniel":
                case "jonathan":
                    CurrentGame.Money = 1000000;
                    break;
                case "mary":
                case "sinovia":
                    CurrentGame.Money = 500000;
                    break;
            }

            db.SaveChanges();
        }


        public void UpdateGameState()
        {
            //add money?
            var minutespassed = (DateTime.Now - CurrentGame.LastPaid).TotalMinutes;
            if (minutespassed >= 1)
            {
                CurrentGame.Money += (int)(CalculateMPM() * minutespassed);
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
        }

        public int CalculateMPM()
        {
            var businessFloors = CurrentGame.Tower.Where(x => x.FloorType.Category == FloorCategory.Business).Where(x => x.NumPeople > 0);

            var mpm =
                businessFloors.Sum(
                    floor => floor.FloorType.Earning + (floor.FloorType.EarningIncrease * (floor.NumPeople - 1)));
            return mpm;
        }
        public IEnumerable<FloorType> GetPossibleFloorTypes()
        {
            return db.FloorTypes.ToList().Where(x => x.BuildCost < CurrentGame.Money);
        }
        public void Restart()
        {
            //Delete all stuff
            var game = db.Games.FirstOrDefault();
            if (game != null)
            {
                db.Games.Remove(game);
                db.SaveChanges();
            }

        }

        public bool CreateNewFloor()
        {

            if (CurrentGame.Money < CurrentGame.NextFloorCost)
            {
                return false;
            }

            var f = new Floor { FloorType = db.FloorTypes.FirstOrDefault(x => x.Category == FloorCategory.Empty) };

            CurrentGame.Tower.Add(f);

            CurrentGame.Money -= CurrentGame.NextFloorCost;

            db.SaveChanges();

            return true;
        }


        public bool AssignPersonToStore(int floorId)
        {
            var businessFloor = CurrentGame.Tower.FirstOrDefault(x => x.Id == floorId && x.NumPeople < x.FloorType.PeopleLimit);
            if (businessFloor == null)
            {
                return false;
            }

            var unemployedGuy = CurrentGame.People.FirstOrDefault(x => x.Work == null);
            if (unemployedGuy == null)
            {
                return false;
            }

            unemployedGuy.Work = businessFloor;
            businessFloor.People.Add(unemployedGuy);


            db.SaveChanges();

            return true;
        }

        public bool UpgradeFloor(int floorid, int newFloorTypeId)
        {
            var floor = db.Floors.Find(floorid);
            var floorType = db.FloorTypes.Find(newFloorTypeId);

            if (floorType.BuildCost >= CurrentGame.Money)
            {
                return false;
            }


            //change floor 
            floor.FloorType = floorType;
            CurrentGame.Money -= floorType.BuildCost;
            db.SaveChanges();
            return true;
        }
    }
}
