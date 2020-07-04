using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class DataModule : ModuleBase
    {
        public int CurrentLevel = 1;

        public Record Record = ModuleManager.GetInstance().GetModule<StaticDataModule>("StaticData").GetRecord();
    }
}
