using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Additions;
using AVL.Core.Graphics;
using AVL.Core.Trees.VisualNodes;


namespace AVL.Core.Trees.VisualTrees
{
    public class LabeledTree<T> : NodeTree<T>
    {
        private ILabeledTreeGraphics _graph;
        private IVisualNode _visualNode;
        
        public LabeledTree(Node<T> tree, ILabeledTreeGraphics graphics, 
            IVisualNode visualNode, bool agreement) 
            : base(tree, graphics, visualNode, agreement)
        {
            _graph = graphics;
            _visualNode = visualNode;
        }
        
        protected void DrawInNode(IFontParams fontParams, string str, float x, float y, bool direction)
        {
            float width = fontParams.GetWidth(str);
            float height = fontParams.GetHight(str);

            // Изначально левый верхний угол символа в центре узла - сдвигаем в цент узла центр символа
            _graph.DrawString(fontParams, x - width / 2, y - height / 2, str);
        }

        protected void DrawUnderNode(IFontParams fontParams, string str, float x, float y, bool direction)
        {
            float width = fontParams.GetWidth(str);
            float height = fontParams.GetHight(str);

            // Изначально левый верхний угол символа в центре узла - сдвигаем в цент узла центр символа
            _graph.DrawString(fontParams, x - width / 2, y - height / 2 + 1.1f * _visualNode.FigureSize, str);
        }

        protected void DrawOnSide(IFontParams fontParams, string str, float x, float y)
        {
            float height = fontParams.GetHight(str);

            _graph.DrawString(fontParams, x, y - _visualNode.FigureSize / 2 - height, str);
        }
    }

}
