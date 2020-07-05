using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class CharacterData
    {
        public int CharacterID;
        public int Level;
        public List<int> Weapons;

        public int Hp;
        public int Mp;
        public int Atk;
        public int Def;
        public int AtkRange;
        public int Movement;


        public CharacterData(CharacterRecord cr)
        {
            CharacterID = cr.CharacterID;
            Level = cr.Level;
            Weapons = cr.Weapons;

            Dictionary<string, string> staticData = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetDataByIndex("Character", CharacterID);

            float _CommandAttrPower = 5 - GameUtility._converAttrToInt(staticData["CommandAttr"]);
            float _IntelligenceAttrPower = (5 - GameUtility._converAttrToInt(staticData["IntelligenceAttr"])) / 2;
            float _ForceAttrPower = 5 - GameUtility._converAttrToInt(staticData["ForceAttr"]);
            float _AgilityAttrPower = (5 - GameUtility._converAttrToInt(staticData["AgilityAttr"])) / 2;

            Hp = Mathf.FloorToInt(_CommandAttrPower * Level) + 100;
            Mp = Mathf.FloorToInt(_IntelligenceAttrPower * Level) + 20;
            Atk = Mathf.FloorToInt(_ForceAttrPower * Level) + 10;
            Def = Mathf.FloorToInt(_AgilityAttrPower * Level) + 0;

            switch (staticData["Corps"])
            {
                case "1":
                    AtkRange = 1;
                    Movement = 5;
                    break;
                case "2":
                    AtkRange = 2;
                    Movement = 3;
                    break;
                case "3":
                    AtkRange = 3;
                    Movement = 2;
                    break;
            }
        }
    }
}
