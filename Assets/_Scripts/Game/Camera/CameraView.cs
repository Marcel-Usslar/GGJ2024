using UnityEngine;
using Utility.Singletons;

namespace Game.Camera
{
    public class CameraView : SingletonMonoBehaviour<CameraView>
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;
    }
}