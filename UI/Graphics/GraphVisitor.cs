using System.Drawing;
using AVL.Core.Graphics;
using AVL.Core.Trees.VisualTrees;


namespace AVL.UI.Graphics
{
    public class GraphVisitor<T> : IGraphVisitor<T>
    {
        private Pen _pen;
        private SolidBrush _brush;
        private System.Drawing.Graphics _graphics;


        public GraphVisitor(System.Drawing.Graphics graphics)
        {
            _pen = Pens.LimeGreen;
            _brush = new SolidBrush(Color.LimeGreen);
            _graphics = graphics;
        }
        
        public ILabeledTreeGraphics Visit(LabeledTree<T> tree)
        {
            return new LabeledTreeGraphicsAdapter(_graphics, _pen, _brush);
        }

        public INodeTreeGraphics Visit(NodeTree<T> tree)
        {
            return new NodeTreeGraphicsAdapter(_graphics, _pen);
        }

        public ITreeGraphics Visit(VisualTree<T> tree)
        {
            return new TreeGraphicsAdapter(_graphics, _pen);
        }
    }
}
