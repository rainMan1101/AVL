using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.Core.Trees.VisualTrees
{
    /*      Интерфейс, который должны поддерживать классы, визуализирующие древовидную структуру      */

    public interface IVisualTree<T>
    { 
        //  Рисовать дерево на заданной области определенного размера
        void Draw(float width, float height);

        //  Определять оптимальную высоту для экрана вывода
        int GetOptimalHeight();

        //  Определять оптимальную ширину для экрана вывода
        int GetOptimalWidth();

        void ChangeAgreement(bool agreement);

    }
}
