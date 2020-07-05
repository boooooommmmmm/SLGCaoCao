using Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Framework.Core;

namespace Game.Data
{
    public class StaticDataModule : ModuleBase
    {
        //static data name; index; value pair
        private Dictionary<string, Dictionary<int, Dictionary<string, string>>> staticData = new Dictionary<string, Dictionary<int, Dictionary<string, string>>>();


        public sealed override void Start()
        {

            StreamReader inp_stm = new StreamReader("Design/Characters.csv", Encoding.UTF8);
            List<string> listCharacterData = new List<string>();
            while (!inp_stm.EndOfStream)
            {
                string inp_ln = inp_stm.ReadLine();
                listCharacterData.Add(inp_ln);
            }
            inp_stm.Close();
            parseData("Character", listCharacterData);

            //load gal config            
            List<string> listGalProcess = new List<string>();
            inp_stm = new StreamReader("Design/Gal/TableGal001.csv", Encoding.UTF8);
            while (!inp_stm.EndOfStream)
            {
                string inp_ln = inp_stm.ReadLine();
                listGalProcess.Add(inp_ln);
            }
            inp_stm.Close();
            parseData("Gal001", listGalProcess);
        }

        void parseData(string dataName, List<string> listRawData)
        {
            if (staticData.ContainsKey(dataName))
            {
                Debug.LogError(string.Format("Static data module error! Data [{0}] already in memory! ", dataName));
                return;
            }

            Dictionary<string, string> _valuePair = new Dictionary<string, string>();
            Dictionary<int, Dictionary<string, string>> _finalData = new Dictionary<int, Dictionary<string, string>>();

            string[] arrDataStructure = listRawData[0].Split(',');
            for (int i = 1; i <= listRawData.Count - 1; i++)
            {
                string[] _tempArr = listRawData[i].Split(',');
                _valuePair = new Dictionary<string, string>();

                for (int i2 = 0; i2 < _tempArr.Length; i2++)
                {
                    _valuePair.Add(arrDataStructure[i2], _tempArr[i2]);
                }

                _finalData[int.Parse(_valuePair["ID"])] = _valuePair;
            }

            staticData[dataName] = _finalData;
        }

        public Dictionary<int, Dictionary<string, string>> GetData(string dataName)
        {
            return staticData[dataName];
        }

        public Dictionary<string, string> GetDataByIndex(string dataName, int index)
        {
            return staticData[dataName][index];
        }

        public Record GetRecord()
        {
            //return JsonUtility.FromJson<Record>("path");
#if true
            return new Record();
#endif
        }

        public void SaveRecord()
        {

        }
    }
}
