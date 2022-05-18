using Imaginer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imaginer.Views
{
    /// <summary>
    /// Interaction logic for MediaListView.xaml
    /// </summary>
    public partial class MediaListView : UserControl
    {
        private bool isExecuting = false;
        public MediaListView()
        {
            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as MediaListViewModel;
            vm.Load();
        }

        //Zoom in out logic
        private void SV_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            HRect.Width = SV.ViewportWidth / Zoom.Value;
            HRect.Height = SV.ViewportHeight / Zoom.Value;
            HRect.SetValue(Canvas.LeftProperty, SV.ContentHorizontalOffset / Zoom.Value);
            HRect.SetValue(Canvas.TopProperty, SV.ContentVerticalOffset / Zoom.Value);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point P = e.GetPosition(Canv);
                SV.ScrollToHorizontalOffset((P.X * Zoom.Value) - HRect.Width / 2);
                SV.ScrollToVerticalOffset((P.Y * Zoom.Value) - HRect.Height / 2);
            }
        }

        private void tbSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TextBox tb = sender as TextBox;
                var vm = this.DataContext as MediaListViewModel;
                vm.SearchText = tb.Text;
                vm.SearchImage(null);
            }
        }
        private void ListBoxScroll_TouchUp(object sender, TouchEventArgs e)
        {
            FrameworkElement scrollView = sender as FrameworkElement;
            if (scrollView == null)
                return;
            scrollView.ReleaseTouchCapture(e.TouchDevice);
            e.Handled = true;
        }
        private void ListBoxScroll_TouchDown(object sender, TouchEventArgs e)
        {
            FrameworkElement scrollView = sender as FrameworkElement;
            if (scrollView == null)
                return;
            scrollView.CaptureTouch(e.TouchDevice);
            e.Handled = true;
        }

        private void ListBoxScroll_Scroll(object sender, RoutedEventArgs e)
        {
            ScrollBar sb = e.OriginalSource as ScrollBar;
            if (sb.Orientation == Orientation.Horizontal)
                return;

            if (sb.Value == sb.Maximum  && !isExecuting)
            {
                isExecuting = true;
                var vm = this.DataContext as MediaListViewModel;
                vm.FetchNextPage();
            }
            isExecuting = false;
        }
        private void ListBoxScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                ListBoxScroll.LineDown();
            }
            else
            {
                ListBoxScroll.LineUp();
            }

            ScrollViewer sb1 = sender as ScrollViewer;
            if (sb1 != null)
            {
                var verticalOffset = sb1.VerticalOffset;
                var maxVerticalOffset = sb1.ScrollableHeight;
                if ((maxVerticalOffset < 0 ||
                    verticalOffset == maxVerticalOffset) && !isExecuting)
                {
                    isExecuting = true;
                    var vm = this.DataContext as MediaListViewModel;
                    vm.FetchNextPage();
                }
            }
            isExecuting = false;
        }
    }
}
