using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Archer:Fighter
    {
        public Archer(double minAttackDistance, double maxAttackDistance, int retreatDistance, int offensiveDistance)
        {
            MinAttackDistance = minAttackDistance;
            MaxAttackDistance = maxAttackDistance;
            RetreatDistance = retreatDistance;
            OffensiveDistance = offensiveDistance;

        }

        public double MinAttackDistance { get; set; }
        public double MaxAttackDistance { get; private set;}
        public int RetreatDistance { get; private set; }
        public int OffensiveDistance { get; private set; }

        public override Fighter? ChooseTarget(Simulation simulation)
        {
            double dist;
            Arena arena = simulation.Arena;            
            List<Fighter> fighters = simulation.Fighters;
            Cell posCol = simulation.Arena.GetCell(Position);
            foreach (Fighter fighter in fighters)
            {
                if  (fighter.Team != Team)
                {
                   
                    Cell targetCell = simulation.Arena.GetCell(fighter.Position); 
                    dist = CalculateDistance(posCol, targetCell);                   
                    if ((MinAttackDistance <= dist)&&(dist <= MaxAttackDistance))
                        return fighter;
                           
                }

            }

            return null;        
                    
        }
        public double CalculateDistance(Cell fighterPos, Cell targetPos)
        {        
           double calculatedDist= Math.Sqrt((targetPos.Line - fighterPos.Line)*(targetPos.Line - fighterPos.Line) +(targetPos.Column - fighterPos.Column)*(targetPos.Column - fighterPos.Column));
           return calculatedDist;
        }


        public override void Move(Simulation simulation)
        {
            double dist;
            Arena myArena = simulation.Arena;
            Fighter? target = ChooseTarget(simulation);
            //double dist = CalculateDistance(this.Position, target.Position);
            if (dist >= OffensiveDistance)
            {
                MoveToTarget();
            }
            if (dist <= RetreatDistance)
            {
                MoveBack();
            }
           

        }

        private void MoveBack()
        {
            throw new NotImplementedException();
        }

        private void MoveToTarget()
        {
            throw new NotImplementedException();
        }
    }
}

