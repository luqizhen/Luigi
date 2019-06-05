using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mainUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Screen, Rectangle> _screenRectagle;
        private double _screenCanvasRatio;

        public MainWindow()
        {
            _screenCanvasRatio = 1;
            InitializeComponent();
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SwitchScreenToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_screenRectagle == null)
            {
                _screenRectagle = new Dictionary<Screen, Rectangle>();
            }
            GetScreensInfo();

            _screenRectagle.Values.ToList().ForEach((screen) => { ScreenCanvas.Children.Add(screen); });
        }

        private void GetScreensInfo()
        {
            double left = 0, top = 0, right = 0, bottom = 0;
            Screen.AllScreens.ToList().ForEach((screen) => {
                if (left > screen.Bounds.Left) left = screen.Bounds.Left;
                if (top > screen.Bounds.Top) top = screen.Bounds.Top;
                if (right < screen.Bounds.Right) right = screen.Bounds.Right;
                if (bottom < screen.Bounds.Bottom) bottom = screen.Bounds.Bottom;
            });

            double screenCanvasWRatio = (right - left) / ScreenCanvas.ActualWidth;
            double screenCanvasHRatio = (bottom - top) / ScreenCanvas.ActualHeight;
            _screenCanvasRatio = (screenCanvasWRatio > screenCanvasHRatio) ? screenCanvasWRatio : screenCanvasHRatio;

            Rectangle rect;
            foreach (var screen in Screen.AllScreens)
            {
                rect = new Rectangle
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    Fill = new SolidColorBrush(Colors.BurlyWood),
                    Width = screen.Bounds.Width / _screenCanvasRatio,
                    Height = screen.Bounds.Height / _screenCanvasRatio
                };
                Canvas.SetLeft(rect, screen.Bounds.X / _screenCanvasRatio);
                Canvas.SetTop(rect, screen.Bounds.Y / _screenCanvasRatio);
                _screenRectagle.Add(screen, rect);
            }
        }
    }
}
