using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TinderApp.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace POCMigrationTinder4Jobs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        private JobReccommendationsViewModel _viewModel;
        private TinderApp.Library.Linkedin.LinkedinUser _LoggedUser;

        public Main()
        {
            this.InitializeComponent();

            _viewModel = new JobReccommendationsViewModel();
            DataContext = _viewModel;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (App.Current as App).ShowTopBar();

            _viewModel.OnMatch += _viewModel_OnMatch;
            _viewModel.OnAnimation += _viewModel_OnAnimation;

            base.OnNavigatedTo(e);

        }

        private void _viewModel_OnAnimation(object sender, JobReccommendationsViewModel.AnimationEventArgs e)
        {
            //Deployment.Current.Dispatcher.BeginInvoke(() =>
            //{
            //    switch (e.AnimationName)
            //    {
            //        case "Like":
            //            LikeAnimation.Begin();
            //            break;

            //        case "Pass":
            //            PassAnimation.Begin();
            //            break;
            //    }
            //});
        }

        private void _viewModel_OnMatch(object sender, EventArgs e)
        {
            matchBorder.Visibility = Visibility.Visible;
            ShowMatch.Begin();
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }

        private void keepPlayingButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sendMessageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        

    }
}
