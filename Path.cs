namespace ArenaProject
{
    public class Path
    {
        private List<Cell> cells;
        public int Length { get { return cells.Count; } }


        public Path()
        {
            this.cells = new List<Cell>();
        }
        public Cell GetCell(int index)
        {
            return cells[index];
        }
        public void AddCell(Cell cell)
        {
            cells.Insert(0, cell);
        }
    }
}
