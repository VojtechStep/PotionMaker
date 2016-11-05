using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionMaker
{
    interface IComponent
    {
        String Name { get; }
        Uri ImageLocation { get; }
    }
}
