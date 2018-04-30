using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Serialization;
using System.IO;
using Helper;

namespace ViewModel
{
    public class PopupViewModel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region gameobjects
        GameObject popup;
        GameObject popupObj;
        GameObject parent;
        #endregion

        #region inheritence
        JsonData Section;
        private AppConfig appconf;
        #endregion

        /*..........................................................................Functions starts...................................................................................*/

        #region Unity Build- In Functions
        private void Start()
        {
            intial();
        }
        #endregion

        #region intial
        void intial()
        {
            appconf = new AppConfig();

            Section = appconf.JsonStrg();

            popup = Resources.Load("Popup") as GameObject;

            parent = GameObject.Find("popup_Parent");
        }
        #endregion

        #region OnPointerUp
        public void OnPointerUp(PointerEventData eventData)
        {

            #region changing a canvas's rendermode for show a line node 

            /* * Meanwhile line renderer will disappear from screen untill "OnDragEnd" function called*
                * Since linerenerer not woriking with screenspaceoverlay rendermode, we changing it to screenspacecamera on "OnDragEnd"
                    * And revert it back to ScreenSpaceOverlay, once "OnClick" function triggered.
                        * And its works vice -Versa.   */

            /*........................................Changing canvas rendermode....................................................*/

            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            /*.......................................*************************.....................................................*/

            #endregion

            #region popup viewer control block

            //Get the array Count of child object from popup's parent object and destroy if its more then 0 index;
            if (parent.transform.childCount > 0)
                Destroy(parent.transform.GetChild(0).gameObject); //Destroy a first one(index 0 )

            #endregion

            string getHoverdObjName = gameObject.GetComponent<Image>().overrideSprite.name;
            popupObj = Instantiate(popup, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            popupObj.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            popupObj.transform.SetParent(parent.transform);
            popupObj.transform.GetChild(1).GetComponent<Text>().text = Section[getHoverdObjName][0]["title"].ToString(); // Load a data from json by getting a mouse over/click button name - Title name.
            popupObj.transform.GetChild(2).GetComponent<Text>().text = Section[getHoverdObjName][0]["Info"].ToString(); // Load a data from json by getting a mouse over/click button name - Discription about cliked object.
            popupObj.transform.name = getHoverdObjName;
 
        }
        #endregion

        #region OnPointerDown
        public void OnPointerDown(PointerEventData eventData)
        {
            Destroy(popupObj);
        }
        #endregion

    }
}
