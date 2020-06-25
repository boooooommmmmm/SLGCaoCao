using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SeparateAnimationFromFBX
{
    [MenuItem("GameObject/Tools/SeparateAnimations &1")]
    static void CreateAnim()
    {
        Object[] selectionAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered);
        
        foreach (Object asset in selectionAsset)
        {
            Debug.Log("asset name: " + asset.name);

            string assetName = selectionAsset[0].name;
            string targetPath = Application.dataPath + "Art/Animations/" + assetName;
            if (!Directory.Exists(targetPath))
            {
                Debug.Log("not exists");
                Directory.CreateDirectory(targetPath);
            }

            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(asset, newClip);
            AssetDatabase.CreateAsset(newClip, targetPath + "/" + asset.name + ".anim");

            Debug.Log(string.Format("Generate animations success! {0}", assetName));
        }
        AssetDatabase.Refresh();        
    }
}
