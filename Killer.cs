using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Killer:Fighter
    {
       public Killer(
       double minAttackDistance,
       double maxAttackDistance,
       int retreatDistance,
       int offensiveDistance,
       char label,
       int attack,
       int protection,
       int armor,
       int damage,
       int health,     
       string color
     ) : base(label,
            attack,
            protection,
            armor,
            damage,
            health,            
            color)
        {
            MinAttackDistance = minAttackDistance;
            MaxAttackDistance = maxAttackDistance;
            RetreatDistance = retreatDistance;
            OffensiveDistance = offensiveDistance;

        }
        public double MinAttackDistance { get; set; }
        public double MaxAttackDistance { get; private set; }
        public int RetreatDistance { get; private set; }
        public int OffensiveDistance { get; private set; }

        public override Fighter? ChooseTarget(Simulation simulation)
        {
            Fighter? target = null;            
            Arena arena = simulation.Arena;           
            Cell posCol = simulation.Arena.GetCell(Position);
            List<Fighter> targets =new List<Fighter>();
            List<Fighter> otherfighters = simulation.Fighters;

            foreach (Fighter fighter in otherfighters)
            {
                if ((fighter != null) && (fighter.Team != Team))
                { 
                     
                     Cell targetCell = simulation.Arena.GetCell(fighter.Position);
                     double dist = CalculateDistance(posCol, targetCell);
                     if ((MinAttackDistance <= dist) && (dist <= MaxAttackDistance))
                    {
                        targets.Add(fighter);
                    }
                     
                }                
                 
               
                
                  
                
            }  
            target = SelectBestTargetForKiller(targets);
            return target;
        }

        private double CalculateDistance(Cell posCol, Cell targetCell)
        {
            double calculatedDist = Math.Sqrt((targetCell.Line - posCol.Line) * (targetCell.Line - posCol.Line) + (targetCell.Column - posCol.Column) * (targetCell.Column - posCol.Column));
            return calculatedDist;
        }

        public Fighter? SelectBestTargetForKiller(List<Fighter> otherfighters)
        {
           
            Fighter? target = otherfighters.MinBy(g => g.Health);
           
            return target;
        }

        public override void Move(Simulation simulation)
        {
            Arena myArena = simulation.Arena;              

            Fighter? chooseTarget = this.ChooseTarget(simulation);
            if (chooseTarget != null)
                MoveToTarget(chooseTarget, simulation);

        }

        private void MoveToTarget(Fighter chooseTarget, Simulation simulation)
        {
            if (chooseTarget == null) return;
            Path pathToTarget = simulation.PathFinder.GetPath(this.Position, chooseTarget.Position, simulation.Arena);
            if (pathToTarget.Length > 2)
            {
                this.Position = new Position(pathToTarget.GetCell(1).Line, pathToTarget.GetCell(1).Column);
                simulation.Arena.GetCell(this.Position).Fighter = this;
                simulation.Arena.GetCell(pathToTarget.GetCell(0).Line, pathToTarget.GetCell(0).Column).Fighter = null;
            }
        }
    }
}
