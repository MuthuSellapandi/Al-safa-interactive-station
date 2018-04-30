using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class AnalogMeterController : MonoBehaviour
    {

        #region Tranforms of Analog display - Note: Pivot of Analog needle should be in "down"
        public Transform co2, tempReduced, reservedEnergy, reservedWater, recycledWater;
        #endregion


        //..........................................................................Functions starts...................................................................................


        #region onclick event - where we set a value for analog meter
        public void OnClick()
        {
            switch (DragBehaviour.usedfacilityname)
            {
                case "301.01":
                    OnRotate(1f, 2f, 3f, 4f, 5f);
                    break;

                case "301.02":
                    OnRotate(2f, 4f, 6f, 8f, 10f);
                    break;

                case "2":
                    OnRotate(3f, 6f, 9f, 12f, -15f);
                    break;

                case "3)":
                    OnRotate(4f, 8f, 12f, 16f, -20f);
                    break;

                case "4":
                    OnRotate(5f, 10f, 15f, 20f, -25f);
                    break;

                case "5":
                    OnRotate(6f, 12f, 18f, 24f, -30f);
                    break;

                case "6":
                    OnRotate(7f, 14f, 21f, 28f, -35f);
                    break;

                case "7":
                    OnRotate(8f, 16f, 24f, 32f, -40f);
                    break;

                case "8":
                    OnRotate(9f, 18f, 27f, 36f, 45f);
                    break;

                case "9":
                    OnRotate(10f, 20f, 30f, 40f, -50f);
                    break;

                default:
                    OnRotate(Random.Range(10, 20), Random.Range(10, 20), Random.Range(10, 20), Random.Range(10, 20), Random.Range(10, 20));
                    break;
            }
        }
        #endregion

        #region Generic function - where we get a value for analog value control from "OnClick" function
        public void OnRotate(float carbon, float tempreduced, float reservedenergy, float reservedwater, float recycledwater)
        {
            co2.transform.eulerAngles = new Vector3(0, 0, carbon);
            tempReduced.transform.eulerAngles = new Vector3(0, 0, tempreduced);
            reservedEnergy.transform.eulerAngles = new Vector3(0, 0, reservedenergy);
            reservedWater.transform.eulerAngles = new Vector3(0, 0, reservedwater);
            recycledWater.transform.eulerAngles = new Vector3(0, 0, recycledwater);
        }
        #endregion
    }
}
