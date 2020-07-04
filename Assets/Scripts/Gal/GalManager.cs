using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Framework.Core;
using Framework.Data;

namespace Gal
{
    public class GalManager : Singleton<GalManager>
    {
        public Transform background;
        public Transform left;
        public Transform right;
        public Transform galFrame;
        public Transform skip;
        public Gal gal;

        private bool next = false;
        private bool finish;
        private List<Sprite> characterImgs = new List<Sprite>();

        private void Start()
        {
            try
            {
                List<Dictionary<string, string>> firstGalData = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetFirstGalData();
            }
            catch
            {
                Debug.Log("本场景无对话内容。");
            }

            finish = false;

            var cImgs = Resources.LoadAll("Textures/Gal/Characters", typeof(Sprite));

            GameController.GetInstance().Invoke(() =>
            {
                foreach (var cImg in cImgs)
                {
                    characterImgs.Add((Sprite)cImg);
                }

                StartCoroutine(PlayGal());
            }, 1f);
        }

        public IEnumerator PlayVoiceOver()
        {
            var text = GalControl.GetInstance().screenFader.transform.Find("Text").GetComponent<Text>();
            for (int i = 0; i < gal.voiceOver.Count; i++)
            {
                text.text = "";
                var textTween = text.DOText("　　" + gal.voiceOver[i], gal.voiceOver[i].Length * 0.1f);
                textTween.SetEase(Ease.Linear);
                yield return StartCoroutine(WaitNext(textTween));
            }
            var textFadeTween = text.DOFade(0, 0.5f);
            textFadeTween.SetEase(Ease.InQuad);
            if (gal.galCons.Count == 0)
                SceneManager.LoadScene(gal.nextScene);
        }

        public IEnumerator PlayGal()
        {
            if (gal.voiceOver.Count > 0)
                yield return StartCoroutine(PlayVoiceOver());
            yield return new WaitForSeconds(0.5f);  //wait fade
            skip.gameObject.SetActive(true);
            GalControl.GetInstance().screenFader.enabled = true;
            yield return new WaitForSeconds(0.5f);  //wait fade
            Transform last = null;
            for (int i = 0; i < gal.galCons.Count; i++)
            {
                Image img;
                if (gal.galCons[i].position == "Left")
                {
                    img = left.Find("Image").GetComponent<Image>();
                    last = left;
                }
                else if (gal.galCons[i].position == "Right")
                {
                    img = right.Find("Image").GetComponent<Image>();
                    last = right;
                }
                else
                {
                    img = last.Find("Image").GetComponent<Image>();
                    img.DOFade(0, 0.5f);
                    continue;
                }

                if (!img.sprite || img.sprite.name != gal.galCons[i].speaker.ToLower())
                {
                    img.sprite = characterImgs.Find(image => image.name == gal.galCons[i].speaker.ToLower());
                    img.SetNativeSize();
                    img.color = new Color(1, 1, 1, 0);
                    img.DOFade(1, 0.5f);
                }

                var textTween = Talk(gal.galCons[i].speaker, gal.galCons[i].content);
                if (i == 0)
                    yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(WaitNext(textTween));
            }

            GalControl.GetInstance().NextScene(gal.nextScene);
        }

        public Tweener Talk(string speaker, string content)
        {
            //var cName = Global.GetInstance().nameDic[speaker];
            var cName = "Speaker";
            galFrame.Find("Speaker").GetComponent<Text>().text = cName.Substring(cName.IndexOf(" ") + 1) + "：";
            galFrame.Find("Text").GetComponent<Text>().text = "";
            var textT = galFrame.Find("Text").GetComponent<Text>().DOText("　　" + content, content.Length * 0.1f);
            textT.SetEase(Ease.Linear);
            return textT;
        }

        IEnumerator WaitNext(Tweener textTween)
        {
            while (true)
            {
                if (next && !finish)
                {
                    next = false;
                    if (textTween.IsPlaying())
                        textTween.Complete();
                    else
                        break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void Skip()
        {
            finish = true;
            GalControl.GetInstance().NextScene(gal.nextScene);
        }

        public void Next()
        {
            next = true;
        }

        private void Update()
        {
#if (UNITY_STANDALONE || UNITY_EDITOR)
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.currentSelectedGameObject)
                {
                    Next();
                }
                else
                {
                    if (EventSystem.current.currentSelectedGameObject.name != "Skip")
                    {
                        Next();
                    }
                }
            }

#elif (!UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID))
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.currentSelectedGameObject)
            {
                Next();
            }
            else
            {
                if (EventSystem.current.currentSelectedGameObject.name != "Skip")
                {
                    Next();
                }
            }
        }
#endif
        }
    }
}
