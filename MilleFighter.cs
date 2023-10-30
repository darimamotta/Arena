using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class MilleFighter : Fighter
    {
        public MilleFighter(
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
        }

        public override Fighter? ChooseTarget(Simulation simulation)
        {
            Cell posCol = simulation.Arena.GetCell(Position);
            List<Cell> neighbors = simulation.Arena.GetNeibours(posCol);
            foreach (Cell neighbor in neighbors)
            {
                if((neighbor.Fighter!=null)&&(neighbor.Fighter.Team != Team))
                {
                    return neighbor.Fighter;
                }                            
            }          
                      
            return null;
        }
        public Fighter? SelectBestTarget(Fighter? past, Fighter current, Simulation simulation)
        {
            if ((current.Team == Team) || (!current.IsAlive))
                return past;
            if (past == null) return current;
            if ((current.Team != Team) && (current.IsAlive) && (past != null))
            {
                Path pastPath = simulation.PathFinder.GetPath(Position, past.Position, simulation.Arena);
                Path currentPath = simulation.PathFinder.GetPath(Position, current.Position, simulation.Arena);
                if (pastPath.Length > currentPath.Length) return current;
            }
            return past;
        }
        public void MoveToTarget(Fighter target, Simulation simulation)
        {
            if (target == null) return;
            Path pathToTarget = simulation.PathFinder.GetPath(this.Position, target.Position, simulation.Arena);
            if (pathToTarget.Length > 2)
            {
                this.Position = new Position(pathToTarget.GetCell(1).Line, pathToTarget.GetCell(1).Column);
                simulation.Arena.GetCell(this.Position).Fighter = this;
                simulation.Arena.GetCell(pathToTarget.GetCell(0).Line, pathToTarget.GetCell(0).Column).Fighter = null;
            }                
        }
        public override void Move(Simulation simulation)
        {
            //bool attackRes;
            Arena myArena = simulation.Arena;
            //renaVisualizer visualizer = simulation.Visualizer;
            //PathFinder? pathFinder = simulation?.PathFinder;
            
            
                Fighter? moveTarget = this.ChooseMoveTarget(simulation);
                if (moveTarget != null)
                    MoveToTarget(moveTarget, simulation);
                
            }

        private Fighter? ChooseMoveTarget(Simulation simulation)
        {
            Fighter? result = null;
            foreach (Fighter fighter in simulation.Fighters)
                result = SelectBestTarget(result, fighter, simulation);
            return result;

        }
    }

    }


