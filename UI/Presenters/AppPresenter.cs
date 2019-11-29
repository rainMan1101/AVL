using System;
using System.Collections.Generic;
using System.Windows.Forms;
using App;
using App.Tree;
using AVL.App.Models;
using AVL.App.VisualTree;
using AVL.Core;
using AVL.Core.Graphics;
using AVL.UI;
using AVL.UI.Graphics;
using AVL.UI.Views;

namespace AVL.App.Presenters
{
    public class AppPresenter
    {
        private IView _view;

        private IAppModel _model;
        
        public AppPresenter(IView view)
        {
            _model = new AppModel();
            _view = view;


            //  Переход в полноэкранный режим
            _view.FullScreenModeClick +=
                (obj, ex) =>
                {
                    _view.DrawWindow.Height = _model.GetOptimalDrawingPanelHeight();
                    _view.DrawWindow.Width = _model.GetOptimalDrawingPanelWidth();
                    _view.DrawWindow.Invalidate();
                };

            _view.DrawWindow.Paint += (obj, ex) =>
            {
                _model.DrawTree(_view.DrawWindow.Height, _view.DrawWindow.Width,
                    Convert(_view.DrawNodeMode), GetDrawMode());
            };
                

            _view.ModeChanged +=
                (obj, ex) => _view.DrawWindow.Invalidate();

            _view.DrawWindow.Resize +=
                (obj, ex) => _view.DrawWindow.Invalidate();

            //_view.DrawClick +=
            //    (o, e) => _view.DrawWindow.Invalidate();

            _view.AddEvent += (sender, args) =>
                {
                    if (!_model.TryAdd(_view.Value))
                        MessageBox.Show("Такой элемент уже существует!");
                    _view.DrawWindow.Invalidate();
                };


            _view.FillingAddEvent += (sender, args) =>
            {
                _model.FillingAdd();
                _view.DrawWindow.Invalidate();
            };
            _view.DeleteEvent += (sender, args) =>
            {
                _model.Delete(_view.Value);
                _view.DrawWindow.Invalidate();
            };

            _view.FindEvent += (sender, args) =>
            {
                int countSteps = 0;
                if (!_model.Find(_view.Value, out countSteps))
                    MessageBox.Show("Элемент не найден!");
                _view.CountSteps = countSteps;
            };

            _view.Traversal += (sender, args) => _view.TraversalList = _model.Traversal();
        }


        private EDrawTreeMode GetDrawMode()
        {
            if (_view.DrawValues && _view.DrawHeight)
                return EDrawTreeMode.ValueAndHeight;

            if (_view.DrawValues)
                return EDrawTreeMode.Value;

            if (_view.DrawHeight)
                return EDrawTreeMode.Height;

            return EDrawTreeMode.Nothing;
        }

        private AVL.App.VisualTree.EDrawNodeMode Convert(AVL.UI.EDrawNodeMode drawNodeMode)
        {
            switch (drawNodeMode)
            {
                case AVL.UI.EDrawNodeMode.CircleMode: return AVL.App.VisualTree.EDrawNodeMode.CircleMode;
                case AVL.UI.EDrawNodeMode.SquareMode: return AVL.App.VisualTree.EDrawNodeMode.SquareMode;
                case AVL.UI.EDrawNodeMode.NotNodeMode: return AVL.App.VisualTree.EDrawNodeMode.NotNodeMode;
                default: throw new ArgumentOutOfRangeException(nameof(drawNodeMode));
            }
        }

    }

}
