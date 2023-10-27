﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Simulation
    {
        public string message;
        public Arena Arena { get; set; }
        public List<Fighter> Fighters { get; set; }
        public List<Team> Teams { get; set; }
        public IPathFinder PathFinder { get; set; }
        public ArenaVisualizer Visualizer { get; set; }
        public FileSimulationBuilder builder { get; set; }


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

                    //Arena myArena = Arena; 
                    //Fighter? target = fighter.ChooseTarget(this);
                    //Path path = PathFinder.GetPath(fighter.Position, target.Position, myArena);
                    //if (path.Length > 2)
                    //{   fighter.Move(this);   
                    //    if (target != null)
                    //    {
                    //        fighter.AttackOtherFighter(target);
                    //    }  
                    //    if (target == null) return;
                    //
                    //if (path.Length == 2)
                    //{
                    //    int healthBefore = target.Health;
                    //    attackRes = fighter.AttackOtherFighter(target);
                    //    Visualizer.AddMessage($"Fighter {fighter.Label} is attacking target {target.Label}!");
                    //
                    //
                    //    if (attackRes)
                    //    {
                    //        int damageAt = healthBefore - target.Health;
                    //        Visualizer.AddMessage($"Fighter {fighter.Label} attacked target {target.Label} with damage {damageAt}!");
                    //
                    //    }
                    //    else { Visualizer.AddMessage($"Fighter {fighter.Label} failed in attack of target {target.Label}!"); }
                    //                   
                    //                    
                    //}    
                    //  
                    //}                
                    //

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
