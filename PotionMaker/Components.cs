using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace PotionMaker
{
    class WaterComp : IComponent
    {
        public Uri ImageLocation
        {
            get
            {
                return new Uri("ms-appx:///Assets/water.png");
            }
        }

        String IComponent.Name
        {
            get
            {
                return "Voda";
            }
        }
    }

    class TeaComp : IComponent
    {
        public Uri ImageLocation
        {
            get
            {
                return new Uri("ms-appx:///Assets/tea.png");
            }
        }

        public String Name
        {
            get
            {
                return "Čaj";
            }
        }
    }
}
