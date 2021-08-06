using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberHighlight
{
    public class Settings
    {
        public bool Enabled = true;
        public bool ShowSummaryOnExit = true;

        public bool SavePass = true;
        public bool SaveFail = true;
        public bool SaveExit = false;

        public int OffsetStart = 0;
        public int OffsetEnd = 0;

        public bool HideMouse = true;
    }
}
