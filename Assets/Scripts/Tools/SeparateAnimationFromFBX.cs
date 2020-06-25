using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SeparateAnimationFromFBX : Editor
{
    [MenuItem("AnimationClip/GetFilteredtoAnim &1", true)]
    static void CreateAnim()
    {
        string targetPath = Application.dataPath + "/AnimationClip";
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        Object[] SelectionAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);
        Debug.Log(SelectionAsset.Length);
        foreach (Object Asset in SelectionAsset)
        {
            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(Asset, newClip);
            AssetDatabase.CreateAsset(newClip, "Assets/AnimationClip/" + Asset.name + ".anim");
        }
        AssetDatabase.Refresh();
    }
}
