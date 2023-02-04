using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.Find("Canvas").GetComponent<PotatoPicker>();
        ol = GetComponent<Outline>();
        ol.enabled = false;
        potatoStr = GameObject.Find("GameEngine").GetComponent<GameRunner>().winnerPotatoStrength + Random.Range(-0.2f, 0.2f);
        potatoLen = GameObject.Find("GameEngine").GetComponent<GameRunner>().winnerPotatoLength + Random.Range(-0.2f, 0.2f);
        pLen.GetComponent<TextMeshProUGUI>().text = "Length: "+potatoLen;
        pStr.GetComponent<TextMeshProUGUI>().text = "Strenght: "+potatoStr;
    }

    public void SelectPotato()
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
            pi.pStats[pi.playerTurn-1, 0] = potatoLen;
            pi.pStats[pi.playerTurn - 1, 1] = potatoStr;
            if (pi.playerTurn < GameObject.Find("GameEngine").GetComponent<LevelGenerator>().playerAmount)
            {
                pi.playerTurn++;
            } else
            {
                pi.EnableButton();
            }
        }
    }

}
