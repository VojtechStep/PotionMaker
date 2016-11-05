using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace PotionMaker.Recipes
{
    class SampleRecipe : IRecipe
    {
        List<IComponent> _comp = new List<IComponent>
        {
            new WaterComp(),
            new TeaComp()
        };

        List<Step> _steps = new List<Step>
        {
            new Step
            {
                Component = new WaterComp(),
                MinTemperature = 80,
                MaxTemperature = 100
            },
            new Step
            {
                Component = new TeaComp(),
                MinTemperature = 80,
                MaxTemperature = 100
            }
        };

        public override List<Step> Steps
        {
            get
            {
                return _steps;
            }
        }

        public override List<IComponent> Components
        {
            get
            {
                return _comp;    
            }
        }

        public override String Name
        {
            get
            {
                return "Sample Potion";
            }
        }

        public override String Description
        {
            get
            {
                return "Only a test";
            }
        }
    }
}
