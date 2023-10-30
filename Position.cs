using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaProject
{
    public class Position
    {
       
        public Position(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; private set; }
        public int Column { get; private set; }

        public Position Offset(int lineOffset, int columnOffset)
        {
            return new Position(Line + lineOffset, Column + columnOffset);

        }
        public override string ToString()
        {
            return $"[{Line}, {Column}]";
        }
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Line, Column);
        }
        public override bool Equals(object? obj)
        {
            Position? position = obj as Position;
            if (position == null) return false;
            return position.Line == Line && position.Column == Column;            
        }
    }
}
