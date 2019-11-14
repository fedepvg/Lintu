using UnityEngine;
using UnityEditor;

public class PrefabReplace : ScriptableObject
{
    [MenuItem("Helper/Replace Objects With Prefab")]
    static void HelperReplaceObjectsWithPrefab()
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Viga.prefab"); // put the location of the prefab here

        // go over all selected objects. Change this if you want to go
        // through other objects, e.g. GameObject.FindWithTag ...
        foreach (GameObject s in Selection.objects)
        {
            // create a new prefab
            var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            go.name = s.name; // remember name
            EditorUtility.CopySerialized(s.transform, go.transform); // remember position/rotation etc..
            go.transform.parent = s.transform.parent; // put into hierarchy
            PrefabUtility.RecordPrefabInstancePropertyModifications(go); // fix modified properties
            GameObject.DestroyImmediate(s); // destroy old
        }
    }
}