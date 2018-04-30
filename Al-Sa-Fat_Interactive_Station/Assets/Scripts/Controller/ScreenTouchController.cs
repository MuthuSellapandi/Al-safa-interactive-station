using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace controller
{


    public class ScreenTouchController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler

    {

        public static GameObject itemBeingDragged;

        #region IDragHandler implementation

        public void OnDrag(PointerEventData eventData)
        {
            //transform.position = eventData.position;

            Vector3 newpos = (Vector3)eventData.delta;
            float XPos = newpos.x;
            float yPos = 0;
            Debug.Log(newpos);
            transform.localPosition += new Vector3(XPos, yPos, 0);
        }

        #endregion

        #region IEndDragHandler implementation

        public void OnEndDrag(PointerEventData eventData)
        {
            itemBeingDragged = null;
        }

        #endregion

        #region IPointerDownHandler implementation
        public void OnPointerDown(PointerEventData eventData)
        {
            try
            {
                GameObject visblePopup = GameObject.Find("popup_Parent").transform.GetChild(0).gameObject;

                if (visblePopup)
                {
                    Destroy(visblePopup);
                    #region changing a canvas's rendermode 
                    /* * Meanwhile line renderer will disappear from screen untill "OnDragEnd" function called*
                        * Since linerenerer not woriking with screenspaceoverlay rendermode, we changing it to screenspacecamera on "OnDragEnd"
                             * And revert it back to ScreenSpaceOverlay, once "OnClick" function triggered.
                                * And its works vice -Versa.   */

                    /*........................................Changing canvas rendermode....................................................*/

                    GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

                    /*.......................................*************************.....................................................*/

                    #endregion
                }
                else
                {
                    visblePopup = null;
                }
            }catch(Exception e)
            {

            }
        }
        #endregion
    }
}
