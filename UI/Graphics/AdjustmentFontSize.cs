using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AVL.UI.Graphics
{
    internal class AdjustmentFontSize
    {
        //  Размер шрифта, используемый по-умолчанию
        private const int DEFAULT_FONT_SIZE = 7;

        //  Название шрифта, используемого по-умолчанию
        private static FontFamily DEFAULT_FONT_FAMILY = FontFamily.GenericSansSerif;


        //  Информация о размере строки, для заданной высоты 
        public static Font GetFont(float wishfulHeight)
        {
            if (wishfulHeight > 0 && wishfulHeight < 100)
            {
                string str = "0";
                Font lastFont = new Font(DEFAULT_FONT_FAMILY, 1);
                System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));

                int index;
                for (index = 2; index < 100; index++)
                {
                    Font font = new Font(DEFAULT_FONT_FAMILY, index);

                    float current = graph.MeasureString(str, font).Height;
                    float last = graph.MeasureString(str, lastFont).Height;

                    // Условие выхода - найден наиболее подходящий по размеру шрифт
                    if (Math.Abs(wishfulHeight - current) > Math.Abs(wishfulHeight - last)) break;

                    lastFont = font;
                }

                //  Желаемый шрифт так и не был подобран - установка размера по-умолчанию
                if (index != 100)
                    return lastFont;
            }

            return new Font(DEFAULT_FONT_FAMILY, DEFAULT_FONT_SIZE);
        }

        public static float GetHeight(string str, Font font)
        {
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            return graph.MeasureString(str, font).Height;
        }

        public static float GetWidth(string str, Font font)
        {
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(new Bitmap(100, 100));
            return graph.MeasureString(str, font).Width;
        }
    }
}
