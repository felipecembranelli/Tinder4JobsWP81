using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TinderApp.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
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
        //private TinderApp.Library.Linkedin.LinkedinUser _LoggedUser;

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

            //_viewModel.OnMatch += _viewModel_OnMatch;
            _viewModel.OnAnimation += _viewModel_OnAnimation;

            base.OnNavigatedTo(e);

        }

        private void _viewModel_OnAnimation(object sender, JobReccommendationsViewModel.AnimationEventArgs e)
        {
            //CoreWindow.GetForCurrentThread().Dispatcher.BeginInvoke(() =>
            //{
            switch (e.AnimationName)
            {
                case "Like":
                    LikeAnimation.Begin();
                    break;

                case "Pass":
                    PassAnimation.Begin();
                    break;
            }
            //});
        }

        //private void _viewModel_OnMatch(object sender, EventArgs e)
        //{
        //    matchBorder.Visibility = Visibility.Visible;
        //    ShowMatch.Begin();
        //}

        //private void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{

        //}

        //private void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        //{

        //}

        //private void keepPlayingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    matchBorder.Visibility = Visibility.Collapsed;
        //    _viewModel.NextJobSuggestion();

        //}

        //private void sendMessageBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    matchBorder.Visibility = Visibility.Collapsed;

        //    string currentId = _viewModel.CurrentJobReccomendation.Id;

        //    _viewModel.NextJobSuggestion();
        //    //NavigationService.Navigate(new Uri("/ViewConversationPage.xaml?id=" + currentId, UriKind.Relative));

        //}

        #region Swiping

        private void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.OnDragDelta(sender, e);
        }

        private void OnDragDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // HorizontalChange and VerticalChange from DragDeltaGestureEventArgs are now
            // DeltaManipulation.Translation.X and DeltaManipulation.Translation.Y.
            transform.TranslateX += e.Delta.Translation.X;

            var trasnform = (sender as FrameworkElement).RenderTransform as CompositeTransform;
            trasnform.TranslateX += e.Delta.Translation.X;
            trasnform.TranslateY += e.Delta.Translation.Y;
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            //if (e.IsInertial)
            //{
            //    this.OnFlick(sender, e);
            //}
            //else
                transform.TranslateX = 0;
        }

        private void OnFlick(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            double horizontalVelocity = e.Velocities.Linear.X;
            double verticalVelocity = e.Velocities.Linear.Y;

            double angle = Math.Round(this.GetAngle(horizontalVelocity, verticalVelocity));

            if (this.GetDirection(horizontalVelocity, verticalVelocity) ==  Windows.UI.Xaml.Controls.Orientation.Horizontal)
            {
                if (angle >= 180)
                    _viewModel.RejectUser();
                else
                    _viewModel.LikeUser();

                transform.TranslateX = 0;
            }
        }

        private Orientation GetDirection(double x, double y)
        {
            return Math.Abs(x) >= Math.Abs(y) ? Windows.UI.Xaml.Controls.Orientation.Horizontal : Windows.UI.Xaml.Controls.Orientation.Vertical;
        }

        private double GetAngle(double x, double y)
        {
            // Note that this function works in xaml coordinates, where positive y is down, and the
            // angle is computed clockwise from the x-axis. 
            double angle = Math.Atan2(y, x);

            // Atan2() returns values between pi and -pi.  We want a value between
            // 0 and 2 pi.  In order to compensate for this, we'll add 2 pi to the angle
            // if it's less than 0, and then multiply by 180 / pi to get the angle
            // in degrees rather than radians, which are the expected units in XAML.
            if (angle < 0)
            {
                angle += 2 * Math.PI;
            }

            return angle * 180 / Math.PI;
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string x = "test";
        }

        private void Border_ManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            //double horizontalVelocity = e.Velocities.Linear.X;
            //double verticalVelocity = e.Velocities.Linear.Y;
        }

        private void Border_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingRoutedEventArgs e)
        {
            double horizontalVelocity = e.Velocities.Linear.X;
            double verticalVelocity = e.Velocities.Linear.Y;

            double angle = Math.Round(this.GetAngle(horizontalVelocity, verticalVelocity));

            if (this.GetDirection(horizontalVelocity, verticalVelocity) == Windows.UI.Xaml.Controls.Orientation.Horizontal)
            {
                if (angle >= 180)
                    _viewModel.RejectUser();
                else
                    _viewModel.LikeUser();

                transform.TranslateX = 0;
            }
        }

        private void Border_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            //double horizontalVelocity = e.Velocities.Linear.X;
            //double verticalVelocity = e.Velocities.Linear.Y;
        }

        #endregion


        #region "Pass and Like buttons"

        private void button_Click(object sender, RoutedEventArgs e)
        {
           // this.image.Visibility = Windows.UI.Xaml.Visibility.Visible;
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //this.image1.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        #endregion

        private void JobList_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PivotPage));
        }

       
    }
}
