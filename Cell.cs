using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Cell
    {
        public Cell(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public Fighter Fighter { get; set; }
        public Obstacle Obstacle { get; set; }
        public bool IsPassable { get { return Obstacle == null && Fighter == null; } }
        public int Line { get; private set; }
        public int Column { get; private set; }

        public override string ToString()
        {
            return $"[{Line}, {Column}] {(IsPassable ? '+' : '-')}";
        }
        public override bool Equals(object obj)
        {
            Cell cell = obj as Cell;
            if(cell == null) return false;
            return Line == cell.Line && Column == cell.Column;
        }
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Line, Column);
        }
    }
}
