using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using TinyJson;
using System.Collections.Generic;

namespace ServoStiker
{
    public class ServoStiker : IGameLaunchingPlugin
    {
        string pathToJoyToTray;
        string pathToConfig;
        string defaultMode;
        string[] eightNames;
        string[] fourNames;
        string resetOnExit;
        public ServoStiker()
        {
            this.pathToConfig = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ServoStiker.conf";
            string configFile = System.IO.File.ReadAllText(pathToConfig);
            var configValues = configFile.FromJson<object>();
            var def = ((Dictionary<string, object>)configValues);
            this.pathToJoyToTray = def["joytrayPath"].ToString();
            this.defaultMode = def["default"].ToString();
            char[] spliter = { ',', ';' };
            this.eightNames = def["8-way-names"].ToString().Split(spliter);
            this.fourNames = def["4-way-names"].ToString().Split(spliter);
            this.resetOnExit = def["reset-on-exit"].ToString();
        }
        public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
        }

        public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            System.Collections.Generic.KeyValuePair<IGameController, int?>[] controllerSupport = game.GetControllerSupport();
            string strCmdText;
            strCmdText = "";
            int fourway;
            int eightway;
            int check;
            if (controllerSupport.Length > 0)
            {
                System.Collections.Generic.KeyValuePair<IGameController, int?> kvp = controllerSupport[0];
                fourway = System.Array.IndexOf(this.fourNames, kvp.Key.Name);
                eightway = System.Array.IndexOf(this.eightNames, kvp.Key.Name);
                check = System.Array.IndexOf(this.eightNames, "Double 8-way Joysticks");
                if (fourway > -1)
                {
                    strCmdText = "-servo joy4way";
                }
                else if (eightway >-1)
                {
                    strCmdText = "-servo joy8way";
                }
                else
                {
                    if (this.defaultMode == "4-way")
                    {
                        strCmdText = "-servo joy4way";
                    }
                    else if (this.defaultMode == "8-way")
                    {
                        strCmdText = "-servo joy8way";
                    }
                }
            }
            System.Diagnostics.Process.Start(this.pathToJoyToTray, strCmdText);
        }

        public void OnGameExited()
        {
            if (this.resetOnExit == "True")
            {
                string strCmdText = "";
                if (this.defaultMode == "8-way")
                {
                    strCmdText = "-servo joy8way";
                }
                else if (this.defaultMode == "4-way")
                {
                    strCmdText = "-servo joy4way";
                }
                System.Diagnostics.Process.Start(this.pathToJoyToTray, strCmdText);
            }
        }


    }


}
