using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Simulation
    {
        public string message = null!;
        public Arena Arena { get; set; } = null!;
        public List<Fighter> Fighters { get; set; } = null!;
        public List<Team> Teams { get; set; } = null!;
        public IPathFinder PathFinder { get; set; } = null!;
        public ArenaVisualizer Visualizer { get; set; } = null!;
        public FileSimulationBuilder builder { get; set; } = null!;


        public void Run()
        {
            while (!WinnerIsDefined()) 
            { 
                ExecuteSimulationStep();
               
                Console.Clear();
                Visualizer.Draw(this);
             
                Console.ReadKey();
                Visualizer.Clear();
            }

        }

        private void ExecuteSimulationStep()
        {                     
            for (int i = 0; i < Fighters.Count; i++)
            { 
                Fighter fighter = Fighters[i];
                if (Fighters[i].IsAlive)
                {
                    fighter.Move(this);
                    Fighter? target = fighter.ChooseTarget(this);
                    if(target != null)
                    {
                        fighter.AttackOtherFighter(target);
                    }                                  

                }

            } 
        }
        private bool WinnerIsDefined()
        {
            for (int i = 0; i < Fighters.Count; i++)
            {
                if (!Fighters[i].IsAlive) continue;

                for (int j = i + 1; j < Fighters.Count; j++)
                {
                    if (Fighters[j].IsAlive && Fighters[i].Team != Fighters[j].Team)
                        return false;
                }
            }
            return true;            
        }
    }
}
