using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.Core.Trees.VisualTrees
{
    public class VisualTree<T> : IVisualTree<T>
    {
        protected readonly ITreeGraphics _graph;

        private readonly Node<T> _rootNode;

        private bool _agreement;

        private float _branchWidth;

        private float _branchHeight;

        protected float _separateWidth;

        

        public VisualTree(Node<T> tree, ITreeGraphics graphics, bool agreement)
        {
            if (_rootNode == null) throw new ArgumentException(nameof(tree) + " was null.");
            _rootNode = tree;
            _agreement = agreement;
            _graph = graphics;
        }

        public void ChangeAgreement(bool agreement)
        {
            _agreement = agreement;
        }

        public int GetOptimalHeight()
        {
            const int optimalNodeBranchHeight = 50;
            return optimalNodeBranchHeight * (GetHeight(_rootNode) + 2);
        }


        public int GetOptimalWidth()
        {
            const int optimalNodeBranchWidth = 50;
            return optimalNodeBranchWidth * (GetWidth(_rootNode) + 2);
        }


        // ----------------------------------------------------------------------------------------
        /*                Графические функции, используемые GraphBuilder             */

        protected virtual void DrawInTheFork(float x, float y, Node<T> node, bool direction) { }

        protected virtual void DrawInTheEnd(float x, float y, Node<T> node, bool direction) { }


        protected virtual void DrawLeftSide(float x, float y, float xLeft, Node<T> parentNode)
        {
            _graph.DrawLine(x, y, xLeft, y + _branchHeight);
        }

        protected virtual void DrawRightSide(float x, float y, float xRight, Node<T> parentNode)
        {
            _graph.DrawLine(x, y, xRight, y + _branchHeight);
        }

        // ----------------------------------------------------------------------------------------

        public void Draw(float width, float height)
        {
            int leftCount = _rootNode.Left != null ? GetWidth(_rootNode.Left) : 0;
            int rightCount = _rootNode.Right != null ? GetWidth(_rootNode.Right) : 0;

            int widthCount = leftCount + rightCount;
            int heightCount = GetHeight(_rootNode);

            _branchWidth = GetCoeffWidth(width, widthCount);
            _branchHeight = GetStepHeight(height, heightCount);

            float startLeftWidth = GetStartLeftWidth(leftCount);

            Initialized?.Invoke(width, height, _branchWidth, _branchHeight);

            GraphBuilder(startLeftWidth, _branchHeight, _rootNode, false);
        }

        protected delegate void PostInit(float width, float height, float branchWidth, float branchHeight);
        protected event PostInit Initialized;

        //  Построитель - рисует структуру дерева (рекурсивно), при помощи вспомогательных функций
        private void GraphBuilder(float leftParentCoord, float topParentCoord, Node<T> node, bool direction)
        {
            if (node.Left == null && node.Right == null)
                DrawInTheEnd(leftParentCoord, topParentCoord, node, direction);
            else
                DrawInTheFork(leftParentCoord, topParentCoord, node, direction);


            if (node.Left != null)
            {
                //  Расстояние между центрами (в пикселях) от текущего узла до левого дочернего
                float offsetToTheLeft;
                //  Если слева узел не последний (не один)
                if (node.Left.Right != null)
                {
                    //  Количество веток между текущим и левым дочерним узлом
                    int leftWidthRigth = GetWidth(node.Left.Right);
                    offsetToTheLeft = leftWidthRigth * (_branchWidth + _separateWidth);
                }
                else
                    //  Если слева узел 1, то GetWidth(node.LeftChildNode) = 1 * (один шаг по горизонтали) +
                    //  + половина дополнительного расстояния
                    offsetToTheLeft = _branchWidth + _separateWidth / 2;

                //  Текущая координата -(минус - сдвиг влево) смещение = координата центра левого дочернего узла
                float leftChildNodeCoord = leftParentCoord - offsetToTheLeft;

                DrawLeftSide(leftParentCoord, topParentCoord, leftChildNodeCoord, node);
                GraphBuilder(leftChildNodeCoord, topParentCoord + _branchHeight, node.Left, !_agreement);
            }

            if (node.Right != null)
            {
                //  Расстояние между центрами (в пикселях) от текущего узла до правого дочернего
                float offsetToTheRight;
                //  Если справа узел не последний (не один)
                if (node.Right.Left != null)
                {
                    //  Количество веток между текущим и правым дочерним узлом
                    int rightWidthLeft = GetWidth(node.Right.Left);
                    //  Расстояние между центрами (в пикселях) от текущего узла до правого дочернего
                    offsetToTheRight = rightWidthLeft * (_branchWidth + _separateWidth);
                }
                else
                    offsetToTheRight = _branchWidth + _separateWidth / 2;

                //  Координата центра левого дочернго узла
                float rightChildNodeCoord = leftParentCoord + offsetToTheRight;

                DrawRightSide(leftParentCoord, topParentCoord, rightChildNodeCoord, node);
                GraphBuilder(rightChildNodeCoord, topParentCoord + _branchHeight, node.Right, _agreement);
            }
        }




        /* (width - (countWidth - 1) * n) / countWidth = 0.1; 
         * - чтобы ширина линий, соединяющих центры была не меньше 0.1
         * 
         * width - (countWidth - 1) * n = 0.1 * countWidth
         * (countWidth - 1) * n  = width - 0.1 * countWidth
         * n = (width - 0.1 * countWidth) / (countWidth - 1) */

        protected float GetMaxSeparateWidth(float width)
        {
            int countWidth = GetWidth(_rootNode);
            width -= 2 * width / (countWidth + 2);
            return (width - 0.1f * countWidth) / countWidth;
        }

        private float GetStartLeftWidth(int countLeft)
        {
            float startLeftWidth = countLeft * (_branchWidth + 1);
            startLeftWidth += (countLeft - 1) * _separateWidth + _separateWidth / 2;
            return startLeftWidth;
        }


        private float GetCoeffWidth(float width, int countWidth) =>
            (2 * width / countWidth - (countWidth - 1) * _separateWidth) / countWidth;

        private float GetStepHeight(float heigth, int countHeigth) => 
            heigth / (countHeigth + 2);


        private int GetWidth(Node<T> node)
        {
            if (node.Left == null && node.Right == null) return 1;
            if (node.Right == null) return GetWidth(node.Left) + 1;
            if (node.Left == null) return GetWidth(node.Right) + 1;

            int width = 0;
            width += GetWidth(node.Left);
            width += GetWidth(node.Right);
            return width;
        }

        private int GetHeight(Node<T> node)
        {
            int leftHeight = node.Left != null ? GetHeight(node.Left) + 1 : 0;
            int rightHeight = node.Right != null ? GetHeight(node.Right) + 1 : 0;
            return leftHeight >= rightHeight ? leftHeight : rightHeight;
        }
    }
}