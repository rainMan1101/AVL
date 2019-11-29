using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Additions;
using AVL.Core.Graphics;

namespace AVL.Core.Trees.VisualNodes
{
    /*
     *      Интерфейс, содержащий набор необходимых свойств и методов, 
     *  для классов, описывающих визуальное представления узлов дерева.
     *  
     */
    public interface IVisualNode
    {

        //  Размер стороны квадрата, ограничивающего фигуру
        //  (Размер фигуры задается в TreeWithNodes в зависимости от размера экрана)
        float FigureSize { get; set; }


        //  Принятое расстояние по вертикали между центами узлов
        float StepHeight { get; set; }


        //  Вывод фигуры по заданным координатам (координаты центра узла)
        void DrawNode(INodeTreeGraphics graph, float x, float y);


        //  Насколько нужно сместить координаты по X и Y, относительно центра фигуры, 
        //  чтобы линяя выходила из края фигуры
        Offset GetOffset(float x1, float y, float x2);
    }
}
