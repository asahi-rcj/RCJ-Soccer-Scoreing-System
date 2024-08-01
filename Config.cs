using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RCJJ_Soccer_Scoring_System
{
    internal class Config
    {
        public class GameConfig
        {
            /// <summary>
            /// 試合時間。
            /// </summary>
            public int GameTime = 5 * 60;

            /// <summary>
            /// ハーフタイム。
            /// </summary>
            public int HalfTime = 2 * 60;

            /// <summary>
            /// 指定したロボットのアウトオブバンズ時間を指定するかどうか。
            /// </summary>
            public bool Original_OutTime = true;

            /// <summary>
            /// ロボットのアウトオブバンズ時間。
            /// </summary>
            public int Robot_OutTime = 5 * 60 / 10;

            /// <summary>
            /// 事前に記入するチーム名。
            /// </summary>
            public string[] Team_Name = { "Test1", "Test2", "Test3" };

            /// <summary>
            /// 事前に記入する試合名。
            /// </summary>
            public string[] Game_Name = { "第1試合", "第2試合", "第3試合" };
        }
    }
}
