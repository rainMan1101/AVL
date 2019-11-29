using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Additions;
using AVL.Core.Graphics;

namespace AVL.Core.Trees.VisualNodes
{
    public class SquareNode : IVisualNode
    {
        //  Расстояние между центрами дочернего и родительского узла по вертикали
        private float stepHeigth;

        //  Размер стороны квадрата
        private float squareSize;


        public float FigureSize { get { return squareSize; } set { squareSize = value; } }
        public float StepHeight { get { return stepHeigth; } set { stepHeigth = value; } }

        public void DrawNode(INodeTreeGraphics graph, float x, float y)
        {
            graph.DrawRectangle(x - squareSize / 2, y - squareSize / 2, squareSize, squareSize);
        }

        //  Определяет насколко нужно сместить координаты по X и Y, относительно
        // центра фигуры, чтобы линяя выходила из края фигуры.
        public Offset GetOffset(float x1, float y, float x2)
        {
            Offset offset = new Offset();
            //  Линии будут выходить из вершин квадрата
            offset.X = (float)Math.Sqrt(0.29 * squareSize * squareSize);
            offset.Y = offset.X;
            return offset;
        }
    }
}
