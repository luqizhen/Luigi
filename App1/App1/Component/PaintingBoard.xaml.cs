using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Component
{
    public sealed partial class PaintingBoard : UserControl
    {
        public PaintingBoard()
        {
            this.InitializeComponent();
        }

        private void drawBtn_Checked(object sender, RoutedEventArgs e)
        {
            eraseBtn.IsChecked = false;
        }

        private void eraseBtn_Checked(object sender, RoutedEventArgs e)
        {
            drawBtn.IsChecked = false;
        }
    }
}
