using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class DataModule : ModuleBase
    {
        public int CurrentLevel = 1;
        public Record Record;
        public Dictionary<int, CharacterData> CharacterDatas = new Dictionary<int, CharacterData>();

        public sealed override void Start()
        {
            Record = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetRecord();

#if true
            Record.PlayerLevel = 5;
            Record.BattleLevel = 1;
            Record.CharacterRecords = new Dictionary<int, CharacterRecord>();
            Record.CharacterRecords.Add(1, new CharacterRecord(1, 5, null));
            Record.CharacterRecords.Add(2, new CharacterRecord(2, 10, null));
            Record.CharacterRecords.Add(3, new CharacterRecord(3, 8, null));

            //init character data
            foreach (KeyValuePair<int, CharacterRecord> kv in Record.CharacterRecords)
            {
                CharacterDatas.Add(kv.Key, new CharacterData(kv.Value));
            }
#endif
        }

        public sealed override void Shutdown()
        {

        }

        public CharacterRecord GetCharacterRecordData(int ID)
        {
            return Record.CharacterRecords[ID];
        }

        public CharacterData GetCharacterData(int ID)
        {
            return CharacterDatas[ID];
        }
    }
}
