using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetGroupScript : MonoBehaviour
{
    GameObject[] players;
    CinemachineTargetGroup targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Init", 0.15f);
    }

    void Init()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        targetGroup = GameObject.Find("TargetGroup1").GetComponent<CinemachineTargetGroup>();
        foreach (GameObject p in players)
        {
            targetGroup.AddMember(p.transform, 1, 0);
        }
    }
}
