using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormDrawTableWithMergeCells
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 用到索引的地方都是从 0 开始计数。

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            // 添加合并单元格
            List<MergeCell> mergeCells = new List<MergeCell>();
            mergeCells.Add(new MergeCell(1, 2, crossCols: 2));
            mergeCells.Add(new MergeCell(3, 3, crossRows: 3, crossCols: 2));

            DrawTable(g, 5, 5, 10, 6, mergeCells);
        }

        /// <summary>
        /// 画表格
        /// </summary>
        /// <param name="g"></param>
        /// <param name="x">表格位置 x</param>
        /// <param name="y">表格位置 y</param>
        /// <param name="rows">行数</param>
        /// <param name="cols">列数</param>
        /// <param name="mergeCells">合并的单元格列表</param>
        private void DrawTable(Graphics g, int x, int y, int rows, int cols, List<MergeCell> mergeCells = null)
        {
            const int cellW = 100; //单元格宽度
            const int cellH = 24; //单元格高度

            // 过滤有效的合并单元格
            var mcs = mergeCells?.Where(m => m.crossCols > 0 && m.crossRows > 0)
                ?.Where(m => !(m.crossCols == 1 && m.crossRows == 1))
                ?.ToList();

            List<Rectangle> mergeRects = new List<Rectangle>();

            int tmpX, tmpY;
            for (int r = 0; r < rows; r++)
            {
                tmpY = y + r * cellH;
                for (int c = 0; c < cols; c++)
                {
                    tmpX = x + c * cellW;

                    //如果当前的位置包含在合并单元格中，则跳过
                    if (mergeRects.Exists(rect => rect.Contains(tmpX + 1, tmpY + 1)))
                        continue;

                    int tmpW = cellW;
                    int tmpH = cellH;

                    // 用于判断当前单元格是否合并单元格的起始单元格
                    var mc = mcs?.FirstOrDefault(m => m.startRow == r && m.startCol == c); //合并单元格的起始单元格
                    if (mc != null)
                    {
                        if (mc.crossRows > 1)
                        {
                            tmpH = mc.crossRows * cellH;
                        }
                        if (mc.crossCols > 1)
                        {
                            tmpW = mc.crossCols * cellW;
                        }
                    }

                    var rec = new Rectangle(tmpX, tmpY, tmpW, tmpH);
                    mergeRects.Add(rec);

                    DrawCell(g, rec);
                }
            }
        }

        private void DrawCell(Graphics g, Rectangle rect)
        {
            g.DrawRectangle(Pens.Blue, rect);
        }
    }
}
