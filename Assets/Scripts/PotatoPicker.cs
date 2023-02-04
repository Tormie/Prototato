using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoPicker : MonoBehaviour
{
    GameObject ge;
    GameObject cv;
    [SerializeField]
    GameObject potatoOption;
    // Start is called before the first frame update
    void Start()
    {
        ge = GameObject.Find("GameEngine");
        cv = GameObject.Find("Canvas");
        Invoke("Init", 0.2f);
    }

    void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject b = Instantiate(potatoOption, new Vector3(0, 0, 0), Quaternion.identity);
            b.transform.SetParent(cv.transform);
            b.transform.localPosition = new Vector3(-720 + (i * 480), 0, 0);
        }
    }

}
