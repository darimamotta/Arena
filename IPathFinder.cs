
namespace ArenaProject
{
    public interface IPathFinder
    {
        public Path GetPath(Cell cellFrom, Cell cellTo, Arena arena);
        public Path GetPath(Position from, Position to, Arena arena) 
        {
            return GetPath(arena.GetCell(from), arena.GetCell(to), arena);
        }

    }
}
