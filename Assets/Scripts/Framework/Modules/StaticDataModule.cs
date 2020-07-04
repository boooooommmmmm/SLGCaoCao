using Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Framework.Data
{
    public class StaticDataModule : Core.ModuleBase
    {
        private string galProcess = "001";
        private List<string> listGalProcess = new List<string>();
        private List<Dictionary<string, string>> listGal = new List<Dictionary<string, string>>();

        public sealed override void Start()
        {
            //StreamReader inp_stm = new StreamReader("Design/Characters.csv", Encoding.UTF8);
            //while (!inp_stm.EndOfStream)
            //{
            //    string inp_ln = inp_stm.ReadLine();
            //    Debug.Log(inp_ln);
            //}
            //inp_stm.Close();

            //load gal config
            listGalProcess.Clear();
            if (galProcess.Equals("001"))
            {
                StreamReader inp_stm = new StreamReader("Design/Gal/TableGal001.csv", Encoding.UTF8);
                while (!inp_stm.EndOfStream)
                {
                    string inp_ln = inp_stm.ReadLine();
                    listGalProcess.Add(inp_ln);
                }
                inp_stm.Close();
            }

            parseGalData();
        }

        void parseGalData()
        {
            listGal = new List<Dictionary<string, string>>();
            string[] arrGalDataStructure = listGalProcess[0].Split(',');

            for (int i = 1; i <= listGalProcess.Count - 1; i++)
            {
                string[] _tempArr = listGalProcess[i].Split(',');
                Dictionary<string, string> _dir = new Dictionary<string, string>();

                for (int i2 = 0; i2 < _tempArr.Length; i2++)
                {
                    _dir.Add(arrGalDataStructure[i2], _tempArr[i2]);
                }

                listGal.Add(_dir);
            }
        }

        public List<Dictionary<string, string>> GetFirstGalData()
        {
            return listGal;
        }

        //void parseData()
        //{
        //    for (int i = 0; i < stringList.Count; i++)
        //    {
        //        string[] temp = stringList[i].Split(",");
        //        for (int j = 0; j < temp.Length; j++)
        //        {
        //            temp[j] = temp[j].Trim();  //removed the blank spaces
        //        }
        //        parsedList.Add(temp);
        //    }
        //    //you should now have a list of arrays, ewach array can ba appied to the script that's on the Sprite
        //    //you'll have to figure out a way to push the data the sprite
        //    SpriteScript.Name = parsedList[0];
        //    SpriteScript.country = parsedList[1];
        //    SpriteScript.size = parsedList[2];
        //    SpriteScript.population = parsedList[3];
        //}
    }
}
