using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meterbar : MonoBehaviour
{
    private static Image MeterBarImage;

    // Start is called before the first frame update
    void Start()
    {
        MeterBarImage = GetComponent<Image>();
    }

    public static void setMeterBarValue(int meter, int max)
    {
        float bar = (float)meter / max;
        //Debug.Log(bar);
        MeterBarImage.fillAmount = bar;
    }
}
