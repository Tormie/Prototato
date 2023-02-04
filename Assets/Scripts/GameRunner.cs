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


    private void Start()
    {
        pStats = new float[GetComponent<LevelGenerator>().playerAmount, 2];
    }

    public void Init()
    {
        //pStats = new float[GetComponent<LevelGenerator>().playerAmount, 2];
        winText = GameObject.Find("WinText");
        winText.SetActive(false);
    }

    public IEnumerator FinishLevel(GameObject winningPlayer)
    {
        winText.SetActive(true);
        winText.GetComponent<TextMeshProUGUI>().text = "Test Player Won!";
        winnerPotatoLength = winningPlayer.GetComponent<PlayerStats>().legLength;
        winnerPotatoStrength = winningPlayer.GetComponent<PlayerStats>().legStrength;
        yield return new WaitForSeconds(2);
        winText.SetActive(false);
        levelNumber++;
        if (levelNumber > levelAmount)
        {
            //Load final scene
        }
        else
        {
            
            SceneManager.LoadScene("PotatoPicker");
            //Load potato picker
        }
    }
}
