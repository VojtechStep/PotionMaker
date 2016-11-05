using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionMaker
{
    class Step
    {
        public IComponent Component { get; set; }
        public Int32 MinTemperature { get; set; }
        public Int32 MaxTemperature { get; set; }
    }
}
