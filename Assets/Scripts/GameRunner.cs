using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{
    bool isPaused = false;
    public int levelNumber = 1;
    public int levelAmount = 5;
    [SerializeField]
    GameObject winText;
    public float winnerPotatoLength;
    public float winnerPotatoStrength;
    public float[,] pStats;
    public int[] playerScores;
    public int[] playerIndexOrder;
    bool areScoresInit = false;
    public List<GameObject> playerOrder;


    private void Start()
    {
        pStats = new float[GetComponent<LevelGenerator>().playerAmount, 2];
    }

    public void Init()
    {
        winText = GameObject.Find("WinText");
        winText.SetActive(false);
    }

    public void SetPScores()
    {
        if (!areScoresInit)
        {
            playerScores = new int[GetComponent<LevelGenerator>().playerAmount];
            areScoresInit = true;
        }
    }

    public IEnumerator FinishLevel(GameObject winningPlayer)
    {
        CalculateScores(winningPlayer);
        winText.SetActive(true);
        winText.GetComponent<TextMeshProUGUI>().text = "Test Player Won!";
        winnerPotatoLength = winningPlayer.GetComponent<PlayerStats>().legLength;
        winnerPotatoStrength = winningPlayer.GetComponent<PlayerStats>().legStrength;
        yield return new WaitForSeconds(2);
        winText.SetActive(false);
        levelNumber++;
        if (levelNumber >= levelAmount)
        {
            //Load final scene
        }
        else
        {
            
            SceneManager.LoadScene("PotatoPicker");
            //Load potato picker
        }
    }

    void CalculateScores(GameObject winningPlayer)
    {
        playerOrder = new List<GameObject>();
        playerIndexOrder = new int[GetComponent<LevelGenerator>().playerAmount];
        playerOrder.Add(winningPlayer);
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (g != winningPlayer)
            {
                foreach (GameObject f in playerOrder)
                {
                    if (g.GetComponent<PlayerMovement>().distanceToFinish < f.GetComponent<PlayerMovement>().distanceToFinish)
                    {
                        playerOrder.Insert(playerOrder.IndexOf(f), g);
                    }
                    else
                    {
                        playerOrder.Add(g);
                        break;
                    }
                }
            }
        }
        Debug.Log(playerOrder.Count);
        for (int i = 0; i < playerOrder.Count; i++)
        {
            playerIndexOrder[i] = playerOrder[i].GetComponent<PlayerMovement>().playerID;
            playerScores[playerOrder[i].GetComponent<PlayerMovement>().playerID - 1] += (3 - i);
        }
    }
}
