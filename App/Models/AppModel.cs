using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App;
using App.Tree;
using AVL.App.VisualTree;
using AVL.Core;
using AVL.Core.Graphics;
using AVL.Core.Trees;
using AVL.Core.Trees.VisualNodes;
using AVL.Core.Trees.VisualTrees;


namespace AVL.App.Models
{
    public class AppModel : IAppModel
    {
        private Node<Content> _rootNode;

        private AVLTree _avlTree;

        private IVisualTree<Content> _visualTree;

        private bool _agreement;

        private EDrawNodeMode _drawNodeMode;

        private EDrawTreeMode _drawTreeMode;

        private IGraphicsRepository _graphRepos;


        public AppModel(IGraphicsRepository graphRepos)
        {
            FillTree();

            _graphRepos = graphRepos;

            _agreement = true;
            _drawNodeMode = EDrawNodeMode.NotNodeMode;
            _drawTreeMode = EDrawTreeMode.Nothing;
            _avlTree = new AVLTree(_rootNode);
            _visualTree = new VisualTree<Content>(_rootNode, _graphRepos.GetTreeGraphics(), _agreement);
            _avlTree.RootNodeChangedEvent += node => { _rootNode = node; _visualTree.SetTree(_rootNode); };
        }

        private void FillTree()
        {
            //_rootNode = new Node<Content>(new Content(5, 2));
            //_rootNode.Left = new Node<Content>(new Content(3, 1));
            //_rootNode.Right = new Node<Content>(new Content(7, 1));

            //_rootNode = new Node<Content>(new Content(5, 4));
            //_rootNode.Left = new Node<Content>(new Content(3, 3));
            //_rootNode.Left.Left = new Node<Content>(new Content(2, 2));
            //_rootNode.Left.Left.Left = new Node<Content>(new Content(1, 1));
            //_rootNode.Left.Right = new Node<Content>(new Content(4, 1));

            //_rootNode.Right = new Node<Content>(new Content(7, 2));
            //_rootNode.Right.Left = new Node<Content>(new Content(6, 1));
            //_rootNode.Right.Right = new Node<Content>(new Content(8, 1));
        }

        private void CreateVisualTree()
        {
            switch (_drawNodeMode)
            {
                case EDrawNodeMode.CircleMode:
                    _visualTree = new ContentTree(_rootNode, _graphRepos.GetLabeledTreeGraphics(), 
                        new CircleNode(), _agreement);
                    break;

                case EDrawNodeMode.SquareMode:
                    _visualTree = new ContentTree(_rootNode, _graphRepos.GetLabeledTreeGraphics(),
                        new SquareNode(), _agreement);
                    break;

                case EDrawNodeMode.NotNodeMode:
                    _visualTree = new VisualTree<Content>(_rootNode, _graphRepos.GetTreeGraphics(),_agreement);
                    break;
            }
        }


        public void DrawTree(int heigth, int width, EDrawNodeMode drawNodeMode, EDrawTreeMode drawTreeMode)
        {
            if (_drawNodeMode != drawNodeMode)
            {
                switch (drawNodeMode)
                {
                    case EDrawNodeMode.CircleMode:
                        _visualTree = new ContentTree(_rootNode, _graphRepos.GetLabeledTreeGraphics(),
                            new CircleNode(), _agreement);
                        break;

                    case EDrawNodeMode.SquareMode:
                        _visualTree = new ContentTree(_rootNode, _graphRepos.GetLabeledTreeGraphics(),
                            new SquareNode(), _agreement);
                        break;

                    case EDrawNodeMode.NotNodeMode:
                        _visualTree = new VisualTree<Content>(_rootNode, _graphRepos.GetTreeGraphics(), _agreement);
                        break;
                }
            }
            
            (_visualTree as ContentTree)?.SetDrawingMode(drawTreeMode);
            _visualTree.Draw(width, heigth);
            _drawNodeMode = drawNodeMode;
            _drawTreeMode = drawTreeMode;
        }


        public void ReplaceAgreement(bool agreement)
        {
            _visualTree.ChangeAgreement(agreement);
        }

        public int GetOptimalDrawingPanelHeight()
        {
            return _visualTree.GetOptimalHeight();
        }


        public int GetOptimalDrawingPanelWidth()
        {
            return _visualTree.GetOptimalWidth();
        }

        public bool TryAdd(Decimal value)
        {
            return _avlTree.TryInsert(value);
        }

        public void FillingAdd()
        {
            _avlTree.FillingTraversal();
        }

        public void Delete(Decimal value)
        {
            _avlTree.Remove(value);
        }

        public  bool Find(Decimal value, out int countSteps)
        {
            return _avlTree.IsExists(value, out countSteps);
        }

        public string Traversal()
        {
            string result = "";

            foreach (var item in _avlTree.Traversal())
            {
                result += $"{item} ";
            }

            return result;
        }
    }
}
