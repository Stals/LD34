using UnityEngine;
using System;
using System.IO;
using UnityEditor;


[InitializeOnLoad]
internal class CurrentSceneProjectWindow
{
    static readonly string currentSceneText = "<|"; // ◀ 
    static readonly float rightOffset = 4f; 

    static CurrentSceneProjectWindow()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowGUI;
    }
    
    static void OnProjectWindowGUI(string pGUID, Rect pDrawingRect)
    {
        string assetpath = AssetDatabase.GUIDToAssetPath(pGUID);
        string extension = Path.GetExtension(assetpath);

        if (extension == ".unity")
        {
            if(assetpath == EditorApplication.currentScene)
            {
                GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
                Vector2 labelSize = labelStyle.CalcSize(new GUIContent(currentSceneText));
                
                Rect newRect = pDrawingRect;
                newRect.width += pDrawingRect.x;
                newRect.x = newRect.width - labelSize.x - rightOffset;
                
                Color prevGuiColor = GUI.color;
                GUI.Label(newRect, currentSceneText, labelStyle);
                GUI.color = prevGuiColor;
            }
        }
    }
}