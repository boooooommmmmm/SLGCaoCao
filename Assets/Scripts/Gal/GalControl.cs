using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Gal
{
    public class GalControl : Singleton<GalControl>
    {
        public ScreenFader screenFader;

        private void Awake()
        {
            _instance = this;
            screenFader.enabled = false;
        }

        public void NextScene(string nextScene)
        {
            //Global.GetInstance().NextScene(nextScene);
        }
    }
}
