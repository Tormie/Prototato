using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Init", 0.1f);
    }

    void Init()
    {
        GameObject.Find("GameEngine").GetComponent<GameRunner>().Init();
        GameObject.Find("GameEngine").GetComponent<LevelGenerator>().Init();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
