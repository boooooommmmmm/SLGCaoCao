using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class Record
    {
        public int PlayerLevel;
        public int Level;

        public List<CharacterRecord> CharacterRecords;
        public List<Item> Items;
    }

    public class CharacterRecord
    {
        public int Level;
        public List<int> Weapons;
    }

    public class Item
    {
        public int ID;
        public int Number;
    }
}
