using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorUI : MonoBehaviour
{
    private Color color;

    public void Start()
    {
        color = GetComponent<Image>().color;
    }
    public void Apply()
    {
        if(PlatformManager.instance != null)
        {
            PlatformManager.instance.ChangeColor(color);
        }
    }
}
