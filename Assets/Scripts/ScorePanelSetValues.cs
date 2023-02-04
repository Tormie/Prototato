using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class ScorePanelSetValues : MonoBehaviour
{
    [SerializeField]
    GameObject playerName;
    [SerializeField]
    GameObject playerScore;

    public void SetValues(string name, int score)
    {
        playerName.GetComponent<TextMeshProUGUI>().text = name;
        playerScore.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
