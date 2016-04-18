using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronTower.Business;

namespace IronTower.Console
{
    class Program
    {
        private static GameManager mgr = new GameManager();
        static void Main(string[] args)
        {


            Render();

            while (true)
            {
                var input = System.Console.ReadLine().ToLower();
                if (input == "a")
                {
                    mgr.CreateNewFloor();
                }

                if (input == "e")
                {
                    var floornumber = System.Console.ReadLine().ToLower();
                    mgr.AssignPersonToStore(int.Parse(floornumber));
                }

                if (input == "r")
                {
                    mgr.Restart();
                }

                if (input == "c")
                {
                    var floornumber = System.Console.ReadLine().ToLower();
                    var floortype = System.Console.ReadLine().ToLower();
                    
                    mgr.UpgradeFloor(int.Parse(floornumber), int.Parse(floortype));
                }


                if (input == "exit")
                {
                    break;
                }


                Render();
            }

        }

        static void Render()
        {
            mgr.UpdateGameState();
            System.Console.Clear();
            System.Console.WriteLine("IRON TOWER!");
            System.Console.WriteLine("Player: " + mgr.CurrentGame.Name);
            System.Console.WriteLine("Money: {0:C}", mgr.CurrentGame.Money);
            foreach (var floor in mgr.CurrentGame.Tower.OrderByDescending(x => x.Id))
            {
                var s = $"{floor.Id}:{floor.FloorType.Name}:{floor.NumPeople}";
                System.Console.WriteLine($"|----------------------------------|");
                System.Console.WriteLine("|     {0}{1," + (30 - s.Length) + "}", s, "|");
                System.Console.WriteLine($"|----------------------------------|");
            }
        }
    }
}
