using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Framework.Core;
using Framework.Data;
using UnityEditor.UIElements;

namespace Gal
{
    public class GalManager : Singleton<GalManager>
    {
        public Transform background;
        public Image left;
        public Image right;
        public Transform galFrame;
        public Button ButtonSkip;

        private List<Sprite> characterImgs = new List<Sprite>();

        private List<Dictionary<string, string>> currentSceneGalData;
        private int iGalIndex = 0;
        private Dictionary<string, string> dirCurrentGalData;

        private void Start()
        {
            try
            {
                currentSceneGalData = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetFirstGalData();
            }
            catch
            {
                Debug.LogError("Get first gal data failed");
            }

            //TODO: load all sprite in cache list

            StartCoroutine(PlayGal());
        }

        public IEnumerator PlayVoiceOver()
        {
            //var text = GalControl.GetInstance().screenFader.transform.Find("Text").GetComponent<Text>();
            //for (int i = 0; i < gal.voiceOver.Count; i++)
            //{
            //    text.text = "";
            //    var textTween = text.DOText("　　" + gal.voiceOver[i], gal.voiceOver[i].Length * 0.1f);
            //    textTween.SetEase(Ease.Linear);
            //    yield return StartCoroutine(WaitNext(textTween));
            //}
            //var textFadeTween = text.DOFade(0, 0.5f);
            //textFadeTween.SetEase(Ease.InQuad);
            //if (gal.galCons.Count == 0)
            //    SceneManager.LoadScene(gal.nextScene);

            yield return new WaitForEndOfFrame();
        }

        public IEnumerator PlayGal()
        {
            if (dirCurrentGalData == null)
            {
                Debug.Log("Gal finished");
                yield break;
                //TODO: exit gal state
            }

            ButtonSkip.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);  //wait fade
            GalControl.GetInstance().screenFader.enabled = true;
            yield return new WaitForSeconds(0.5f);  //wait fade

            Image last = null;
            for (int i = 0; i < currentSceneGalData.Count; i++)
            {
                iGalIndex = i;
                dirCurrentGalData = currentSceneGalData[i];

                if (!dirCurrentGalData["Audio"].Equals("0"))
                    yield return StartCoroutine(PlayVoiceOver());

                Image img = null;
                if (dirCurrentGalData["Position"].Equals("Left"))
                {
                    last = left;
                }
                else if (dirCurrentGalData["Position"].Equals("Right"))
                {
                    last = right;
                }
                else
                {
                    img.DOFade(0, 0.5f);
                    continue;
                }

                if (!string.IsNullOrEmpty(dirCurrentGalData["Speaker"]))
                {
                    int characterID = int.Parse(dirCurrentGalData["Speaker"]);
                    //var CharacterIconPath = ModuleManager.GetInstance().GetModule<>

                    //img.sprite = characterImgs.Find(image => image.name == gal.galCons[i].speaker.ToLower());
                    //img.SetNativeSize();
                    //img.color = new Color(1, 1, 1, 0);
                    //img.DOFade(1, 0.5f);
                }

                var textTween = Talk(dirCurrentGalData["Speaker"], dirCurrentGalData["Content"]);

                if (i == 0)
                    yield return new WaitForSeconds(0.5f);

                yield return StartCoroutine(WaitNext(textTween));
            }

            GalControl.GetInstance().NextScene("");
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
            yield return new WaitForSeconds(0.1f);
        }

        public void Skip()
        {
            GalControl.GetInstance().NextScene("");
        }

        private void Update()
        {
        }
    }
}
