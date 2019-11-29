using System.Drawing;
using AVL.Core.Graphics;
using AVL.Core.Trees.VisualTrees;


namespace AVL.Graphics
{
    public class GraphicsRepository : IGraphicsRepository
    {
        private Pen _pen;
        private SolidBrush _brush;
        private System.Drawing.Graphics _graphics;


        public GraphicsRepository(System.Drawing.Graphics graphics)
        {
            _pen = Pens.LimeGreen;
            _brush = new SolidBrush(Color.LimeGreen);
            _graphics = graphics;
        }
        
        public ILabeledTreeGraphics GetLabeledTreeGraphics()
        {
            return new LabeledTreeGraphicsAdapter(_graphics, _pen, _brush);
        }

        public INodeTreeGraphics GetNodeTreeGraphics()
        {
            return new NodeTreeGraphicsAdapter(_graphics, _pen);
        }

        public ITreeGraphics GetTreeGraphics()
        {
            return new TreeGraphicsAdapter(_graphics, _pen);
        }
    }
}
