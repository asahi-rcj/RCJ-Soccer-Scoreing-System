using Amaoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCJJ_Soccer_Scoring_System
{
    internal class ResourceLoader
    {
        public void LoadTexture()
        {
            Banner = new Texture("banner.jpg");
        }

        public Texture Banner;

        public void LoadSound()
        {
            Game_End = new Sound("game_end.mp3");
            Time_Out = new Sound("time_out.mp3");
            Time_Notification = new Sound("time_notification.mp3");
        }

        public Sound Game_End;
        public Sound Time_Out;
        public Sound Time_Notification;
    }
}
