using UnityEngine;
using UnityEditor;
using System.Collections;

public class CurrentSceneFinder : MonoBehaviour {

    [MenuItem("Tools/Find Current Scene")]
    private static void FindCurrentScene()
    {
        Object sceneObj = AssetDatabase.LoadAssetAtPath(EditorApplication.currentScene, typeof(Object));
        if (sceneObj != null)
        {
            EditorGUIUtility.PingObject( sceneObj );
        }
    }
}
