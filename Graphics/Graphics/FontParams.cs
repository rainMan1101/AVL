using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Graphics;

namespace AVL.Graphics
{
    internal class FontParams : IFontParams
    {
        private Font _font;

        public FontParams(float height)
        {
            _font = AdjustmentFontSize.GetFont(height);
        }

        public float GetHight(string str)
        {
            return AdjustmentFontSize.GetHeight(str, _font);
        }

        public float GetWidth(string str)
        {
            return AdjustmentFontSize.GetWidth(str, _font);
        }

        public Font Font => _font;
    }
}
