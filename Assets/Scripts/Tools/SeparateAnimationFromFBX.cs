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
            var newAnimationClipDir = new Dictionary<string, AnimationClip>();
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

                if (ac.name.Equals("run") || ac.name.Equals("victory") || ac.name.Equals("stand"))
                {
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

                newAnimationClipDir.Add(ac.name, newClip);
                AssetDatabase.CreateAsset(newClip, "Assets/Art/Animations/" + assetName + "/" + assetName + "_" + ac.name + ".anim");
            }
            AssetDatabase.Refresh();

            //create animator controller
            AnimatorOverrideController aoc = new AnimatorOverrideController();
            aoc.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<AnimatorController>("Assets/Art/Animations/_base/CharacterBaseAnimatorController.controller");

            AnimationClipOverrides clipOverrides = new AnimationClipOverrides(aoc.overridesCount);
            aoc.GetOverrides(clipOverrides);
            clipOverrides["attack"] = newAnimationClipDir.ContainsKey("attack") ? newAnimationClipDir["attack"] : null;
            clipOverrides["fight_be_hit"] = newAnimationClipDir.ContainsKey("fight_be_hit_0") ? newAnimationClipDir["fight_be_hit_0"] : null;
            clipOverrides["fight_death"] = newAnimationClipDir.ContainsKey("fight_death") ? newAnimationClipDir["fight_death"] : null;
            clipOverrides["run"] = newAnimationClipDir.ContainsKey("run") ? newAnimationClipDir["run"] : null;
            clipOverrides["skill"] = newAnimationClipDir.ContainsKey("skill") ? newAnimationClipDir["skill"] : null;
            clipOverrides["skill_l"] = newAnimationClipDir.ContainsKey("skill_l") ? newAnimationClipDir["skill_l"] : null;
            clipOverrides["stand"] = newAnimationClipDir.ContainsKey("stand") ? newAnimationClipDir["stand"] : null;
            clipOverrides["victory"] = newAnimationClipDir.ContainsKey("victory") ? newAnimationClipDir["victory"] : null;

            aoc.ApplyOverrides(clipOverrides);

            AssetDatabase.CreateAsset(aoc, "Assets/Art/Animations/" + assetName + "/" + assetName + "AC" + ".overrideController");
            AssetDatabase.Refresh();
        }

        AssetDatabase.Refresh();
        Debug.Log("Animation extrat finished!");
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
