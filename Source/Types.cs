using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Sources.Utils
{
    public enum DIR
    {
        LEFT,
        RIGHT,
    }
    public enum CHARTYPE
    {
       NONE,
       BOY,
       GIRL,
    }
    public enum NPCTYPE
    {
        NPC01,
        NPC02,
        NPC03,
    }
    public struct Pos
    {
        public int x;
        public int y;
    }
}
