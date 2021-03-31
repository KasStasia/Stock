using System.Windows.Controls;

namespace Stock
{
    internal class GridViewCellInfo
    {
        private DataGridCellInfo currentCell;

        public GridViewCellInfo(DataGridCellInfo currentCell)
        {
            this.currentCell = currentCell;
        }
    }
}