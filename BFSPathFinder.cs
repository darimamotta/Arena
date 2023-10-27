using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class BFSPathFinder : IPathFinder
    {
        Path IPathFinder.GetPath(Cell? cellFrom, Cell? cellTo, Arena arena)
        {
            int[,] lengthes = CalculateLengthes(cellFrom, cellTo, arena);
            return CalculatePath(cellFrom, cellTo, arena, lengthes);
        }

        private static Path CalculatePath(Cell cellFrom, Cell cellTo, Arena arena, int[,] lengthes)
        {
            Path path = new Path();
            Cell currentCell = cellTo;
            while (!currentCell.Equals(cellFrom))
            {
                path.AddCell(currentCell);
                List<Cell> nextCells = arena.GetNeibours(currentCell);
                foreach (Cell neubour in nextCells)
                {
                    if (lengthes[currentCell.Line, currentCell.Column] - 1 == lengthes[neubour.Line, neubour.Column])
                    {
                        currentCell = neubour;
                        break;

                    }
                }
            }
            path.AddCell(cellFrom);
            return path;
        }

        private static int[,] CalculateLengthes(Cell cellFrom, Cell cellTo, Arena arena)
        {
            bool pathFound = false;
            int[,] lengthes = new int[arena.Height, arena.Width];
            for(int i=0; i<arena.Height; i++)
            {
                for(int j=0;j<arena.Width; j++)
                {
                    lengthes[i,j]=-1;
                }
            }
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(cellFrom);
            lengthes[cellFrom.Line, cellFrom.Column] = 0;
            while (queue.Count > 0 && !pathFound)
            {
                pathFound = CalculateNextCell(cellTo, arena, pathFound, lengthes, queue);
            }

            return lengthes;
        }

        private static bool CalculateNextCell(Cell cellTo, Arena arena, bool pathFound, int[,] lengthes, Queue<Cell> queue)
        {
            Cell myCell = queue.Dequeue();
            List<Cell> neubours = arena.GetNeibours(myCell);
            foreach (Cell neubour in neubours)
            {
                if (neubour.IsPassable && lengthes[neubour.Line, neubour.Column] < 0)
                {
                    lengthes[neubour.Line, neubour.Column] = lengthes[myCell.Line, myCell.Column] + 1;
                    queue.Enqueue(neubour);
                } 
                if (neubour.Equals(cellTo))
                {
                    pathFound = true;
                    lengthes[neubour.Line, neubour.Column] = lengthes[myCell.Line, myCell.Column] + 1;
                }

            }           

            return pathFound;
        }

        Path IPathFinder.GetPath(Fighter fighter, Fighter? target, Arena myArena)
        {
            throw new NotImplementedException();
        }
    }
}
