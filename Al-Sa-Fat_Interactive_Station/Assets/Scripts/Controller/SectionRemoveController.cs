using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Helper;

namespace controller
{
    public class SectionRemoveController : MonoBehaviour
    {

        #region UI - Button
        private Button yourButton;
        #endregion

        #region String
        private string RemoveSection_btn;
        #endregion

        #region inheritence
        AppConfig appconf;
        #endregion


        /*..........................................................................Functions starts...................................................................................*/


        #region Unity build - in functions
        void Start()
        {
            intial();
        }
        #endregion

        #region intial
        void intial()
        {
            appconf = new AppConfig();
            yourButton = this.GetComponent<Button>();
            Button btn = yourButton.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }
        #endregion

        #region TaskOnclick
        void TaskOnClick()
        {
            #region changing a canvas's rendermode 

            /**Meanwhile line renderer will appear on screen when "OnDragEnd" function called*
                *Since linerenerer not woriking with screenspaceoverlay rendermode, we changing it to screenspacecamera on "OnDragEnd"
                 * And revert it back to ScreenSpaceOverlay, once "OnClick" function triggered.
                  *And its works vice - Versa.   */

            /*........................................Changing canvas rendermode....................................................*/

            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

            /*.......................................*************************.....................................................*/
            #endregion

            RemoveSection_btn = EventSystem.current.currentSelectedGameObject.transform.parent.name;
            Debug.Log(RemoveSection_btn);

            #region Summery
            /*
                condition 1a ------- [true] -------> condition 2a ------ [true] -----> condition 3a ---[true]--->> Do Stuff
                     .                                   .                                 .
                     .                                   .                                 .
                     .                                   .                                 .
                  (false)                             (false)                           (false)
                     .                                   .                                 .
                     .                                   .                                 .
                     .                                   .                                 .
                condition 1b --->>  Do Stuff        condition 2b --->> Do Stuff        condition 3b --->> Do Stuff



            *Condition 1a = Find a "ColorObjects" transform which is tranform parent of Color sections obejects - See region "make a group for Color Section objects" in DragBehaviour.cs
            *Condition 2a = Find a "ColorObjects" transform's child objects count - Normally there will be only one child, But however we validate this condition for safe.
            *Condition 3a = Validate a Clicked gameobject name with "ColorObjects" transform's first child objects name.
            *Condition 1b = Just detsroy a gameobject relative by name between clicked popup button name and section name.
            *Condition 2b = Just detsroy a gameobject relative by name between clicked popup button name and section name.
            *Condition 3b = Just detsroy a gameobject relative by name between clicked popup button name and section name.

             */
            #endregion

            if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects")) //----------------------------------------------------------------------------Condition 1a  
            {
                if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.childCount > 0) //-----------------------------------------------Condition 2a
                {
                    if (RemoveSection_btn == GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.GetChild(0).transform.name) //----------Condition 3a
                    {
                        appconf.MainHomebacktoOrginColor();
                        Destroy(GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.Find(RemoveSection_btn).gameObject);
                    }
                    else //-----------------------------------------------------------------------------------------------------------------------------------------Condition 3b
                    {
                        Destroy(GameObject.Find("Buildings_parent/buildingSub_Parents").transform.Find(RemoveSection_btn).gameObject);
                    }
                }
                else //----------------------------------------------------------------------------------------------------------------------------------------------Condition 2b
                {
                    Destroy(GameObject.Find("Buildings_parent/buildingSub_Parents").transform.Find(RemoveSection_btn).gameObject);
                }
            }
            else //---------------------------------------------------------------------------------------------------------------------------------------------------Condition 1b
            {
                Destroy(GameObject.Find("Buildings_parent/buildingSub_Parents").transform.Find(RemoveSection_btn).gameObject);
            }

            Destroy(GameObject.Find("popup_Parent").transform.Find(RemoveSection_btn).gameObject);

            try
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(RemoveSection_btn).GetComponent<Button>().interactable = true;
            }
            catch
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.Find(RemoveSection_btn).GetComponent<Button>().interactable = true;
            }


        }
        #endregion

    }
}