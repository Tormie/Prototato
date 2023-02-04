using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    public string tag;
    Color baseColor;
    Color activeColor;
    // Start is called before the first frame update
    void Start()
    {
        baseColor = new Color(1, 1, 1);
        activeColor = new Color(0.7f, 0.5f, 0.2f);
    }

    public void Click()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(tag))
        {
            Button b = g.GetComponent<Button>();
            ColorBlock cb = b.colors;
            cb.normalColor = baseColor;
            b.colors = cb;
        }
        Button c = GetComponent<Button>();
        ColorBlock cc = c.colors;
        cc.normalColor = activeColor;
        c.colors = cc;

    }

}
