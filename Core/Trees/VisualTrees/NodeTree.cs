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
    public class NodeTree<T> : VisualTree<T>
    {
        private INodeTreeGraphics _graph;
        private IVisualNode _visualNode;
        
        public NodeTree(Node<T> tree, INodeTreeGraphics graphics, 
            IVisualNode visualNode, bool agreement) 
            : base(tree, graphics, agreement)
        {
            _visualNode = visualNode;
            Initialized += (width, height, branchWidth, branchHeight) => {
                _separateWidth = GetMaxSeparateWidth(width);
                _visualNode.StepHeight = branchHeight;
                _visualNode.FigureSize = branchWidth / 2;
            };
        }

        // ----------------------------------------------------------------------------------------
        /*                Графические функции, используемые GraphBuilder             */

        protected override void DrawInTheFork(float x, float y, Node<T> node, bool direction)
        {
            _visualNode.DrawNode(_graph, x, y);
        }

        protected override void DrawInTheEnd(float x, float y, Node<T> node, bool direction)
        {
            _visualNode.DrawNode(_graph, x, y);
        }

        protected override void DrawLeftSide(float x, float y, float xLeft, Node<T> node)
        {
            Offset offset = _visualNode.GetOffset(x, y, xLeft);
            float newY = y + _visualNode.StepHeight;

            _graph.DrawLine(x - offset.X, y + offset.Y, xLeft + offset.X, newY - offset.Y);
        }

        protected override void DrawRightSide(float x, float y, float xRight, Node<T> node)
        {
            Offset offset = _visualNode.GetOffset(x, y, xRight);
            float newY = y + _visualNode.StepHeight;

            _graph.DrawLine(x + offset.X, y + offset.Y, xRight - offset.X, newY - offset.Y);
        }

        // ----------------------------------------------------------------------------------------

    }
}
