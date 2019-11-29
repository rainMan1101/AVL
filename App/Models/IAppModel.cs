using System;
using App;
using App.Tree;
using AVL.App.VisualTree;
using AVL.Core.Graphics;

namespace AVL.App.Models
{
    public interface IAppModel
    {
        //  Отображать дерево
        void DrawTree(int heigth, int width, EDrawNodeMode drawNodeMode, EDrawTreeMode drawTreeMode);

        //  Изменять направление
        void ReplaceAgreement(bool agreement);

        //  Оптимальная высота для области рисования в FullScreen окне
        int GetOptimalDrawingPanelHeight();

        //  Оптимальная ширина для области рисования в FullScreen окне
        int GetOptimalDrawingPanelWidth();

        // true - если удалось добавить
        bool TryAdd(Decimal value);

        void FillingAdd();

        void Delete(Decimal value);

        // Возвращает количество шагов при поиске
        bool Find(Decimal value, out int countSteps);

        // Строка с вершинами обратного обхода дерева
        string Traversal();
    }
}
