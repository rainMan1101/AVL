using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.Graphics
{
    public class NodeTreeGraphicsAdapter : TreeGraphicsAdapter, INodeTreeGraphics
    {
        private System.Drawing.Graphics _graph;
        private Pen _pen;

        public NodeTreeGraphicsAdapter(System.Drawing.Graphics graph, Pen pen) : base(graph, pen)
        {
            _graph = graph;
            _pen = pen;
        }

        public void DrawRectangle(float x1, float y1, float x2, float y2)
        {
            _graph.DrawRectangle(_pen, x1, y1, x2, y2);
        }

        public void DrawEllipse(float x1, float y1, float x2, float y2)
        {
            _graph.DrawEllipse(_pen, x1, y1, x2, y2);
        }
    }
}
