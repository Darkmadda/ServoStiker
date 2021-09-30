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
        string eightName;
        string fourName;
        string resetOnExit;
        public ServoStiker()
        {
            this.pathToConfig = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ServoStiker.conf";
            string configFile = System.IO.File.ReadAllText(pathToConfig);
            var configValues = configFile.FromJson<object>();
            var def = ((Dictionary<string, object>)configValues);
            this.pathToJoyToTray = def["joytrayPath"].ToString();
            this.defaultMode = def["default"].ToString();
            this.eightName = def["8-way-name"].ToString();
            this.fourName = def["4-way-name"].ToString();
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
            if (controllerSupport.Length > 0)
            {
                System.Collections.Generic.KeyValuePair<IGameController, int?> kvp = controllerSupport[0];
                if (kvp.Key.Name == this.fourName)
                {
                    strCmdText = "-servo joy4way";
                }
                else if (kvp.Key.Name == this.eightName)
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
