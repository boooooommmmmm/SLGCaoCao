using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Units;

namespace Framework.Core
{
    public class BaseUnit : Unit
    {
        [SerializeField]
        private List<Renderer> m_ListRenders;
        [SerializeField]
        private Animator AnimatorController;

        private List<MaterialPropertyBlock> m_ListMPB = new List<MaterialPropertyBlock>();

        private float fSetGraySpeed = 0.2f;
        private float fSetRedSpeed = 2f;
        private float fMaxRed = 0.5f;

        public override void Initialize()
        {
            base.Initialize();
            transform.localPosition -= new Vector3(0, 0, 1);

            //init MaterialPropertyBlock
            foreach (Renderer render in m_ListRenders)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                render.GetPropertyBlock(propertyBlock);
                propertyBlock.SetFloat("_Gray", 0f);
                render.SetPropertyBlock(propertyBlock);

                m_ListMPB.Add(propertyBlock);
            }

        }
        public override void MarkAsAttacking(Unit other)
        {
        }

        public override void MarkAsDefending(Unit other)
        {
        }

        public override void MarkAsDestroyed()
        {
        }

        public override void MarkAsFinished()
        {
            //set gray
            StartCoroutine(SetGray(true));
        }

        public override void MarkAsFriendly()
        {
        }

        public override void MarkAsReachableEnemy()
        {
            //set red
        }

        public override void MarkAsSelected()
        {
            //change animation to run
            StartCoroutine(SetRed(true));
        }

        public override void UnMark()
        {
            //change animation back to idle
        }

        private IEnumerator SetGray(bool b)
        {
            float timeCount = 0;
            float gray = b ? 0 : 1f;

            while (timeCount < fSetGraySpeed)
            {
                gray = b ? timeCount / fSetGraySpeed : (1 - timeCount / fSetGraySpeed);
                for (int i = 0; i < m_ListMPB.Count; i++)
                {
                    m_ListMPB[i].SetFloat("_Gray", gray);
                    m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
                }

                timeCount += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < m_ListMPB.Count; i++)
            {
                m_ListMPB[i].SetFloat("_Gray", b ? 1f : 0f);
                m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
            }
        }

        private IEnumerator SetRed(bool b)
        {
            float timeCount = 0;
            float red = b ? 0 : fMaxRed;

            while (timeCount < fSetRedSpeed)
            {
                red = b ? timeCount / fSetRedSpeed * fMaxRed : (fMaxRed - timeCount / fSetRedSpeed);
                for (int i = 0; i < m_ListMPB.Count; i++)
                {
                    m_ListMPB[i].SetFloat("_Hitted", red);
                    m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
                }

                timeCount += Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < m_ListMPB.Count; i++)
            {
                m_ListMPB[i].SetFloat("_Hitted", b ? fMaxRed : 0f);
                m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
            }
        }
    }
}