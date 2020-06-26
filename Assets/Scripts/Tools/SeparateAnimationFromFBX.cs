using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Animations;

public class SeparateAnimationFromFBX
{
    [MenuItem("GameObject/Tools/SeparateAnimations &1")]
    static void CreateAnim()
    {
        Object[] selectionAsset = Selection.GetFiltered(typeof(GameObject), SelectionMode.Unfiltered);

        foreach (Object asset in selectionAsset)
        {
            string assetName = asset.name;
            string targetPath = Application.dataPath + "/Art/Animations/" + assetName;
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            //cache animation clips
            var animationClipDir = new Dictionary<string, AnimationClip>();
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset.GetInstanceID()));
            foreach (Object obj in objects)
            {
                AnimationClip clip = obj as AnimationClip;
                if (clip != null && !clip.name.StartsWith("__preview"))
                    animationClipDir.Add(clip.name, clip);
            }

            //save
            foreach (AnimationClip ac in animationClipDir.Values)
            {
                AnimationClip newClip = new AnimationClip();
                EditorUtility.CopySerialized(ac, newClip);

                //newClip.frameRate = 60f;

                if (ac.name.Equals("run") || ac.name.Equals("victory") || ac.name.Equals("stand"))
                {
                    //newClip.wrapMode = WrapMode.Loop;

                    AnimationClipSettings _setting = AnimationUtility.GetAnimationClipSettings(ac);
                    _setting.loopTime = true;
                    AnimationUtility.SetAnimationClipSettings(ac, _setting);
                }
                else
                {
                    //newClip.wrapMode = WrapMode.Once;

                    AnimationClipSettings _setting = AnimationUtility.GetAnimationClipSettings(ac);
                    _setting.loopTime = false;
                    AnimationUtility.SetAnimationClipSettings(ac, _setting);
                }                    

                AssetDatabase.CreateAsset(newClip, "Assets/Art/Animations/" + assetName + "/" + ac.name + ".anim");
            }
            AssetDatabase.Refresh();

            //create animator controller
            AnimatorOverrideController aoc = new AnimatorOverrideController();
            aoc.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Art/Animations/_base/CharacterBaseAnimatorController.controller");

            AnimationClipOverrides clipOverrides = new AnimationClipOverrides(aoc.overridesCount);
            aoc.GetOverrides(clipOverrides);
            clipOverrides["attack"] = animationClipDir["attack"];
            clipOverrides["fight_be_hit"] = animationClipDir["fight_be_hit_0"];
            clipOverrides["fight_death"] = animationClipDir["fight_death"];
            clipOverrides["run"] = animationClipDir["run"];
            clipOverrides["skill"] = animationClipDir["skill"];
            clipOverrides["skill_l"] = animationClipDir["skill_l"];
            clipOverrides["stand"] = animationClipDir["stand"];
            clipOverrides["victory"] = animationClipDir["victory"];

            aoc.ApplyOverrides(clipOverrides);

            AssetDatabase.CreateAsset(aoc, "Assets/Art/Animations/" + assetName + "/" + assetName + "AC" + ".overrideController");
            AssetDatabase.Refresh();
        }
        AssetDatabase.Refresh();
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity) { }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }
}
