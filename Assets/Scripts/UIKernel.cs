using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class UIKernel : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        public GameObject RootUI;
        public GameObject TopUI;

        [Header("Prefabs")]
        public GameObject StartPanel;
        public GameObject BattlePanel;

        private GameObject _startPanel;
        private GameObject _battlePanel;

        private void Awake()
        {
        }

        private void Start()
        {
        }

        public void PushStartPanel()
        {
            _startPanel = Instantiate(StartPanel, RootUI.transform);
            _startPanel.transform.localPosition = Vector3.zero;
            _startPanel.transform.localRotation = Quaternion.identity;
        }

        public void PopStartPanel()
        {
            Destroy(_startPanel);
            _startPanel = null;
        }
    }
}
