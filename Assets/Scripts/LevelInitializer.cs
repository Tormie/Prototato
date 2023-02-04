using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitializer : MonoBehaviour
{
    GameObject p1Panel, p2Panel, p3Panel, p4Panel;
    // Start is called before the first frame update
    void Start()
    {
        p1Panel = GameObject.Find("P1Panel");
        p2Panel = GameObject.Find("P2Panel");
        p3Panel = GameObject.Find("P3Panel");
        p4Panel = GameObject.Find("P4Panel");
        p3Panel.SetActive(false);
        p4Panel.SetActive(false);
        Invoke("Init", 0.1f);
    }

    void Init()
    {
        GameObject.Find("GameEngine").GetComponent<GameRunner>().Init();
        GameObject.Find("GameEngine").GetComponent<GameRunner>().SetPScores();
        GameObject.Find("GameEngine").GetComponent<LevelGenerator>().Init();
        switch (GameObject.Find("GameEngine").GetComponent<LevelGenerator>().playerAmount)
        {
            case 4:
                p4Panel.SetActive(true);
                p3Panel.SetActive(true);
                break;
            case 3:
                p3Panel.SetActive(true);
                break;
            case 2:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
