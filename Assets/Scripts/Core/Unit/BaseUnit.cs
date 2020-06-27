using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TbsFramework.Units;

namespace Framework.Core
{
    public class BaseUnit : Unit
    {

        public Color LeadingColor;
        public override void Initialize()
        {
            base.Initialize();
            transform.localPosition -= new Vector3(0, 0, 1);
            GetComponent<Renderer>().material.color = LeadingColor;
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
        }

        public override void MarkAsFriendly()
        {
            GetComponent<Renderer>().material.color = LeadingColor + new Color(0.8f, 1, 0.8f);
        }

        public override void MarkAsReachableEnemy()
        {
            GetComponent<Renderer>().material.color = LeadingColor + Color.red;
        }

        public override void MarkAsSelected()
        {
            GetComponent<Renderer>().material.color = LeadingColor + Color.green;
        }

        public override void UnMark()
        {
            GetComponent<Renderer>().material.color = LeadingColor;
        }
    }
}