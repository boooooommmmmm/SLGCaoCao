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

        private void Awake()
        {
        }

        private void Start()
        {
        }
    }
}
