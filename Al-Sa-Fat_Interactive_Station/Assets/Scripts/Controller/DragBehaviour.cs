using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Events;
using Helper;
using controller;
using ViewModel;

namespace controller
{
    public class DragBehaviour : MonoBehaviour
    {
        #region GameObject

        [Tooltip("The obj which going to Instantiate while click and drag to scene")]
        public Sprite[] sections_Sprite, ColorsOptionsList_Sprite; //Need to load a game object dynamically from Resources foler/Asset bundle

        public GameObject sections_btn;

        GameObject clone; //dummyObj will load as this object

        public GameObject section_Cloneobj; 

        public GameObject triggerEvent_Holder_obj;

        GameObject colorParent, colorsListParent;

        GameObject sectionBtn, ColorOptionsBtn;

        #endregion

        #region Tranform
        [Tooltip("This transform will make a Instantiated 'dummyObj's as a children of  own")]
        public Transform parent;
        #endregion

        #region Strings
        public static string usedfacilityname;//Returnes the selected facility name in string
        private List<string> ColorButtonID = new List<string>{ "304.04" };

        private List<string> ColorListFromSprite = new List<string>();
        #endregion

        #region bool
        public bool moved;
        bool CanShowColorSection;
        #endregion

        #region inheritence
        AppConfig appconf;
        #endregion

        #region Vectors
        Vector3 DynamicPosForSections;
        #endregion

        #region Interger
        int childIndex;
        int colorbtnClickCount_int;
        #endregion


        /*..........................................................................Functions starts...................................................................................*/


        #region Unity build - in functions
        private void Start()
        {
            Intital();
        }
        #endregion

        #region Intital
        private void Intital()
        {
            appconf = new AppConfig();
            DynamicPosForSections = new Vector3(0, 0, -1);
            ColorsOptionsList_Sprite = Resources.LoadAll<Sprite>("UV value sheet_1024x1024");
            for (int i = 0; i < ColorsOptionsList_Sprite.Length; i++)
            {
                ColorListFromSprite.Add(ColorsOptionsList_Sprite[i].name);
            }
            
        }
        #endregion

        #region Createbutton - Where we dynamically create a button for sections from length of sprite slices - Execute from listerner
        public void Createbutton()//Createbutton
        {
            sections_Sprite = Resources.LoadAll<Sprite>("Section 300 sheet 4096x4096");

            for (int i = 0; i < sections_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_Sprite[i];
                sectionBtn.gameObject.name = sections_Sprite[i].name;
            }

        }
        #endregion

        #region Onclick - Where builing objects instantiate
        public void OnClick()
        {
            #region changing a canvas's rendermode 

            /* * Meanwhile line renderer will disappear from screen untill "OnDragEnd" function called*
                * Since linerenerer not woriking with screenspaceoverlay rendermode, we changing it to screenspacecamera on "OnDragEnd"
                    * And revert it back to ScreenSpaceOverlay, once "OnClick" function triggered.
                         * And its works vice -Versa.   */
            /*........................................Changing canvas rendermode....................................................*/

            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            /*.......................................*************************.....................................................*/

            #endregion
 
            usedfacilityname = EventSystem.current.currentSelectedGameObject.name;

            GameObject[] popupHolderObj = GameObject.FindGameObjectsWithTag("popUp");
            foreach(GameObject go in popupHolderObj)
            {
                Debug.Log("GameObject go in popupHolderObj ");
                Destroy(go.gameObject);
            }

            if (!ColorListFromSprite.Contains(usedfacilityname))
            {
                GameObject ColorDragbtnHolder = GameObject.Find("colorsListParent");
                if (ColorDragbtnHolder && !ColorButtonID.Contains(usedfacilityname))
                {
                    Destroy(ColorDragbtnHolder.gameObject);
                }
            }

            if (ColorButtonID.Contains(usedfacilityname))
            {
                if (colorbtnClickCount_int == 0)
                {
                    ShowColorOption();
                    colorbtnClickCount_int += 1;
                }
            }
            else
            {              
                int selectedSection = EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex();
                clone = Instantiate(section_Cloneobj, Input.mousePosition, Quaternion.identity);
                clone.transform.name = usedfacilityname;
                if (ColorListFromSprite.Contains(usedfacilityname))
                {
                    clone.GetComponent<Image>().overrideSprite = ColorsOptionsList_Sprite[selectedSection];
                    CanShowColorSection = true;
                } else
                {
                    clone.GetComponent<Image>().overrideSprite = sections_Sprite[selectedSection - 1];
                }
                clone.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
                clone.GetComponent<Image>().SetNativeSize();
                clone.GetComponent<RectTransform>().sizeDelta = clone.GetComponent<RectTransform>().sizeDelta / 4;
                clone.transform.SetParent(parent);
                colorbtnClickCount_int = 0;
            }

            #region add triggerEvent during runtime
            //EventTrigger.Entry onEntry = new EventTrigger.Entry();
            //onEntry.eventID = EventTriggerType.Drag;
            //onEntry.callback.RemoveAllListeners();
            //onEntry.callback.AddListener((eventData) => { OnDragMove((PointerEventData)eventData); });
            //clone.gameObject.AddComponent<EventTrigger>().triggers.Add(onEntry);
            #endregion
        }
        #endregion

        #region onExit - where we destroy a clone object once there is no drag 
        public void OnExit()
        {
            if (!ColorButtonID.Contains(usedfacilityname))
            {
                if (!moved && EventSystem.current.currentSelectedGameObject.GetComponent<Button>().IsInteractable())
                {
                    Destroy(clone);
                }
            }


        }
        #endregion

        #region OnDragMove - Where instantiate object moves
        public void OnDragMove()
        {        
            if (CanShowColorSection)
            {
                if (ColorButtonID.Contains(usedfacilityname))
                {
                    if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable)
                    {                       
                        OnDragMoveController(usedfacilityname);
                    }
                }
                else if (ColorListFromSprite.Contains(usedfacilityname))
                {
                    if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable)
                    {                      
                        OnDragMoveController(usedfacilityname);
                    }
                }
            }
            else
            {               
                if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.Find(usedfacilityname).GetComponent<Button>().interactable)
                {                  
                   OnDragMoveController(usedfacilityname);
                }
            }
        }

        void OnDragMoveController(string draggingObj)
        {
            GameObject dragObj = GameObject.Find("Buildings_parent/buildingSub_Parents").transform.Find(usedfacilityname).gameObject;
            if (dragObj)
            {
                dragObj.transform.position = new Vector3(Mathf.Round(Input.mousePosition.x), Mathf.Round(Input.mousePosition.y), Mathf.Round(Input.mousePosition.z));
                moved = true;
            }
        }
        #endregion

        #region OnDragEnd - Where have some validation and logics
        public void OnDragEnd()
        {
            //RectTransform homeRect = GameObject.Find("Home").GetComponent<RectTransform>();
            //RectTransform rect_ = clone.GetComponent<RectTransform>();

            //if(homeRect.rect.Overlaps(rect_.rect))
            //{
            //    Debug.Log("Overlaps");
            //}

            // SetPostionForSections();
            clone.GetComponent<Image>().color = Color.white;
            clone.GetComponent<PopupViewModel>().enabled = true;
            moved = false;
            clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, -1f);
            appconf.MainHomeImapactController();

            #region changing a canvas's rendermode

            /**Meanwhile line renderer will appear on screen when "OnDragEnd" function called*
                *Since linerenerer not woriking with screenspaceoverlay rendermode, we changing it to screenspacecamera on "OnDragEnd"
                 * And revert it back to ScreenSpaceOverlay, once "OnClick" function triggered.
                  *And its works vice - Versa.   */

            /*........................................Changing canvas rendermode....................................................*/

            GameObject.Find("Canvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

            /*.......................................*************************.....................................................*/

            #endregion
           

            #region make a group for Color Section objects
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>().IsInteractable())
            {
                if (ColorListFromSprite.Contains(usedfacilityname))
                {
                    SetGroupByTag(usedfacilityname);
                }              
            }
            #endregion
            if (!CanShowColorSection)
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.Find(usedfacilityname).GetComponent<Button>().interactable = false;
            }else
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable = false;
            }
            foreach (string str in ColorButtonID)
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.Find(str).GetComponent<Button>().interactable = true;
            }
            CanShowColorSection = false;
        }

        #endregion

        #region checkForColorGroupNull - where we validate a colorparent child object count and destroy if its 0
        private void checkForColorGroupNull()
        {
            if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects"))
            {
                if (colorParent.transform.childCount <= 0)
                {
                    Destroy(colorParent);
                }
            }
        }
    #endregion

        #region assign a tag name
        public void SetGroupByTag(string stg)
        {

            if(colorParent==null)
            {
                colorParent = new GameObject();
            }

            if (colorParent.transform.childCount > 0)
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(colorParent.transform.GetChild(0).gameObject.name).GetComponent<Button>().interactable = true;
                Destroy(colorParent.transform.GetChild(0).gameObject);
            }

            colorParent.gameObject.name = "colorObjects";
            colorParent.transform.parent = parent;

            GameObject colorSectionObj = GameObject.Find("buildingSub_Parents/" + stg);
            colorSectionObj.tag = "Color";
            colorSectionObj.transform.SetParent(colorParent.transform);

        }
        #endregion

        #region ShowColorOption
        public void ShowColorOption()
        {
            Debug.Log("show color option");
            ColorsOptionsList_Sprite = Resources.LoadAll<Sprite>("UV value sheet_1024x1024");
            float PosMultiplier = ColorsOptionsList_Sprite.Length ;

            if(colorsListParent == null)
            {
                colorsListParent = new GameObject();
                colorsListParent.gameObject.name = "colorsListParent";
                colorsListParent.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/").transform, false);
                GameObject parentObj = GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/" + usedfacilityname);
                colorsListParent.transform.localPosition = new Vector3(parentObj.transform.localPosition.x, parentObj.transform.localPosition.y, parentObj.transform.localPosition.z);
            }

            for (int i = 0; i < ColorsOptionsList_Sprite.Length; i++)
            {
                ColorOptionsBtn = Instantiate(sections_btn, new Vector3(0, 90, 0), Quaternion.identity);
                ColorOptionsBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform, false);
                ColorOptionsBtn.GetComponentInChildren<Text>().text = ColorsOptionsList_Sprite[i].name;
                ColorOptionsBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = ColorsOptionsList_Sprite[i];
                ColorOptionsBtn.gameObject.name = ColorsOptionsList_Sprite[i].name;
                ColorOptionsBtn.tag = "ColorListObjsBtn";
            }

            if(GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects"))
            {
                if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.childCount > 0)
                {
                    string UsedColorName_strg = GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.GetChild(0).name;
                    GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/colorsListParent").transform.Find(UsedColorName_strg).GetComponent<Button>().interactable = false;
                }
            }
            StartCoroutine(AnimateOption("colorsListParent", 0.01f)); // Show popup by sending a genric function name to animateoption
            Debug.Log("show color option 2");
        }
        #endregion

        #region animateOption
        IEnumerator AnimateOption(string nameofOption, float wait)
        {
            Debug.Log("AnimateOptionn 1");
            int e = 0;
            GameObject options = GameObject.Find(nameofOption);
            int sibblingIndex = options.transform.childCount;
            int middleSibblingIndex = sibblingIndex / 2;
            Vector3 middlePos = options.transform.GetChild(middleSibblingIndex).transform.localPosition;
            float XPos = middlePos.x;

            for (int i = 0; i <= middleSibblingIndex; i++)
            {
                Vector3 start = new Vector3(options.transform.GetChild(i).transform.localPosition.x, 90f, 0);
                Vector3 endPos = new Vector3(options.transform.GetChild(i).transform.localPosition.x + (XPos + (i * -100)), 90, 0);
                options.transform.GetChild(i).localPosition = endPos;
                yield return new WaitForSeconds(wait);
            }

            for (int j = middleSibblingIndex + 1; j < sibblingIndex; j++)
            {
                e += 1;
                Vector3 endPos = new Vector3(options.transform.GetChild(j).transform.localPosition.x + (XPos + (e * 100)), 90, 0);
                options.transform.GetChild(j).localPosition = endPos;
                yield return new WaitForSeconds(wait);
            }
            Debug.Log("AnimateOptionn 2");
        }
        #endregion
    }
}

/*..........................................................................Unused Functions ...................................................................................*/

#region unused Functions
/*

#region setposition for sections
public void SetPostionForSections()
{
    if (parent.childCount < 2)
    {
        Debug.Log("parent child count " + parent.childCount);
        clone.transform.localPosition = DynamicPosForSections;
    }
    else if (parent.childCount >= 2)
    {
        Debug.Log("Parent child count grater 2");
        Vector3 newPos = parent.GetChild(childIndex).transform.localPosition;
        clone.transform.localPosition = new Vector3(newPos.x * 2, 0, -2);
    }

    childIndex++;
}
#endregion
 */
#endregion

