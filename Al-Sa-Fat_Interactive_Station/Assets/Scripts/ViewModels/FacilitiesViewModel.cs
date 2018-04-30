using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ViewModel
{
    public class FacilitiesViewModel : MonoBehaviour
    {
        public GameObject[] FacilityToggles;

        //..........................................................................Functions starts...................................................................................

        public void FacilityViewer()
        {
            FacilityToggles[Random.Range(0, FacilityToggles.Length)].GetComponent<Toggle>().isOn = true;
        }

    }
}
