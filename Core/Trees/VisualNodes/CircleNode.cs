﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Additions;
using AVL.Core.Graphics;

namespace AVL.Core.Trees.VisualNodes
{
    /*                      Описывает представление узла в виде круга                   */

    public class CircleNode : IVisualNode
    {

        //  Расстояние между центрами дочернего и родительского узла по вертикали
        private float stepHeigth;


        //  Размер стороны квадрата, описывающего круг = диаметру круга
        private float circleSize;


        public float FigureSize { get { return circleSize; } set { circleSize = value; } }
        public float StepHeight { get { return stepHeigth; } set { stepHeigth = value; } }


        //  Вывод круга по заданным координатам
        public void DrawNode(INodeTreeGraphics graph, float x, float y)
        { 
            graph.DrawEllipse(x - circleSize / 2, y - circleSize / 2, circleSize, circleSize);
        }


        //  Определяет насколко нужно сместить координаты по X и Y, относительно
        // центра фигуры, чтобы линяя выходила из края фигуры.
        public Offset GetOffset(float x1, float y, float x2)
        {
            /*  Тангенс угла наклона линии при вхождении в круг (линяя стремится в центр круга) равна
             *  тангенсу угла наклона отрезка, соединяющего два центра окружностей - дочерней и родительской
             *  Этот тангенс можно рассчитать как (y2 - y1)/(x2 - x1) или (y2 - y1)/(x1 - x2), в зависимости от
             *  того, куда направленна линяя (x2 или x1 больше).
             *  y2 - y1 = stepHeigth
             *  знак k не имееет значения, так как в последствии k возводится в квадрат
             */
            float k = stepHeigth / (x1 - x2);
            float r = circleSize / 2;

            Offset offset = new Offset();
            /*
             * Если рассматривать точку вхождения отрезка, соединяющего центры окружностей, в круг как начало 
             * координат, то отрезок, соединяющий, точку вхождения в круг и центр этого круга можно описать уравнением
             * Y = K*X. Причем длина данного отрезка будет равна R - радиусу оружности. А расстояние между эими точками
             * по X и Y и будет необходимым смещением для начала рисования отрезка, соединяющего два круга, от центра 
             * окружности.
             * Таким образом можно рассмотреть треугольник где R - гипотенуза, а X и Y = X*K - катеты. Тогда по теореме
             * Пифагора: R^2 = X^2 + (X*K)^2 = X^2 + X^2 * K^2
             * R^2 = X^2 * (1 + K^2)    -->     X = (R^2 / (1 + K^2)) ^ 1/2
             */
            offset.X = (float)Math.Sqrt((double)(r * r) / (k * k + 1));
            //  Y = K*X
            offset.Y = offset.X * Math.Abs(k);

            return offset;
        }
    }
}
