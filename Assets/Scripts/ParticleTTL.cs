using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTTL : MonoBehaviour
{
    float ttl = 1;

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0)
        {
            Destroy(gameObject);
        }
    }
}
