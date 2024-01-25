using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectFactory
{
    public static class ScriptableObjectFactory
    {
        [MenuItem("Assets/Create/ScriptableObject")]
        public static void CreateScriptableObjectFactoryWindow()
        {
            CreateScriptableObjectFactoryWindow(false);
        }

        public static void CreateScriptableObjectFactoryWindow(bool getAllAssemblies)
        {

            List<Type> allScriptableObjectTypes;

            try
            {
                var assemblies = getAllAssemblies ? GetAllAssemblies() : GetCSharpAssembly();

                allScriptableObjectTypes = new List<Type>(from assembly in assemblies
                    from type in GetTypesSafe(assembly)
                    where type.IsSubclassOf(typeof(ScriptableObject))
                    select type);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while opening scriptable object window: " + e.Message);
                allScriptableObjectTypes = new List<Type>();
            }

            ScriptableObjectWindow.Init(allScriptableObjectTypes.ToArray(), getAllAssemblies);
        }

        private static IEnumerable<Assembly> GetCSharpAssembly()
        {
            return new[] {Assembly.Load(new AssemblyName("Assembly-CSharp"))};
        }

        private static IEnumerable<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        private static Type[] GetTypesSafe(Assembly assembly)
        {
            var types = new Type[0];
            try
            {
                types = assembly.GetTypes();
            }
            catch (Exception e)
            {
                Debug.LogError($"Could not get types for assembly {assembly}: {e.Message}");
            }

            return types;
        }
    }
}