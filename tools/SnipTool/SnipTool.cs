using luigi.utilities;
using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace luigi.tools
{
    public partial class SnipTool
    {
        static void Main(string[] args)
        {
            Thread.Sleep(3000); // delay 3 seconds to start the tool.
            bool shouldLoop = false;
            Action<string> action = null;
            if (LoadActions(ref shouldLoop, ref action))
            {
                if (shouldLoop)
                {
                    foreach (var language in Languages)
                    {
                        action?.Invoke(language);
                    }
                }
                else
                {
                    action?.Invoke(string.Empty);
                }
            }
            else
            {
                string fileName = string.Empty;

                foreach (var arg in args)
                {
                    fileName += arg + "_";
                }

                fileName += DateTime.Now.Ticks + ".png";
                SnipUtils.CaptureScreen(fileName);
            }
        }

        private static bool LoadActions(ref bool shouldLoop, ref Action<string> action)
        {
            if (!File.Exists(actionFileName))
            {
                return false;
            }

            XDocument xDoc = XDocument.Load(actionFileName);
            bool.TryParse(xDoc.Element("snipActions").Element("repeat").Value, out shouldLoop);
            action = null;
            foreach (var step in xDoc.Descendants("step"))
            {
                int time = 0;
                bool needWait = false;
                if (step.HasAttributes)
                {
                    needWait = int.TryParse(step.Attribute("time").Value, out time);
                    if (!needWait)
                    {
                        return false;
                    }
                }
                var stepStr = step.Value.ToUpper().Split(' ');
                Action<string> innerAction = null;
                switch (stepStr[0])
                {
                    case "PRESS":
                        KeyboardUtils.Key key;
                        if(Enum.TryParse<KeyboardUtils.Key>(stepStr[1].ToUpper(), out key))
                        {
                            innerAction = (str) =>
                            {
                                KeyboardUtils.Press(key);
                                Thread.Sleep(300);
                            };
                        }
                        break;
                    case "SNIP":
                        innerAction = (str) =>
                        {
                            string fileName = (string.IsNullOrWhiteSpace(str) ? "" : str + "_") + stepStr[1] + ".png";
                            SnipUtils.CaptureScreen(fileName);
                            Thread.Sleep(300);
                        };
                        break;
                    case "WAIT":
                        int wait;
                        if(int.TryParse(stepStr[1], out wait))
                        {
                            innerAction = (str) =>
                            {
                                Thread.Sleep(wait);
                            };
                        }
                        break;
                    default:
                        return false;
                }
                if (needWait)
                {
                    action += (str) =>
                    {
                        Thread td = new Thread(() => Thread.Sleep(time));
                        td.Start();
                        innerAction?.Invoke(str);
                        td.Join();
                    };
                }
                else
                {
                    action += innerAction;
                }
            }
            return true;
        }
    }
}
