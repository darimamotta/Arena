using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Arena
    {
        private int width;
        private int height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        List<List<Cell>> cells = null!;

        public Arena(int width, int height)
        {
            this.width = width;
            this.height = height;
            CreateCells();
        }

        private void CreateCells()
        {
            cells = new List<List<Cell>>();
            for (int i = 0; i < height; ++i)
            {
                cells.Add(new List<Cell>());
                for (int j = 0; j < width; ++j)
                    cells[i].Add(new Cell(i,j));
            }
        }

        public Cell GetCell(int line, int column)
        {
            return cells[line][column];
        }
        public Cell GetCell(Position pos)
        {
            return cells[pos.Line][pos.Column];
        }
        public List<Cell> GetNeibours(Cell cell)
        {
            List<Cell> neibours = new List<Cell>();

            int newline1 = cell.Line - 1;
            int newline2 = cell.Line + 1;
            int newcolumn3 = cell.Column - 1;
            int newcolumn4 = cell.Column + 1;
            if(newline1>=0) 
            {
                neibours.Add(GetCell(newline1, cell.Column));
            }
            if(newline2<height)
            {
                neibours.Add(GetCell(newline2, cell.Column));
            }
            if (newcolumn3 >= 0)
            {
                neibours.Add(GetCell(cell.Line, newcolumn3));
            }
            if(newcolumn4 <width)
            {
                neibours.Add(GetCell(cell.Line, newcolumn4));
            }
          
            return neibours;
        }
    }
}
