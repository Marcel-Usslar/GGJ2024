﻿using UnityEditor;
using UnityEngine;
using vietlabs.fr2;

[CustomEditor(typeof(FR2_Cache))]
internal class FR2_CacheEditor : UnityEditor.Editor
{
    private static string inspectGUID;
    private static int index;

    public override void OnInspectorGUI()
    {
	    var c = (FR2_Cache) target;
	    
	    GUILayout.Label("Total : " + c.AssetList.Count);
	    
		c.priority = EditorGUILayout.IntSlider("Priority", c.priority, 0, 5);	
	    
	    
        var s = Selection.activeObject;
        if (s == null) return;

        var guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(s));

        if (inspectGUID != guid)
        {
            inspectGUID = guid;
            index = c.AssetList.FindIndex(item => item.guid == guid);
        }

        if (index != -1)
        {
            if (index >= c.AssetList.Count) index = 0;

            serializedObject.Update();
            var prop = serializedObject.FindProperty("AssetList").GetArrayElementAtIndex(index);
            EditorGUILayout.PropertyField(prop, true);
        }
    }
}