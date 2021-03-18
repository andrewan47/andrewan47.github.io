using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private static Image HealthBarImage;

    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    public static void setHealthBarValue(int hp, int max)
    {
        float bar = (float)hp / max;
        Debug.Log(bar);
        HealthBarImage.fillAmount = bar;
    }
}
