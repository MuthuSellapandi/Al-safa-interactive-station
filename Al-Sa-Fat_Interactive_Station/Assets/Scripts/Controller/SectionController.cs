using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using controller;

namespace controller
{
    public class SectionController : MonoBehaviour
    {
        #region enum
        public enum sectionsCatgory
        {
            Section_301,
            Section_401,
            Section_501,
            Section_601,
            Section_701,
        }

        [SerializeField]
        private static sectionsCatgory sectioncatgory = sectionsCatgory.Section_301;
        #endregion

        public GameObject[] obj;
  
        public static int i = 0;
        DragBehaviour dragbehaviour;


        public void Start()
        {
            
        }

        IEnumerator GetParent(float wait)
        {
            yield return new WaitForSeconds(wait);
            obj = GameObject.FindGameObjectsWithTag("Buttons_Section_Catagory");
            dragbehaviour = new DragBehaviour();
            Sectioncontroller(true);
        }

        public void btn()
        {
            StartCoroutine(GetParent(1.0F));
        }
        public void Sectioncontroller(bool isIncrease)
        {
            if (isIncrease)
            {
                if (i >= 0)
                {
                    i += 1;
                }

                if (i > 5)
                {
                    i = 1;
                }
            }else
            {
                if (i >= 1)
                {
                    i -= 1;
                }

                if (i == 0)
                {
                    i = 5;
                }
            }

            for (int a = 0; a < obj.Length; a++)
            {
                if (a == i-1)
                {
                    obj[a].SetActive(true);
                }
                else
                    obj[a].SetActive(false);
            }

            

        }



    }

    
}
