using Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class StaticDataModule : ModuleBase
{
    public sealed override void Start()
    {
        StreamReader inp_stm = new StreamReader("Design/Characters.csv", Encoding.UTF8);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();

            Debug.Log(inp_ln);
        }

        inp_stm.Close();
    }

    //void parseList()
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
