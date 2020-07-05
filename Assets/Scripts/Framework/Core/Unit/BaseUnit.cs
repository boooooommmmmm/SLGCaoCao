using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Units;
using Game.Data;

namespace Framework.Core
{
    [System.Serializable]
    public class Skill_Configs
    {
        public string strSkillName;
        public float fSkillFactor;//negative for heal
        public int iSkillRange;
    }

    public class BaseUnit : Unit
    {
        [Header("CharacterID:")]
        public int CharacterID = 0;

        [SerializeField]
        private List<Skill_Configs> m_ListSkills;

        [SerializeField]
        private List<Renderer> m_ListRenders;
        [SerializeField]
        private Animator AnimatorController;

        private List<MaterialPropertyBlock> m_ListMPB = new List<MaterialPropertyBlock>();

        private float fSetGraySpeed = 0.2f;
        private float fSetRedSpeed = 0.2f;
        private float fMaxRed = 0.5f;

        private bool bIsReachableEnemy = false;

        public override void Initialize()
        {
            base.Initialize();
            transform.localPosition -= new Vector3(0, 0, 1);//terrian has height

            //init MaterialPropertyBlock
            foreach (Renderer render in m_ListRenders)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                render.GetPropertyBlock(propertyBlock);
                propertyBlock.SetFloat("_Gray", 0f);
                render.SetPropertyBlock(propertyBlock);

                m_ListMPB.Add(propertyBlock);
            }

            //get data from data module
            if (CharacterID != 0)
            {
                CharacterData characterData = ModuleManager.GetInstance().GetModule<DataModule>("Data").GetCharacterData(CharacterID);
                HitPoints = characterData.Hp;
                AttackRange = characterData.AtkRange;
                AttackFactor = characterData.Atk;
                DefenceFactor = characterData.Def;
                MovementPoints = characterData.Movement;
            }

        }
        public override void MarkAsAttacking(Unit other)
        {
            AnimatorController.SetTrigger("TriggerAttack");
        }

        public override void MarkAsDefending(Unit other)
        {
            AnimatorController.SetTrigger("TriggerHitted");
            StartCoroutine(SetRed(true, true));
        }

        public override void MarkAsDestroyed()
        {
            //play death animation then destroy
        }

        public override void MarkAsFinished()
        {
            base.MarkAsFinished();
        }

        public override void MarkAsFriendly()
        {
        }

        public override void MarkAsReachableEnemy()
        {
            //set red
            bIsReachableEnemy = true;
            SetRed(true);
        }

        public override void MarkAsNotReachableEnemy()
        {
            base.MarkAsNotReachableEnemy();

            if (bIsReachableEnemy)
            {
                SetRed(false);
                bIsReachableEnemy = false;
            }
        }

        public override void MarkAsSelected()
        {
        }

        public override void UnMark()
        {
        }

        public override void OnUnitSelected()
        {
            base.OnUnitSelected();

            if (IsFinished)
                AnimatorController.SetTrigger("TriggerIdle");
            else
                AnimatorController.SetTrigger("TriggerRun");
        }

        public override void OnUnitDeselected()
        {
            base.OnUnitDeselected();

            if (!IsMoving)
                AnimatorController.SetTrigger("TriggerIdle");
        }

        public override void OnUnitFinished()
        {
            base.OnUnitFinished();

            StartCoroutine(SetGray(true));
            AnimatorController.SetTrigger("TriggerIdle");
        }

        public override void OnTurnEnd()
        {
            base.OnTurnEnd();

            AnimatorController.SetTrigger("TriggerIdle");
            StartCoroutine(SetGray(false));
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

        private IEnumerator SetRed(bool b, bool isYoyo = false)
        {
            float timeCount = 0;
            float red;

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

            if (isYoyo)
            {
                timeCount = 0;
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
            }

            for (int i = 0; i < m_ListMPB.Count; i++)
            {
                if (isYoyo)
                    m_ListMPB[i].SetFloat("_Hitted", b ? 0f : fMaxRed);
                else
                    m_ListMPB[i].SetFloat("_Hitted", b ? fMaxRed : 0f);
                m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
            }

        }

        private void SetRed(bool b)
        {
            for (int i = 0; i < m_ListMPB.Count; i++)
            {
                m_ListMPB[i].SetFloat("_Hitted", b ? fMaxRed : 0f);
                m_ListRenders[i].SetPropertyBlock(m_ListMPB[i]);
            }
        }
    }
}