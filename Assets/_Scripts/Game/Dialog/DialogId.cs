using System;
using Game.Speaker;

namespace Game.Dialog
{
    [Serializable]
    public struct DialogId
    {
        public SpeakerType Speaker;
        public int State;

        public DialogId(SpeakerType speaker, int state)
        {
            Speaker = speaker;
            State = state;
        }

        public static bool operator ==(DialogId obj1, DialogId obj2)
        {
            return obj1.Speaker == obj2.Speaker && obj1.State == obj2.State;
        }

        public static bool operator !=(DialogId obj1, DialogId obj2)
        {
            return !(obj1 == obj2);
        }
    }
}