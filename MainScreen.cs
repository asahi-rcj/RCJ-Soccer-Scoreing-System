using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amaoto;
using RCJ_Soccer_Scoring_System;
using static DxLibDLL.DX;

namespace RCJJ_Soccer_Scoring_System
{
    internal class MainScreen
    {
        private bool IsFirstHalf = true;
        private bool IsGameStarting = false;

        private int GameNameIndex;

        //Blue / Yellow
        private int[] TeamPoint = new int[] { 0, 0 };
        private int[] TeamNameIndex = new int[] { 0, 0 };

        private Counter ctGameTimer;
        private DateTime dt;

        private Counter[] ctRoboOutCounter = new Counter[4];

        private bool IsSettingMode = false;

        public void Enable()
        {
            ctGameTimer = new Counter(0, Program.config.GameTime, 1000000, true);

            for(int i = 0; i < 4; i++)
            {
                if(Program.config.Original_OutTime)
                {
                    ctRoboOutCounter[i] = new Counter(0, Program.config.Robot_OutTime, 1000000, false);
                }
                else
                {
                    ctRoboOutCounter[i] = new Counter(0, Program.config.GameTime / 10, 1000000, false);
                }
            }
        }

        public void Draw()
        {
            #region [ Main Board ]

            // Main Background
            DrawBox(0, 52, 1346, 1028, GetColor(255, 255, 255), TRUE);

            #region [ Banner ]

            Program.rl.Banner.Draw(0, 52);

            #endregion

            #region [ Game Name ]

            DrawBox(387, 57, 957, 213, GetColor(80, 80, 80), TRUE);
            {
                string text = Program.config.Game_Name[this.GameNameIndex];
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font3);
                DrawStringToHandle(387 + 570 / 2 - width / 2, 95, text, GetColor(225, 220, 20), System_Font3);

                if (Program.Mouse.IsExistInArea(new Point(387, 57), new Point(957, 213)) && !IsSettingMode)
                {
                    DrawBoxO(387, 57, 957, 213, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        this.GameNameIndex = (this.GameNameIndex + 1) % Program.config.Game_Name.Length;
                    }
                }
            }

            #endregion

            #region [ Now Time ]

            DrawBox(963, 57, 1341, 213, GetColor(0, 0, 0), TRUE);

            DrawStringToHandle(1015, 95, dt.ToString("hh:mm:ss"), GetColor(255, 255, 0), System_Font);

            #endregion

            #region [ Team Name : Blue Side ]

            DrawBox(3, 220, 670, 375, GetColor(0, 100, 255), TRUE);

            {
                int team_type = IsFirstHalf == true ? 0 : 1;

                int width = GetDrawStringWidthToHandle(Program.config.Team_Name[TeamNameIndex[team_type]], (int)strlenDx(Program.config.Team_Name[TeamNameIndex[team_type]]), System_Font3);
                DrawStringToHandle(667 / 2 - width / 2, 257, Program.config.Team_Name[TeamNameIndex[team_type]], GetColor(255, 255, 255), System_Font3);

                if (Program.Mouse.IsExistInArea(new Point(3, 220), new Point(670, 375)) && !IsSettingMode)
                {
                    DrawBoxO(3, 220, 670, 375, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        this.TeamNameIndex[team_type] = (this.TeamNameIndex[team_type] + 1) % Program.config.Team_Name.Length;
                    }
                }
            }

            #endregion

            #region [ Team Name : Yellow Side ]

            DrawBox(676, 220, 1342, 375, GetColor(255, 200, 0), TRUE);

            {
                int team_type = IsFirstHalf == true ? 1 : 0;

                int width = GetDrawStringWidthToHandle(Program.config.Team_Name[TeamNameIndex[team_type]], (int)strlenDx(Program.config.Team_Name[TeamNameIndex[team_type]]), System_Font3);
                DrawStringToHandle(676 + 667 / 2 - width / 2, 257, Program.config.Team_Name[TeamNameIndex[team_type]], GetColor(0, 0, 0), System_Font3);

                if (Program.Mouse.IsExistInArea(new Point(676, 220), new Point(1342, 375)) && !IsSettingMode)
                {
                    DrawBoxO(676, 220, 1342, 375, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        this.TeamNameIndex[team_type] = (this.TeamNameIndex[team_type] + 1) % Program.config.Team_Name.Length;
                    }
                }
            }

            #endregion

            #region [ Team Points : Blue Side ]

            DrawBox(4, 381, 477, 916, GetColor(50, 50, 50), TRUE);

            {
                int team_type = IsFirstHalf == true ? 0 : 1;

                DrawStringToHandle(36, 480, this.TeamPoint[team_type] < 10 ? "0" + this.TeamPoint[team_type].ToString() : this.TeamPoint[team_type].ToString(), GetColor(255, 255, 255), System_Font4);

                if (Program.Mouse.IsExistInArea(new Point(4, 381), new Point(477, 916)) && !IsSettingMode)
                {
                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        if (this.TeamPoint[team_type] + 1 <= 99)
                            this.TeamPoint[team_type]++;

                        for (int i = 0; i < 4; i++)
                        {
                            this.ctRoboOutCounter[i].Stop();
                            this.ctRoboOutCounter[i].Value = 0;
                        }
                    }
                }
            }

            #endregion

            #region [ Team Points : Yellow Side ]

            DrawBox(868, 381, 1342, 916, GetColor(50, 50, 50), TRUE);

            {
                int team_type = IsFirstHalf == true ? 1 : 0;

                DrawStringToHandle(867 + 36, 480, this.TeamPoint[team_type] < 10 ? "0" + this.TeamPoint[team_type].ToString() : this.TeamPoint[team_type].ToString(), GetColor(255, 255, 255), System_Font4);

                if (Program.Mouse.IsExistInArea(new Point(868, 381), new Point(1342, 916)) && !IsSettingMode)
                {
                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        if (this.TeamPoint[team_type] + 1 <= 99)
                            this.TeamPoint[team_type]++;

                        for (int i = 0; i < 4; i++)
                        {
                            this.ctRoboOutCounter[i].Stop();
                            this.ctRoboOutCounter[i].Value = 0;
                        }
                    }
                }
            }

            #endregion

            #region [ 1st Half / 2nd Half ]

            DrawBox(484, 381, 861, 538, GetColor(50, 100, 50), TRUE);

            DrawStringToHandle(556, 405, IsFirstHalf ? "前半" : "後半", GetColor(255, 255, 255), System_Font2);

            if (Program.Mouse.IsExistInArea(new Point(484, 381), new Point(861, 538)) && !IsSettingMode)
            {
                DrawBoxO(484, 381, 861, 538, GetColor(0, 0, 0), TRUE, 128);

                if (Program.Mouse.IsPushedButton(MouseButton.Left))
                {
                    this.IsFirstHalf = !this.IsFirstHalf;
                }
            }

            #endregion

            #region [ Game Time ]

            DrawBox(484, 543, 861, 807, GetColor(0, 0, 0), TRUE);

            {
                int Minutes = (Program.config.GameTime - this.ctGameTimer.Value) / 60;
                int Second = (Program.config.GameTime - this.ctGameTimer.Value) % 60;

                string strM = Minutes < 10 ? "0" + Minutes.ToString() : Minutes.ToString();
                string strS = Second < 10 ? "0" + Second.ToString() : Second.ToString();

                string text = strM + ":" + strS;
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font6);

                DrawStringToHandle(484 + 190 - width / 2, 605, text, GetColor(255, 255, 0), System_Font6);

                if (Program.Mouse.IsExistInArea(new Point(484, 543), new Point(861, 807)) && !IsSettingMode)
                {
                    DrawBoxO(484, 543, 861, 807, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        IsSettingMode = !IsSettingMode;
                    }
                }
            }

            #endregion

            #region [ Start / Stop ]

            DrawBox(484, 813, 670, 916, GetColor(255, 0, 0), TRUE);

            {
                string text = IsGameStarting ? "Stop" : "Start";
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font5);

                DrawStringToHandle(484 + 93 - width / 2, 833, text, GetColor(255, 255, 255), System_Font5);

                if (Program.Mouse.IsExistInArea(new Point(484, 813), new Point(670, 916)) && !IsSettingMode)
                {
                    DrawBoxO(484, 813, 670, 916, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        this.IsGameStarting = !this.IsGameStarting;

                        if (IsGameStarting)
                        {
                            this.ctGameTimer.Start();
                        }
                        else
                        {
                            this.ctGameTimer.Stop();
                        }
                    }
                }
            }

            #endregion

            #region [ Reset ]

            DrawBox(675, 814, 861, 916, GetColor(0, 0, 0), TRUE);

            {
                string text = "Reset";
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font5);

                DrawStringToHandle(675 + 93 - width / 2, 833, text, GetColor(255, 255, 255), System_Font5);

                if (IsGameStarting)
                {
                    DrawBoxO(675, 814, 861, 916, GetColor(0, 0, 0), TRUE, 196);
                }
                else
                {
                    if (Program.Mouse.IsExistInArea(new Point(675, 814), new Point(861, 916)) && !IsSettingMode)
                    {
                        DrawBoxO(675, 814, 861, 916, GetColor(0, 0, 0), TRUE, 128);

                        if (Program.Mouse.IsPushedButton(MouseButton.Left))
                        {
                            this.ctGameTimer.Value = this.ctGameTimer.Begin;
                        }
                    }
                }
            }

            #endregion

            #region [ Next Game ]

            DrawBox(483, 921, 861, 1024, GetColor(0, 0, 0), TRUE);
            {
                string text = "Next Game";
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font5);

                DrawStringToHandle(483 + 190 - width / 2, 950, text, GetColor(255, 255, 255), System_Font5);

                if (IsGameStarting)
                {
                    DrawBoxO(483, 921, 861, 1024, GetColor(0, 0, 0), TRUE, 196);
                }
                else
                {
                    if (Program.Mouse.IsExistInArea(new Point(483, 921), new Point(861, 1024)) && !IsSettingMode)
                    {
                        DrawBoxO(483, 921, 861, 1024, GetColor(0, 0, 0), TRUE, 128);

                        if (Program.Mouse.IsPushedButton(MouseButton.Left))
                        {
                            this.IsGameStarting = false;

                            this.ctGameTimer.Stop();
                            this.ctGameTimer.Value = this.ctGameTimer.Begin;

                            this.IsFirstHalf = true;

                            this.TeamPoint[0] = 0;
                            this.TeamPoint[1] = 0;

                            for (int i = 0; i < 4; i++)
                            {
                                this.ctRoboOutCounter[i].Stop();
                                this.ctRoboOutCounter[i].Value = 0;
                            }
                        }
                    }
                }
            }

            #endregion

            #region [ Point Remove : Blue Side ]

            DrawBox(4, 921, 477, 1024, GetColor(120, 120, 120), TRUE);
            {
                int team_type = IsFirstHalf == true ? 0 : 1;

                string text = "＜＜＜";
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font7);

                DrawStringToHandle(4 + 238 - width / 2, 955, text, GetColor(255, 255, 255), System_Font7);

                if (Program.Mouse.IsExistInArea(new Point(4, 921), new Point(477, 1024)) && !IsSettingMode)
                {
                    DrawBoxO(4, 921, 477, 1024, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        if (this.TeamPoint[team_type] - 1 >= 0)
                            this.TeamPoint[team_type]--;
                    }
                }
            }

            #endregion

            #region [ Point Remove : Yellow Side ]

            DrawBox(868, 921, 1342, 1024, GetColor(120, 120, 120), TRUE);
            {
                int team_type = IsFirstHalf == true ? 1 : 0;

                string text = "＜＜＜";
                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font7);

                DrawStringToHandle(867 + 238 - width / 2, 955, text, GetColor(255, 255, 255), System_Font7);

                if (Program.Mouse.IsExistInArea(new Point(868, 921), new Point(1342, 1024)) && !IsSettingMode)
                {
                    DrawBoxO(868, 921, 1342, 1024, GetColor(0, 0, 0), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        if (this.TeamPoint[team_type] - 1 >= 0)
                            this.TeamPoint[team_type]--;
                    }
                }
            }

            #endregion

            #endregion

            #region [ Robot OutTimer Board ]

            //first robo

            //Background
            DrawBox(1438, 52, 1919, 218, GetColor(255, 255, 255), TRUE);

            //Number
            DrawBox(1443, 52 + 5, 1629, 218 - 5, GetColor(200, 50, 50), TRUE);

            //Timer
            DrawBox(1635, 52 + 5, 1917, 218 - 5, GetColor(0, 0, 0), TRUE);

            //second robo

            //Background
            DrawBox(1438, 268, 1919, 434, GetColor(255, 255, 255), TRUE);

            //Number
            DrawBox(1443, 268 + 5, 1629, 434 - 5, GetColor(50, 100, 50), TRUE);

            //Timer
            DrawBox(1635, 268 + 5, 1917, 434 - 5, GetColor(0, 0, 0), TRUE);

            //third robo

            //Background
            DrawBox(1438, 484, 1919, 650, GetColor(255, 255, 255), TRUE);

            //Number
            DrawBox(1443, 484 + 5, 1629, 650 - 5, GetColor(50, 50, 200), TRUE);

            //Timer
            DrawBox(1635, 484 + 5, 1917, 650 - 5, GetColor(0, 0, 0), TRUE);

            //forth robo

            //Background
            DrawBox(1438, 700, 1919, 866, GetColor(255, 255, 255), TRUE);

            //Number
            DrawBox(1443, 700 + 5, 1629, 866 - 5, GetColor(200, 50, 200), TRUE);

            //Timer
            DrawBox(1635, 700 + 5, 1917, 866 - 5, GetColor(0, 0, 0), TRUE);

            //Timer Reset

            //Background
            DrawBox(1438, 916, 1919, 1028, GetColor(255, 255, 255), TRUE);

            //Button Background
            DrawBox(1443, 921, 1917, 1023, GetColor(120, 120, 120), TRUE);
            {
                string text = "Reset All P-Timers";

                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font7);

                DrawStringToHandle(1443 + 474 / 2 - width / 2, 948, text, GetColor(255, 255, 255), System_Font7);

                if (Program.Mouse.IsExistInArea(new Point(1443, 921), new Point(1917, 1023)) && !IsSettingMode)
                {
                    DrawBoxO(1443, 921, 1917, 1023, GetColor(255, 255, 255), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        for(int i = 0; i < 4; i++)
                        {
                            this.ctRoboOutCounter[i].Stop();
                            this.ctRoboOutCounter[i].Value = 0;
                        }
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int value = (this.ctRoboOutCounter[i].End - this.ctRoboOutCounter[i].Value);
                string text = value < 10 ? "0" + value.ToString() : value.ToString();

                int width = GetDrawStringWidthToHandle(text, (int)strlenDx(text), System_Font8);

                DrawStringToHandle(1636 + 282 / 2 - width / 2, 86 + i * 216, text, GetColor(255, 255, 255), System_Font8);

                DrawStringToHandle(1502, 86 + i * 216, (i + 1).ToString(), GetColor(255, 255, 255), System_Font8);

                if (this.ctRoboOutCounter[i].State == TimerState.Stopped)
                {
                    DrawBoxO(1635, 57 + i * 216, 1919, 213 + i * 216, GetColor(0, 0, 0), TRUE, 128);
                }

                if (Program.Mouse.IsExistInArea(new Point(1438, 52 + i * 216), new Point(1919, 218 + i * 216)) && !IsSettingMode)
                {                    
                    DrawBoxO(1438, 52 + i * 216, 1919, 218 + i * 216, GetColor(255, 255, 255), TRUE, 128);

                    if (Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        if (this.ctRoboOutCounter[i].State == TimerState.Stopped)
                        {
                            this.ctRoboOutCounter[i].Start();
                        }
                        else
                        {
                            this.ctRoboOutCounter[i].Stop();
                            this.ctRoboOutCounter[i].Value = 0;
                        }
                    }
                }
            }

            #endregion

            if (IsSettingMode)
            {
                #region [ Setting Mode ]

                DrawBoxO(0, 0, 1920, 1080, GetColor(0, 0, 0), TRUE, 192);

                DrawBox(1920 - 550, 0, 1920, 1080, GetColor(255, 255, 255), TRUE);

                #region [ Close Button ]

                DrawRoundRect(50, 50, 150, 150, 10, 10, GetColor(255, 255, 255), TRUE);

                DrawStringToHandle(72, 28, "x", GetColor(0, 0, 0), System_Font2);

                if(Program.Mouse.IsExistInArea(new Point(50, 50), new Point(150, 150)) && IsSettingMode)
                {
                    DrawRoundRectO(50, 50, 150, 150, 10, 10, GetColor(0, 0, 0), TRUE, 128);

                    if(Program.Mouse.IsPushedButton(MouseButton.Left))
                    {
                        IsSettingMode = false;
                    }
                }

                #endregion 

                #endregion
            }
        }

        public void Update()
        {
            ctGameTimer.Tick();
            ctRoboOutCounter[0].Tick();
            ctRoboOutCounter[1].Tick();
            ctRoboOutCounter[2].Tick();
            ctRoboOutCounter[3].Tick();

            dt = DateTime.Now;
        }

        private void DrawBoxO(int x1, int y1, int x2, int y2, uint color, int fillflag, int opacity)
        {
            SetDrawBlendMode(DX_BLENDMODE_ALPHA, opacity);
            DrawBox(x1, y1, x2, y2, color, fillflag);
            SetDrawBlendMode(DX_BLENDMODE_ALPHA, 255);
        }

        private void DrawRoundRectO(int x1, int y1, int x2, int y2, int rx, int ry, uint color, int fillflag, int opacity)
        {
            SetDrawBlendMode(DX_BLENDMODE_ALPHA, opacity);
            DrawRoundRect(x1, y1, x2, y2, rx, ry, color, fillflag);
            SetDrawBlendMode(DX_BLENDMODE_ALPHA, 255);
        }

        private int System_Font = CreateFontToHandle("MS UI Gothic", 80, 5, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font2 = CreateFontToHandle("MS UI Gothic", 120, 5, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font3 = CreateFontToHandle("MS UI Gothic", 80, 5, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font4 = CreateFontToHandle("MS UI Gothic", 400, 5, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font5 = CreateFontToHandle("MS UI Gothic", 70, 5, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font6 = CreateFontToHandle("MS UI Gothic", 160, 2, DX_FONTTYPE_ANTIALIASING_16X16);
        private int System_Font7 = CreateFontToHandle("MS UI Gothic", 50, 10, DX_FONTTYPE_ANTIALIASING_16X16);

        private int System_Font8 = CreateFontToHandle("MS UI Gothic", 120, 5, DX_FONTTYPE_ANTIALIASING_16X16);
    }
}
