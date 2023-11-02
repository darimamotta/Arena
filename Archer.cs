using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Archer : Fighter
    {
        public Archer(
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
            string team,
            string color
          ) : base(label,
                 attack,
                 protection,
                 armor,
                 damage,
                 health,
                 team,
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
            double dist;
            Arena arena = simulation.Arena;
            List<Fighter> fighters = simulation.Fighters;
            Cell posCol = simulation.Arena.GetCell(Position);
            foreach (Fighter fighter in fighters)
            {
                if (fighter.Team != Team)
                {

                    Cell targetCell = simulation.Arena.GetCell(fighter.Position);
                    dist = CalculateDistance(posCol, targetCell);
                    if ((MinAttackDistance <= dist) && (dist <= MaxAttackDistance))
                        return fighter;
                }
            }
            return null;
        }
        public double CalculateDistance(Cell fighterPos, Cell targetPos)
        {
            double calculatedDist = Math.Sqrt((targetPos.Line - fighterPos.Line) * (targetPos.Line - fighterPos.Line) + (targetPos.Column - fighterPos.Column) * (targetPos.Column - fighterPos.Column));
            return calculatedDist;
        }


        public override void Move(Simulation simulation)
        {          
                   
            //= pathToTarget.Length ;
            Arena myArena = simulation.Arena;
            Fighter? target = ChooseTarget(simulation);
            //double dist = CalculateDistance(this.Position, target.Position);
            Path pathToTarget = simulation.PathFinder.GetPath(this.Position, target.Position, myArena);
            double dist = pathToTarget.Length;
            if (dist >= OffensiveDistance)
            {
                MoveToTarget(this, target, simulation);
            }
            if (dist <= RetreatDistance)
            {
                MoveBack(this, target, simulation);
            }


        }
    private void MoveBack(Fighter fighter, Fighter target, Simulation simulation)
    {
        
            if (target == null) return;
            Path pathToTarget = simulation.PathFinder.GetPath(fighter.Position, target.Position, simulation.Arena);
            if (pathToTarget.Length > 2)
            {
                fighter.Position = new Position(pathToTarget.GetCell(0).Line, pathToTarget.GetCell(0).Column);
                simulation.Arena.GetCell(fighter.Position).Fighter = fighter;
                simulation.Arena.GetCell(pathToTarget.GetCell(1).Line, pathToTarget.GetCell(1).Column).Fighter = null;
            }
    }

    private void MoveToTarget(Fighter fighter, Fighter target, Simulation simulation)
    {
        if (target == null) return;
        Path pathToTarget = simulation.PathFinder.GetPath(fighter.Position, target.Position, simulation.Arena);
        if (pathToTarget.Length > 2)
        {
            fighter.Position = new Position(pathToTarget.GetCell(1).Line, pathToTarget.GetCell(1).Column);
            simulation.Arena.GetCell(fighter.Position).Fighter = fighter;
            simulation.Arena.GetCell(pathToTarget.GetCell(0).Line, pathToTarget.GetCell(0).Column).Fighter = null;
        }

    }

    }



}


    


