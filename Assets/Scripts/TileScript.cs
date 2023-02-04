using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    int tileType;
    [SerializeField]
    GameObject fogCube;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void DisableFog()
    {
        Destroy(fogCube);
    }
}
