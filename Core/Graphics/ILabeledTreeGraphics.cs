using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL.Core.Graphics
{
    public interface ILabeledTreeGraphics : INodeTreeGraphics
    {
        IFontParams GetFontParams(float height);

        void DrawString(IFontParams fontParams, float x, float y, string str);
    }
}
