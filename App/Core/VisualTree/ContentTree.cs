using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App;
using App.Tree;
using AVL.Core.Graphics;
using AVL.Core.Trees;
using AVL.Core.Trees.VisualNodes;
using AVL.Core.Trees.VisualTrees;

namespace AVL.App.VisualTree
{
    internal sealed class ContentTree : LabeledTree<Content>
    {
        private EDrawTreeMode _drawingMode;
        private IFontParams _fontParams;
        private float _branchHeight;

        public ContentTree(Node<Content> tree, ILabeledTreeGraphics graphics, 
            IVisualNode visualNode, bool agreement)
            : base(tree, graphics, visualNode, agreement)
        {
            _drawingMode = EDrawTreeMode.Nothing;
            Initialized += (width, height, branchWidth, branchHeight) => {
                _branchHeight = branchHeight;
                _fontParams = graphics.GetFontParams(_branchHeight / 2);
            };
        }

        public void SetDrawingMode(EDrawTreeMode drawingMode)
        {
            _drawingMode = drawingMode;
        }

        // ----------------------------------------------------------------------------------------
        /*                Графические функции, используемые GraphBuilder             */

        protected override void DrawInTheFork(float x, float y, Node<Content> node, bool direction)
        {
            base.DrawInTheFork(x, y, node, direction);

            if (_drawingMode == EDrawTreeMode.Value || _drawingMode == EDrawTreeMode.ValueAndHeight)
                DrawInNode(_fontParams, node.Content.Value.ToString(), x, y, direction);
        }

        protected override void DrawInTheEnd(float x, float y, Node<Content> node, bool direction)
        {
            DrawInTheFork(x, y, node, direction);
        }


        protected override void DrawLeftSide(float x, float y, float xLeft, Node<Content> parentNode)
        {
            base.DrawLeftSide(x, y, xLeft, parentNode);

            if (_drawingMode == EDrawTreeMode.Height || _drawingMode == EDrawTreeMode.ValueAndHeight)
                DrawOnSide(_fontParams, parentNode.Left.Content.Height.ToString(), xLeft, y + _branchHeight);
        }

        protected override void DrawRightSide(float x, float y, float xRight, Node<Content> parentNode)
        {
            base.DrawRightSide(x, y, xRight, parentNode);

            if (_drawingMode == EDrawTreeMode.Height || _drawingMode == EDrawTreeMode.ValueAndHeight)
                DrawOnSide(_fontParams, parentNode.Right.Content.Height.ToString(), xRight, y + _branchHeight);
        }

        // ----------------------------------------------------------------------------------------

    }
}
