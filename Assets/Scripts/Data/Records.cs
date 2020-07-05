using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class Record
    {
        public int PlayerLevel;
        public int BattleLevel;

        public Dictionary<int, CharacterRecord> CharacterRecords;
        public List<Item> Items;
    }

    public class CharacterRecord
    {
        public int CharacterID;
        public int Level;
        public List<int> Weapons;

        public CharacterRecord(int characterID, int level, List<int> weapons)
        {
            CharacterID = characterID;
            Level = level;
            Weapons = weapons;
        }
    }

    public class Item
    {
        public int ID;
        public int Number;

        public Item(int id, int number)
        {
            ID = id;
            Number = number;
        }
    }
}
