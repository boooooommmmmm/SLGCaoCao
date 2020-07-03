using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Framework.Core;

namespace UI.Start
{
    public class UIStartPanel : MonoBehaviour
    {
        public Button Button_Start;

        void Start()
        {
            Button_Start.onClick.AddListener(OnClickStart);
        }

        void Update()
        {

        }

        private void OnDestroy()
        {
            Button_Start.onClick.RemoveAllListeners();
        }

        void OnClickStart()
        {
            StateMachineManager.Get().SwitchToBrother("Battle");
        }
    }
}
