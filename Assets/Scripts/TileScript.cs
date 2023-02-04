using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    int tileType;
    [SerializeField]
    GameObject fogCube;
    [SerializeField]
    GameObject fogParticle;
    bool fogDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void DisableFog()
    {
        if (fogDisabled == false)
        {
            Destroy(fogCube);
            Instantiate(fogParticle, transform.position+ new Vector3(0,1,0), Quaternion.identity);
            fogDisabled = true;
        }
    }
}
