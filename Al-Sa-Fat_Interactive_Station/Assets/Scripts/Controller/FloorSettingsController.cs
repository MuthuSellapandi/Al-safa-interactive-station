using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Helper;

namespace controller
{
    public class FloorSettingsController : MonoBehaviour
    {


        #region UI image
        public Image buildingFloor_img;
        #endregion

        #region UI sprite
        public Sprite img;
        private Sprite[] buildingFloors_Sprite;
        #endregion

        #region UI Text
        public Text floorCount_txt;
        #endregion

        #region integer
        int floorCount;
        #endregion

        //..........................................................................Functions starts...................................................................................


        #region floorcountController

        private void Awake()
        {
            buildingFloors_Sprite = Resources.LoadAll<Sprite>("0-10 floors sheet 2048x2048");//Get floor count dynamically from sprite
            //Debug.Log(buildingFloors_Sprite.Length);
        }

        #region add a floor count
        public void OnIncrease()
        {
            if(floorCount < 10)
            floorCount += 1;

            floorCount_txt.text = floorCount.ToString();
        }
        #endregion

        #region decrease a floor count
        public void OnDecrease()
        {
            if (floorCount > 0)
                floorCount -= 1;

            floorCount_txt.text = floorCount.ToString();
        }
        #endregion

        #region render a floor accoring to floor count
        public void renderBuildingFloor()
        {
            buildingFloor_img.GetComponent<Image>().overrideSprite = buildingFloors_Sprite[floorCount];//Override a sprite image from building sprite
            buildingFloor_img.SetNativeSize();
        }
        #endregion


        #endregion
    }
}