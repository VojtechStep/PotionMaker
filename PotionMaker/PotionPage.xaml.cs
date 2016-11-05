using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PotionMaker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PotionPage : Page
    {
        IRecipe Recipe { get; set; }
        Int32 CurrentStep = 0;
        Int32 CurrentTemp { get; set; } = 0;
        Double TempAugmentRatio = 2d;
        DispatcherTimer tempTimer;
        public PotionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Recipe = e.Parameter as IRecipe;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            foreach (var comp in Recipe.Components)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                var img = new Image();
                img.Source = new BitmapImage(comp.ImageLocation);
                Debug.WriteLine(comp.ImageLocation.AbsolutePath);
                img.SetValue(Grid.RowProperty, 1);
                img.SetValue(Grid.ColumnProperty, MainGrid.ColumnDefinitions.Count - 1);
                TempIndicator.SetValue(Grid.ColumnProperty, MainGrid.ColumnDefinitions.Count - 1);
                img.CanDrag = true;
                img.Stretch = Stretch.Uniform;
                img.Tag = new TagInfo
                {
                    Comp = comp,
                    Lifted = false
                };
                img.DragStarting += (s, ev) =>
                {
                    (img.Tag as TagInfo).Lifted = true;
                };
                img.DropCompleted += (s, ev) =>
                {
                    if (ev.DropResult != Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move) (img.Tag as TagInfo).Lifted = false;
                };
                MainGrid.Children.Add(img);
            }
            tempTimer = new DispatcherTimer();
            tempTimer.Tick += (s, ev) =>
            {
                CurrentTemp += (Int32)TempAugmentRatio;
                TempEnumerator.Text = CurrentTemp.ToString();
            };
            tempTimer.Interval = TimeSpan.FromSeconds(1);
            tempTimer.Start();
            MinTempEnumerator.Text = Recipe.Steps[0].MinTemperature.ToString();
            MaxTempEnumerator.Text = Recipe.Steps[0].MaxTemperature.ToString();
        }

        private async void DropOnKettle(Object sender, DragEventArgs e)
        {
            var current = MainGrid.Children.OfType<Image>().First(p => p.Tag is TagInfo && (p.Tag as TagInfo).Lifted);
            var expected = Recipe.Steps[CurrentStep];
            if (expected.Component.GetType() == (current.Tag as TagInfo).Comp.GetType() && expected.MinTemperature <= CurrentTemp && expected.MaxTemperature >= CurrentTemp)
            {
                CurrentStep++;
                MainGrid.Children.Remove(current);
                if (CurrentStep == Recipe.Steps.Count)
                {
                    await new Windows.UI.Popups.MessageDialog($"Congratulations!! You brew {Recipe.Name}").ShowAsync();
                    (Window.Current.Content as Frame).GoBack();
                }
                else
                {
                    MinTempEnumerator.Text = Recipe.Steps[CurrentStep].MinTemperature.ToString();
                    MaxTempEnumerator.Text = Recipe.Steps[CurrentStep].MaxTemperature.ToString();
                }
            }
            else await new Windows.UI.Popups.MessageDialog("You fucked up").ShowAsync();
        }

        private void Grid_DragOver(Object sender, DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
        }

        private void FlameManipulationDelta(Object sender, ManipulationDeltaRoutedEventArgs e)
        {
            var tr = FlameImage.RenderTransform as ScaleTransform;
            tr.CenterY = FlameImage.ActualHeight;
            tr.ScaleX = tr.ScaleY = Math.Max(tr.ScaleX - e.Delta.Translation.Y / 100, 0.5);
            tr.ScaleX = tr.ScaleY = Math.Min(tr.ScaleX - e.Delta.Translation.Y / 100, 3);
            if (tr.ScaleX < 1 && TempAugmentRatio > 0)
                FlameImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/flame_bw.png"));
            else if (tr.ScaleX > 1 && TempAugmentRatio < 0)
                FlameImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/flame.png"));
            TempAugmentRatio = (tr.ScaleY - 1) * 2;
        }
    }

    class TagInfo
    {
        public IComponent Comp { get; set; }
        public Boolean Lifted { get; set; }
    }
}
