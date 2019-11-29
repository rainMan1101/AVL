using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL.Core.Graphics
{
    public interface IFontParams
    {
        float GetHight(string str);

        float GetWidth(string str);
    }
}
