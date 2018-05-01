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

        //Need to load a game object dynamically from Resources foler/Asset bundle
        [Tooltip("The obj which going to Instantiate while click and drag to scene")]
        public Sprite[] sections_301_Sprite, sections_401_Sprite, sections_501_Sprite, sections_601_Sprite, sections_701_Sprite, 
                        ColorsOptionsList_Sprite, PollutionOptionList_Sprite;                                                                           

        public GameObject sections_btn;

        GameObject clone; //dummyObj will load as this object

        public GameObject section_Cloneobj; 

        public GameObject triggerEvent_Holder_obj;

        GameObject SubSectionParent, colorsListParent, 
                                PollutionListParent ;

        GameObject sectionBtn, SubSectionBtn;

        GameObject[] parentobj;

        #endregion

        #region Tranform
        [Tooltip("This transform will make a Instantiated 'dummyObj's as a children of  own")]
        public Transform parent;
        #endregion

        #region Strings
        public static string usedfacilityname;//Returnes the selected facility name in string
        private List<string> ColorButtonID = new List<string>{ "304.04" };
        private List<string> ColorListFromSprite = new List<string>();

        private List<string> Exterior_InteriorPolutionButtonID = new List<string> { "303.01"};
        private List<string> Exterior_InteriorPolution_ListFromSprite = new List<string>();
        #endregion

        #region bool
        public bool moved;
        bool CanShowSubSection;
        #endregion

        #region inheritence
        AppConfig appconf;
        #endregion

        #region Vectors
        Vector3 DynamicPosForSections;
        #endregion

        #region Interger
        //int childIndex;
        int colorbtnClickCount_int, pollutionbtnClickCount_int;
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
            //DynamicPosForSections = new Vector3(0, 0, -1);

            #region Load sprites from Resources folder
            ColorsOptionsList_Sprite = Resources.LoadAll<Sprite>("UV value sheet_1024x1024");
            PollutionOptionList_Sprite = Resources.LoadAll<Sprite>("Section_3_pollution_SubSection");

            #endregion

            for (int i = 0; i < ColorsOptionsList_Sprite.Length; i++)
            {
                ColorListFromSprite.Add(ColorsOptionsList_Sprite[i].name);
            }

            for (int i = 0; i < PollutionOptionList_Sprite.Length; i++)
            {
                Exterior_InteriorPolution_ListFromSprite.Add(PollutionOptionList_Sprite[i].name);
            }
            Debug.Log("dfsdfsdfsdf " + Exterior_InteriorPolution_ListFromSprite.Count);
        }
        #endregion

        #region Createbutton - Where we dynamically create a button for sections from length of sprite slices - Execute from listerner
        public void Createbutton()//Createbutton
        {

            sections_301_Sprite = Resources.LoadAll<Sprite>("Section_3_Sprite");
            sections_401_Sprite = Resources.LoadAll<Sprite>("401 ilustrations 2048x2048");
            sections_501_Sprite = Resources.LoadAll<Sprite>("Section 300 sheet 4096x4096");
            sections_601_Sprite = Resources.LoadAll<Sprite>("Section 300 sheet 4096x4096");
            sections_701_Sprite = Resources.LoadAll<Sprite>("Section 300 sheet 4096x4096");

            for(int i = 0; i < 5; i++ )
            {
                GameObject obj = new GameObject("parent " + i);
                obj.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform, false);
                obj.transform.localPosition = new Vector3(2344f, -150f, 0);
                obj.transform.tag = "Buttons_Section_Catagory";
            }

            parentobj = GameObject.FindGameObjectsWithTag("Buttons_Section_Catagory");


            for (int i = 0; i < sections_301_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_301_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_301_Sprite[i];
                sectionBtn.gameObject.name = sections_301_Sprite[i].name;
            }
            for (int i = 0; i < sections_401_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 1").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_401_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_401_Sprite[i];
                sectionBtn.gameObject.name = sections_401_Sprite[i].name;
            }
            for (int i = 0; i < sections_301_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 2").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_301_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_301_Sprite[i];
                sectionBtn.gameObject.name = sections_301_Sprite[i].name;
            }
            for (int i = 0; i < sections_301_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 3").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_301_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_301_Sprite[i];
                sectionBtn.gameObject.name = sections_301_Sprite[i].name;
            }
            for (int i = 0; i < sections_301_Sprite.Length; i++)
            {
                sectionBtn = Instantiate(sections_btn, new Vector3(-1750f + (i * 100), 109f, 0), Quaternion.identity);
                sectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 4").transform, false);
                sectionBtn.GetComponentInChildren<Text>().text = sections_301_Sprite[i].name;
                sectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = sections_301_Sprite[i];
                sectionBtn.gameObject.name = sections_301_Sprite[i].name;
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
                Destroy(go.gameObject);
            }

            #region destroy a subSection popup 
            if (!ColorListFromSprite.Contains(usedfacilityname))
            {
                Debug.Log("1");
                GameObject DragHolder = GameObject.Find("colorsListParent");
                if (DragHolder && !ColorButtonID.Contains(usedfacilityname))
                {
                    Debug.Log("2");
                    Destroy(DragHolder.gameObject);
                }
            }

            if (!Exterior_InteriorPolution_ListFromSprite.Contains(usedfacilityname))
            {
                Debug.Log("3");
                GameObject DragHolder = GameObject.Find("PollutionListParent");
                if (DragHolder && !Exterior_InteriorPolutionButtonID.Contains(usedfacilityname))
                {
                    Debug.Log("4");
                    Destroy(DragHolder.gameObject);
                }
            }
            #endregion

            if (ColorButtonID.Contains(usedfacilityname))
            {
                if (colorbtnClickCount_int == 0)
                {
                    ShowColorOption();
                    colorbtnClickCount_int += 1;
                }
            }else if(Exterior_InteriorPolutionButtonID.Contains(usedfacilityname))
            {
                if(pollutionbtnClickCount_int == 0)
                {
                    ShowPollutionOption();
                    pollutionbtnClickCount_int += 1; 
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
                    CanShowSubSection = true;
                }else if(Exterior_InteriorPolution_ListFromSprite.Contains(usedfacilityname))
                {
                    clone.GetComponent<Image>().overrideSprite = PollutionOptionList_Sprite[selectedSection];
                    CanShowSubSection = true;
                }
                else
                {
                    //clone.GetComponent<Image>().overrideSprite = sections_301_Sprite[selectedSection];
                    switch (SectionController.i)
                    {
                        case 1:
                            clone.GetComponent<Image>().overrideSprite = sections_301_Sprite[selectedSection];
                            break;

                        case 2:
                            clone.GetComponent<Image>().overrideSprite = sections_401_Sprite[selectedSection];
                            break;

                        case 3:
                            clone.GetComponent<Image>().overrideSprite = sections_501_Sprite[selectedSection];
                            break;

                        case 4:
                            clone.GetComponent<Image>().overrideSprite = sections_601_Sprite[selectedSection];
                            break;

                        case 5:
                            clone.GetComponent<Image>().overrideSprite = sections_701_Sprite[selectedSection];
                            break;
                    }
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
            if (CanShowSubSection)
            {
                if (ColorButtonID.Contains(usedfacilityname))
                {
                    if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable)
                    {                       
                        OnDragMoveController(usedfacilityname);
                    }
                }
                else if (ColorListFromSprite.Contains(usedfacilityname))
                {
                    if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable)
                    {                      
                        OnDragMoveController(usedfacilityname);
                    }
                }
            }
            else
            {               
                if (GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.GetChild(SectionController.i).transform.Find(usedfacilityname).GetComponent<Button>().interactable)
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

            Debug.Log("clone position world " + clone.transform.position);
            Debug.Log("clone position local " + clone.transform.localPosition);

            if(clone.transform.localPosition.y > 20)
            {
                clone.transform.localPosition = new Vector3(clone.transform.localPosition.x, 40f, clone.transform.localPosition.z);
            }

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
                    SetGroupByTag(usedfacilityname, "parent 0/colorsListParent");
                }    
                
                if(Exterior_InteriorPolution_ListFromSprite.Contains(usedfacilityname))
                {
                    SetGroupByTag(usedfacilityname, "parent 0/PollutionListParent");
                }
            }
            #endregion
            if (!CanShowSubSection)
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.GetChild(SectionController.i).Find(usedfacilityname).GetComponent<Button>().interactable = false;
            }else
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform.Find(usedfacilityname).GetComponent<Button>().interactable = false;
            }

            if (SectionController.i == 1)
            {
                foreach (string str in ColorButtonID)
                {
                    GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content").transform.GetChild(SectionController.i).transform.Find(str).GetComponent<Button>().interactable = true;
                }
            }
            CanShowSubSection = false;
        }

        #endregion

        #region checkForColorGroupNull - where we validate a colorparent child object count and destroy if its 0
        private void checkForColorGroupNull()
        {
            if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects"))
            {
                if (SubSectionParent.transform.childCount <= 0)
                {
                    Destroy(SubSectionParent);
                }
            }
        }
    #endregion

        #region assign a tag name
        public void SetGroupByTag(string stg, string path)
        {

            if(SubSectionParent==null)
            {
                SubSectionParent = new GameObject();
            }


            //continur here 
            if (SubSectionParent.transform.childCount > 0)
            {
                GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/"+ path).transform.Find(SubSectionParent.transform.GetChild(0).gameObject.name).GetComponent<Button>().interactable = true;
                Destroy(SubSectionParent.transform.GetChild(0).gameObject);
            }

            SubSectionParent.gameObject.name = "colorObjects";
            SubSectionParent.transform.parent = parent;

            GameObject colorSectionObj = GameObject.Find("buildingSub_Parents/" + stg);
            colorSectionObj.tag = "Color";
            colorSectionObj.transform.SetParent(SubSectionParent.transform);

        }
        #endregion

        #region ShowColorOption
        public void ShowColorOption()
        {
            Debug.Log("showcolor");
            ColorsOptionsList_Sprite = Resources.LoadAll<Sprite>("UV value sheet_1024x1024");
            float PosMultiplier = ColorsOptionsList_Sprite.Length ;

            if(colorsListParent == null)
            {
                colorsListParent = new GameObject();
                colorsListParent.gameObject.name = "colorsListParent";
                colorsListParent.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0").transform, false);
                GameObject parentObj = GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/" + usedfacilityname);
                Debug.Log(parentObj.transform.localPosition + "," + parentObj.transform.position) ;
                colorsListParent.transform.localPosition = new Vector3(parentObj.transform.localPosition.x, parentObj.transform.localPosition.y, parentObj.transform.localPosition.z);
            }

            for (int i = 0; i < ColorsOptionsList_Sprite.Length; i++)
            {
                SubSectionBtn = Instantiate(sections_btn, new Vector3(0, 90, 0), Quaternion.identity);
                SubSectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform, false);
                SubSectionBtn.GetComponentInChildren<Text>().text = ColorsOptionsList_Sprite[i].name;
                SubSectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = ColorsOptionsList_Sprite[i];
                SubSectionBtn.gameObject.name = ColorsOptionsList_Sprite[i].name;
                SubSectionBtn.tag = "ColorListObjsBtn";
            }

            if(GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects"))
            {
                if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.childCount > 0)
                {
                    string UsedColorName_strg = GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.GetChild(0).name;
                    GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform.Find(UsedColorName_strg).GetComponent<Button>().interactable = false;
                }
            }
            StartCoroutine(AnimateOption("colorsListParent", 0.01f)); // Show popup by sending a genric function name to animateoption
        }
        #endregion

        #region ShowPollutionOption
        public void ShowPollutionOption()
        {
            Debug.Log("showpollution");
            PollutionOptionList_Sprite = Resources.LoadAll<Sprite>("Section_3_pollution_SubSection");
            float PosMultiplier = PollutionOptionList_Sprite.Length;

            if (PollutionListParent == null)
            {
                PollutionListParent = new GameObject();
                PollutionListParent.gameObject.name = "PollutionListParent";
                PollutionListParent.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0").transform, false);
                GameObject parentObj = GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/" + usedfacilityname);
                Debug.Log(parentObj.transform.localPosition + "," + parentObj.transform.position);
                PollutionListParent.transform.localPosition = new Vector3(parentObj.transform.localPosition.x, parentObj.transform.localPosition.y, parentObj.transform.localPosition.z);
            }

            for (int i = 0; i < PollutionOptionList_Sprite.Length; i++)
            {
                SubSectionBtn = Instantiate(sections_btn, new Vector3(0, 90, 0), Quaternion.identity);
                SubSectionBtn.transform.SetParent(GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/PollutionListParent").transform, false);
                SubSectionBtn.GetComponentInChildren<Text>().text = PollutionOptionList_Sprite[i].name;
                SubSectionBtn.transform.Find("Image").GetComponent<Image>().overrideSprite = PollutionOptionList_Sprite[i];
                SubSectionBtn.gameObject.name = PollutionOptionList_Sprite[i].name;
                SubSectionBtn.tag = "PollutionListObjsBtn";
            }

            if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects"))
            {
                if (GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.childCount > 0)
                {
                    string UsedColorName_strg = GameObject.Find("Buildings_parent/buildingSub_Parents/colorObjects").transform.GetChild(0).name;
                    GameObject.Find("Drag_OBJ_Holder/Scroll View/Viewport/Content/parent 0/colorsListParent").transform.Find(UsedColorName_strg).GetComponent<Button>().interactable = false;
                }
            }
            StartCoroutine(AnimateOption("PollutionListParent", 0.01f)); // Show popup by sending a genric function name to animateoption
        }
        #endregion

        #region animateOption
        IEnumerator AnimateOption(string nameofOption, float wait)
        {
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

