using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PotionMaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<IRecipe> Recipes = new List<IRecipe>();
        List<IRecipe> FirstRecipes;
        List<IRecipe> LastRecipes;

        public MainPage()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            Recipes = new List<IRecipe>
            {
                new Recipes.SampleRecipe()
            };
            FirstRecipes = Recipes.Where(p => Recipes.IndexOf(p) % 2 == 0).ToList();
            LastRecipes = Recipes.Where(p => Recipes.IndexOf(p) % 2 == 1).ToList();
            this.InitializeComponent();
        }
    }
}
