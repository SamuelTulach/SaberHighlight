using NVIDIA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberHighlight
{
    internal class Highlight
    {
        private static DateTime _startTime;
        private static string _lastName;
        
        public static void LogCallback(Highlights.ReturnCode ret, int id)
        {
            Plugin.Log.Info($"Callback from NVIDIA SDK with code {ret} for ID {id}.");
        }

        public static void StartRecording(string name)
        {
            _startTime = DateTime.Now;
            _lastName = name;
        }

        public static void SaveRecording()
        {
            TimeSpan difference = DateTime.Now - _startTime;

            Highlights.VideoHighlightParams vhp = new Highlights.VideoHighlightParams();
            vhp.highlightId = "MAP_PLAY";
            vhp.groupId = "MAP_PLAY_GROUP";
            vhp.startDelta = (int)-difference.TotalMilliseconds;
            vhp.endDelta = 2000;

            Highlights.SetVideoHighlight(vhp, LogCallback);
        }

        public static void ShowSummary()
        {
            Highlights.GroupView[] groupViews = new Highlights.GroupView[1];
            Highlights.GroupView gv1 = new Highlights.GroupView();
            gv1.GroupId = "MAP_PLAY_GROUP";
            gv1.SignificanceFilter = Highlights.HighlightSignificance.Good;
            gv1.TagFilter = Highlights.HighlightType.Achievement;
            groupViews[0] = gv1;

            Highlights.OpenSummary(groupViews, LogCallback);
        }
    }
}
