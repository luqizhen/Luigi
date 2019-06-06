using Simulator;
using System;
using System.IO;
using System.Threading;

namespace DoScreenshots
{
    class Program
    {
        static string[] Languages = new[]
        {
            "csCZ",
            "daDK",
            "elGR",
            "fiFI",
            "heIL",
            "huHU",
            "itIT",
            "koKR",
            "nlNL",
            "nbNO",
            "plPL",
            "roRO",
            "ruRU",
            "skSK",
            "slSI",
            "svSE",
            "trTR",
            "ukUA",
            "zhTW",
            "zhHK",
            "zhMO",
            "zhSG",
            "enUS",
            "deDE",
            "esES",
            "frFR",
            "zhCN",
            "jaJP",
            "ptBR",
            "ptPT",
            "arAE"
        };

        [STAThread]
        static void Main(string[] args)
        {
            PrepareAction();
            LoopAction(DoAction, Languages);
        }

        private static void PrepareAction()
        {
            Thread.Sleep(3000);
        }

        private static void DoAction(string language)
        {
            json
            KeyboardSimulator.Press(Key.TAB);
            KeyboardSimulator.Press(Key.TAB);
            KeyboardSimulator.Press(Key.ENTER);
            KeyboardSimulator.Press(Key.UP);
            KeyboardSimulator.Press(Key.ENTER);
            Thread.Sleep(2000);
            Screenshot.CaptureScreen("restart", language);
            Thread.Sleep(6000);
            Screenshot.CaptureScreen("shutdown", language);
            Thread.Sleep(6000);
            Screenshot.CaptureScreen("winre", language);
            Thread.Sleep(6000);
            KeyboardSimulator.Press(Key.UP);
            KeyboardSimulator.Press(Key.UP);
            KeyboardSimulator.Press(Key.DOWN);
            Thread.Sleep(2000);
        }

        private static void LoopAction(Action<string> action, string[] Languages)
        {
            foreach(var language in Languages)
            {
                action?.Invoke(language);
            }
        }
    }
}
