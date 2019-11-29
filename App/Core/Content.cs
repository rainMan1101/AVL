using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVL.Core.Trees;

namespace App
{
    public class Content
    {
        public Decimal Value { get;  }

        public Int32 Height { get; set; }
        
        public Content(Decimal value, Int32 height)
        {
            Value = value;
            Height = height;
        }
    }
}
