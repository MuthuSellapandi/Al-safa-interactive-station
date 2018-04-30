using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using controller;
using System;
using LitJson;
using UnityEngine.Serialization;
using System.IO;

namespace Helper
{
    public class AppConfig
    {
        string json;
        JsonData Section;


        /*..........................................................................Functions starts...................................................................................*/



        public JsonData JsonStrg()
        {
            json = File.ReadAllText(Application.dataPath + "/Resources/Json/SectionInfo.json"); //Load json data from data path
            Section = JsonMapper.ToObject(json);
            return Section;
        }

        public void  MainHomeImapactController()
        {
            GameObject mainhome_Obj = GameObject.Find("Home");
            string sectionName = DragBehaviour.usedfacilityname;

            switch (sectionName)
            {
                case "brown":
                    mainhome_Obj.GetComponent<Image>().color = Color.gray;
                    break;

                case "pale":
                    mainhome_Obj.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f , 1);
                    break;

                case "yello":
                    mainhome_Obj.GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                    break;
            }
        }

        public void MainHomebacktoOrginColor()
        {
            GameObject mainhome_Obj = GameObject.Find("Home");
            mainhome_Obj.GetComponent<Image>().color = Color.white;
        }



    }
}