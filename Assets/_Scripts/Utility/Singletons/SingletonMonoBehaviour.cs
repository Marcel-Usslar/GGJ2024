using UnityEngine;

namespace Utility.Singletons
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindOrCreateInstance();

                return _instance;
            }
        }

        protected virtual void OnInitialize()
        { }

        private void Awake()
        {
            if (_instance == this)
                return;

            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            Setup(_instance);
        }

        private static T FindOrCreateInstance()
        {
            var sceneObject = FindObjectOfType<T>();
            if (sceneObject != null)
            {
                Setup(sceneObject);
                return sceneObject;
            }

            var instance = new GameObject(typeof(T).Name);
            var component = instance.AddComponent<T>();
            Setup(component);

            return component;
        }

        private static void Setup(T component)
        {
            DontDestroyOnLoad(component.gameObject);
            component.OnInitialize();
        }
    }
}