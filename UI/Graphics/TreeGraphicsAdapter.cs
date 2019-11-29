using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.UI.Graphics
{
    public class TreeGraphicsAdapter : ITreeGraphics
    {
        private System.Drawing.Graphics _graph;
        private Pen _pen;


        public TreeGraphicsAdapter(System.Drawing.Graphics graph, Pen pen)
        {
            _graph = graph;
            _pen = pen;
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            _graph.DrawLine(_pen, x1, y1, x2, y2);
        }
    }
}
