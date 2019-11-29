using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.UI.Graphics
{
    public class LabeledTreeGraphicsAdapter : NodeTreeGraphicsAdapter, ILabeledTreeGraphics
    {
        private System.Drawing.Graphics _graph;
        private SolidBrush _brush;

        public LabeledTreeGraphicsAdapter(System.Drawing.Graphics graph, Pen pen, SolidBrush brush) : base(graph, pen)
        {
            _graph = graph;
            _brush = brush;
        }

        public IFontParams GetFontParams(float height)
        {
            return new FontParams(height);
        }

        public void DrawString(IFontParams fontParams, float x, float y, string str)
        {
            _graph.DrawString(str, ((FontParams)fontParams).Font, _brush, x, y);
        }

    }
}
