﻿using System.ComponentModel;
using PhantasmaMail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationRenderer))]
namespace PhantasmaMail.iOS.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationBar.BarTintColor = UIColor.FromPatternImage(UIImage.FromFile("TopBar.png"));
            Element.PropertyChanged += HandlePropertyChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var navPage = (NavigationPage)Element;
                navPage.PropertyChanged -= HandlePropertyChanged;
            }

            base.Dispose(disposing);
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName || e.PropertyName == Xamarin.Forms.PlatformConfiguration.iOSSpecific.NavigationPage.StatusBarTextColorModeProperty.PropertyName)
                UpdateStatusBarStyle();
        }

        private async void UpdateStatusBarStyle()
        {
            // we want to change defaults XF status bar style calcs
            await System.Threading.Tasks.Task.Delay(1);
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }
    }
}