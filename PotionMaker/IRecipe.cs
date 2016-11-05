using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PotionMaker
{
    abstract class IRecipe
    {
        public abstract List<IComponent> Components { get; }
        public abstract List<Step> Steps { get; } 
        public abstract String Name { get; }
        public abstract String Description { get; }

        public void Navigate()
        {
            (Window.Current.Content as Frame).Navigate(typeof(PotionPage), this);
        }
    }
}
