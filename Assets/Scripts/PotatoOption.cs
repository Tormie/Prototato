using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PotatoOption : MonoBehaviour
{
    public float potatoStr;
    public float potatoLen;
    [SerializeField]
    GameObject pLen;
    [SerializeField]
    GameObject pStr;
    Outline ol;
    bool isSelectedP1, isSelectedP2, isSelectedP3, isSelectedP4 = false;
    PotatoPicker pi;
    bool canClick = true;

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Canvas").GetComponent<PotatoPicker>();
        ol = GetComponent<Outline>();
        ol.enabled = false;
        if (SceneManager.GetActiveScene().name == "InitialPotatoPicker")
        {
        }
        else
        {
            potatoStr = GameObject.Find("GameEngine").GetComponent<GameRunner>().winnerPotatoStrength + Random.Range(-10, 11);
            potatoStr = Mathf.Clamp(potatoStr, -100, 100);
            potatoLen = GameObject.Find("GameEngine").GetComponent<GameRunner>().winnerPotatoLength + Random.Range(-10, 11);
            potatoLen = Mathf.Clamp(potatoLen, -100, 100);
        }
        pLen.GetComponent<TextMeshProUGUI>().text = "Length: "+potatoLen;
        pStr.GetComponent<TextMeshProUGUI>().text = "Strenght: "+potatoStr;
    }

    public void SelectPotato()
    {
        if (canClick)
        {
            if (!isSelectedP1 && !isSelectedP2 && !isSelectedP3 && !isSelectedP4)
            {
                switch (pi.playerTurn)
                {
                    case 1:
                        isSelectedP1 = true;
                        ol.enabled = true;
                        ol.effectColor = new Color(0, 0, 1);
                        break;
                    case 2:
                        isSelectedP2 = true;
                        ol.enabled = true;
                        ol.effectColor = new Color(1, 0, 0);
                        break;
                    case 3:
                        isSelectedP3 = true;
                        ol.enabled = true;
                        ol.effectColor = new Color(0, 1, 0);
                        break;
                    case 4:
                        isSelectedP4 = true;
                        ol.enabled = true;
                        ol.effectColor = new Color(1, 1, 0);
                        break;
                }
                pi.pStats[pi.playerTurn - 1, 0] = potatoLen;
                pi.pStats[pi.playerTurn - 1, 1] = potatoStr;
                if (pi.playerOrderIndex < GameObject.Find("GameEngine").GetComponent<LevelGenerator>().playerAmount-1)
                {
                    pi.playerOrderIndex++;
                    pi.playerTurn = pi.playerIndexOrder[pi.playerOrderIndex];
                }
                else
                {
                    canClick = false;
                    pi.EnableButton();
                }
            } else
            {
                canClick = false;
            }
        }
    }

}
