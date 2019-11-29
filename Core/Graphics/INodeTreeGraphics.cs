using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL.Core.Graphics
{
    public interface INodeTreeGraphics : ITreeGraphics
    {
        void DrawEllipse(float x1, float y1, float x2, float y2);

        void DrawRectangle(float x1, float y1, float x2, float y2);
    }
}
