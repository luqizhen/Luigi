using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SeamlessConnection
{
    /// <summary>
    /// this class contains all information about screens related locations, the resolutions and the directions
    /// </summary>
    public class ScreensLayoutInfo: Singleton
    {
        private readonly Point Zero;

        ScreensLayoutInfo() : base()
        {
            Zero = new Point(0, 0);
            Point temp;
            foreach (var screen in Screen.AllScreens)
            {
                temp = screen.Bounds.Location;
                Console.WriteLine(temp);
                temp = temp + screen.Bounds.Size;
                Console.WriteLine(temp);
            }
        }
    }
}