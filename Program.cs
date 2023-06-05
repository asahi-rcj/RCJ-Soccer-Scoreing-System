using Amaoto;
using RCJJ_Soccer_Scoring_System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DxLibDLL.DX;

namespace RCJ_Soccer_Scoring_System
{
    internal class Program
    {
        public static void Main()
        {
            #region [ Initializing DxLib ]

            SetUseTransColor(FALSE);
            SetWindowText("RCJ Soccer Scoring System v1.0.0");
            SetGraphMode(1920, 1080, 32);
            SetWindowSize(1280, 720);
            ChangeWindowMode(TRUE);
            unsafe
            {
                SetUseASyncChangeWindowModeFunction(TRUE, null, null);
                SetChangeScreenModeGraphicsSystemResetFlag(FALSE);
            }
            SetAlwaysRunFlag(TRUE);
            SetWaitVSyncFlag(FALSE);
            SetOutApplicationLogValidFlag(FALSE);
            SetUseTransColor(FALSE);
            if (DxLib_Init() == -1) return;
            SetDrawScreen(DX_SCREEN_BACK);
            SetMouseDispFlag(TRUE);

            #endregion

            string ExeDirPath = Directory.GetCurrentDirectory();
            config = ConfigManager.GetConfig<Config.GameConfig>(ExeDirPath + @"/Config.json");

            Input = new Input();
            Mouse = new Mouse();

            rl = new ResourceLoader();

            rl.LoadTexture();
            rl.LoadSound();

            ms = new MainScreen();

            ms.Enable();

            while (ProcessMessage() == 0)
            {
                ClearDrawScreen();

                Input.Update();
                Mouse.Update();

                ms.Draw();
                ms.Update();

                if(Input.IsPushedKey(KEY_INPUT_F1))
                {
                    if(IsWindowMode)
                    {
                        ChangeWindowMode(FALSE);
                        IsWindowMode = false;
                        SetMouseDispFlag(TRUE);
                    }
                    else
                    {
                        ChangeWindowMode(TRUE);
                        IsWindowMode = true;
                        SetMouseDispFlag(TRUE);
                    }
                }

                ScreenFlip();
            }
        }

        public static bool IsWindowMode = true;

        public static MainScreen ms;
        public static Config.GameConfig config;

        public static ResourceLoader rl;

        public static Input Input;
        public static Mouse Mouse;
    }
}
