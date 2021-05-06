using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormDrawTableWithMergeCells
{
    /// <summary>
    /// 合并单元格定义
    /// </summary>
    public record MergeCell(int startRow, int startCol, int crossRows = 1, int crossCols = 1);

    //public class MergeCell
    //{
    //    public MergeCell(int startRow, int startCol, int crossRows = 1, int crossCols = 1)
    //    {
    //        this.startRow = startRow;
    //        this.startCol = startCol;
    //        this.crossRows = crossRows;
    //        this.crossCols = crossCols;
    //    }

    //    public int startRow { get; set; }
    //    public int startCol { get; set; }
    //    public int crossRows { get; set; }
    //    public int crossCols { get; set; }
    //}
}
