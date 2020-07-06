using System;
using RoundedLayoutsSamples.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoundedLayoutsSamples
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var tb = new TabbedPage();
            tb.Children.Add(new RoundedStackLayoutPage());
            tb.Children.Add(new RoundedGridPage());
            tb.Children.Add(new RoundedContentViewPage());

            MainPage = tb;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
