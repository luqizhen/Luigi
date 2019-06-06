using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace luigi.utilities.Simulator
{
    public class Screenshot
    {
        /// <summary>
        /// Used to capture and save the screenshot, NOTICE that it can only be called by a STA thread.
        /// </summary>
        /// <param name="outPath"></param>
        internal static void SaveAsPNG(string outPath)
        {
            KeyboardSimulator.PressScreenshot();
            Thread.Sleep(1000);
            if (!Clipboard.ContainsImage())
            {
                return;
            }
            int width = (int)(SystemParameters.WorkArea.Width);
            int height = (int)(SystemParameters.WorkArea.Height);
            using (MemoryStream ms = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));
                enc.Save(ms);
                Bitmap img = null;
                try
                {
                    img = (new Bitmap(ms));
                    img.Save(outPath, ImageFormat.Png);
                }
                finally
                {
                    if (null != img)
                        img.Dispose();
                }
            }
        }

        public static bool CaptureScreen(string FileName)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(FileName))
            {
                SaveAsPNG(FileName);
                ret = true;
            }
            return ret;
        }

        public static bool CaptureScreen(string ID, string language = "en")
        {
            if (!Directory.Exists("screenshots"))
            {
                Directory.CreateDirectory("screenshots");
            }
            bool ret = false;
            SaveAsPNG(Path.Combine("screenshots", language + "_" + ID + ".png"));
            ret = true;
            return ret;
        }

        public static void CaptureScreenByRect(Rect rect, string outPath)
        {
            try
            {
                Bitmap bitmap = new Bitmap(Convert.ToInt32(rect.Width), Convert.ToInt32(rect.Height));
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(Convert.ToInt32(rect.Left), Convert.ToInt32(rect.Top), 0, 0, new System.Drawing.Size(Convert.ToInt32(rect.Width), Convert.ToInt32(rect.Height)));
                    bitmap.Save(outPath, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
