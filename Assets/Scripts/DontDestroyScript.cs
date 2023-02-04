using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    GameObject instance;

    void Awake()
    {
        if (instance != null && instance != gameObject)
        {
            Debug.Log("Destroying this");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not destroying this");
            instance = gameObject;
        }
        DontDestroyOnLoad(instance);
    }
}
