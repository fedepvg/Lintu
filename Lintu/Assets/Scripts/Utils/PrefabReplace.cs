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
        int i = 0;
        foreach (GameObject s in Selection.objects)
        {
            // create a new prefab
            GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            go.name = "Viga_" + i; // remember name
            go.transform.parent = s.transform.parent; // put into hierarchy
            EditorUtility.CopySerialized(s.transform, go.transform); // remember position/rotation etc..
            PrefabUtility.RecordPrefabInstancePropertyModifications(go); // fix modified properties
            GameObject.DestroyImmediate(s); // destroy old
            i++;
        }
    }
}