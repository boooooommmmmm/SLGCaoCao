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

        public int Command = 0;
        public int Intelligence = 0;
        public int Force = 0;
        public int Agility = 0;
        public int Luck = 0;


        public CharacterData(CharacterRecord cr)
        {
            CharacterID = cr.CharacterID;
            Level = cr.Level;
            Weapons = cr.Weapons;

            Dictionary<string, string> staticData = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetDataByIndex("Character", CharacterID);

            Command = (5 - GameUtility._converAttrToInt(staticData["CommandAttr"])) * Level;
            Intelligence = (5 - GameUtility._converAttrToInt(staticData["IntelligenceAttr"])) * Level * 2;
            Force = (5 - GameUtility._converAttrToInt(staticData["ForceAttr"])) * Level / 2;
            Agility = (5 - GameUtility._converAttrToInt(staticData["AgilityAttr"])) * Level / 2;

            Hp = Mathf.FloorToInt(Command * 3) + 100;
            Mp = Mathf.FloorToInt(Intelligence * 2) + 20;
            Atk = Mathf.FloorToInt(Force * Level) + 10;
            Def = Mathf.FloorToInt(Agility * Level) + 0;

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
