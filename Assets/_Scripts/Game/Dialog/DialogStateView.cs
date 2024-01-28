using UnityEngine;

namespace Game.Dialog
{
    public abstract class DialogStateView : MonoBehaviour
    {
        public abstract int State { get; }
    }
}