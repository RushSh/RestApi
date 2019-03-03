#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;
using Windows.System.Profile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RestApi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public SplitView MainSpiltView { get; set; }

        public int ListSelection
        {
            get { return (int)GetValue(ListSelectionProperty); }
            set { SetValue(ListSelectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ListSelection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListSelectionProperty =
            DependencyProperty.Register("ListSelection", typeof(int), typeof(MainPage), new PropertyMetadata(-1, new PropertyChangedCallback(OnListSelectionChanged)));

        private static void OnListSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int a = (int)e.NewValue;
            MainPage main = d as MainPage;
            main.SpiltViewPaneListBox.SelectedIndex = a;
            if (a == 0)
                FrameNavigationService.Header.Text = "Main Page";
            else if (a == 1)
                FrameNavigationService.Header.Text = "Page with Tab";
            else if (a == 2)
                FrameNavigationService.Header.Text = "Page with ListView";
        }
        public MainPage()
        {
            this.InitializeComponent();
            FrameNavigationService.CurrentFrame = this.MyFrame;
            SpiltViewPaneListBox.SelectedIndex = 0;
            this.backbutton.Visibility = Visibility.Collapsed;
        }

        private void MySplitView_PaneClosed(SplitView sender, object args)
        {
            MySplitView.Content.Opacity = 1;
        }

        private void SpiltViewPaneListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListSelection != SpiltViewPaneListBox.SelectedIndex)
            {
                if (FrameNavigationService.CurrentFrame == null)
                    return;
                var listBox = sender as ListBox;
                if (DeviceFamily.GetDeviceFamily() == Devices.Desktop)
                {
                    MySplitView.Content = FrameNavigationService.CurrentFrame;
                    MainSpiltView = MySplitView;

                    if (listBox.SelectedIndex == 0)
                    {
                        this.layoutheader.Text = "Main Page";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item1), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                        this.backbutton.Visibility = Visibility.Visible;
                    }
                    else if (listBox.SelectedIndex == 1)
                    {
                        this.layoutheader.Text = "Page with Tab";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item2), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                        this.backbutton.Visibility = Visibility.Visible;
                    }
                    else if (listBox.SelectedIndex == 2)
                    {
                        this.layoutheader.Text = "Page with ListView";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item3), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                        this.backbutton.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (listBox.SelectedIndex == 0)
                    {

                        FrameNavigationService.Header.Text = "Main Page";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item1), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;

                    }

                    else if (listBox.SelectedIndex == 1)
                    {
                        FrameNavigationService.Header.Text = "Page with Tab";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item2), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                    }
                    else if (listBox.SelectedIndex == 2)
                    {
                        FrameNavigationService.Header.Text = "Page with ListView";
                        FrameNavigationService.CurrentFrame.Navigate(typeof(Item3), this);
                        if (MySplitView.IsPaneOpen)
                            MySplitView.IsPaneOpen = false;
                    }
                    if (!MySplitView.IsPaneOpen)
                    {
                        SpiltViewPaneListBox.SelectedIndex = -1;
                        ListSelection = -1;
                    }
                }
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;

            if (MySplitView.IsPaneOpen)
                MySplitView.Content.Opacity = 0.4;
        }

        private void backbutton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationService.GoBack();
        }
    }

    public static class FrameNavigationService
    {

        public static Button BackButton;
        public static TextBlock Header;

        public static Frame currentFrame;
        public static Frame CurrentFrame
        {
            get
            {
                return currentFrame;
            }
            set
            {
                currentFrame = value;
            }
        }

        public static void GoBack()
        {
            if (CurrentFrame.CanGoBack)
            {
                CurrentFrame.GoBack();
            }

            if (!CurrentFrame.CanGoBack)
            {
                if (BackButton != null)
                    BackButton.Visibility = Visibility.Collapsed;
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }
    }

    public static class DeviceFamily
    {
        public static Devices GetDeviceFamily()
        {
            if (AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
                return Devices.Mobile;
            return Devices.Desktop;
        }
    }

    public enum Devices
    {
        Desktop,
        Mobile
    }
}
