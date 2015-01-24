using POCMigrationTinder4Jobs.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TinderApp.Library.Controls;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace POCMigrationTinder4Jobs
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        private TransitionCollection transitions;

        public static CustomPhoneApplicationFrame RootFrame { get; private set; }

        //public CustomPhoneApplicationFrame RootFrameInstance { get { return RootFrame; } }

        public RightSideBarControl RightSideBar { get; set; }

        public Boolean JustLoggedOut { get; set; }


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            //TopBarViewModel.ShowTopButtons = Visibility.Collapsed;

            this.InitializeComponent();
            this.Suspending += this.OnSuspending;

            //RootFrame = new CustomPhoneApplicationFrame();
            //RootFrame.Navigated += CompleteInitializePhoneApplication;

            //RootFrame.SetValue(CustomPhoneApplicationFrame.StyleProperty, Application.Current.Resources["CustomFrame"]);

            //RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            //RootFrame.Navigated += CheckForResetNavigation;

            //phoneApplicationInitialized = true;

        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            //Frame rootFrame = Window.Current.Content as Frame;
            CustomPhoneApplicationFrame rootFrame = Window.Current.Content as CustomPhoneApplicationFrame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page.
                rootFrame = new CustomPhoneApplicationFrame();

                // Associate the frame with a SuspensionManager key.
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

                // TODO: Change this value to a cache size that is appropriate for your application.
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate.
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue.
                    }
                }

                // Place the frame in the current Window.
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter.
                if (!rootFrame.Navigate(typeof(InitialPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active.
            Window.Current.Activate();
        }

        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            //VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            //if (TinderSession.CurrentSession != null)
            //    RootFrame.Navigate(new Uri("/EditProfile.xaml", UriKind.Relative));
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            //if (TinderSession.CurrentSession != null)
            //    RootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            //VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            //if (TinderSession.CurrentSession != null)
            //    RootFrame.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void privacyPolicy_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Phone.Tasks.WebBrowserTask webTask = new Microsoft.Phone.Tasks.WebBrowserTask();
            //webTask.Uri = new Uri("http://www.gotinder.com/privacy/", UriKind.Absolute);
            //webTask.Show();
        }

        public void ShowTopBar()
        {
            //RootFrame.ViewModel.TopBarVisible = true;
        }
    }
}
