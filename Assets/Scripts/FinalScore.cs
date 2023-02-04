using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour
{

    GameObject ge;
    [SerializeField]
    GameObject playerScorePanel;

    // Start is called before the first frame update
    void Start()
    {
        ge = GameObject.Find("GameEngine");
        //StartCoroutine(DisplayRanking());
        ShowScores();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ShowScores()
    {
        string playerName = "";
        for (int i = 0; i < ge.GetComponent<LevelGenerator>().playerAmount; i++)
        {
            switch (i)
            {
                case 0:
                    playerName = "Blue";
                    break;
                case 1:
                    playerName = "Red";
                    break;
                case 2:
                    playerName = "Green";
                    break;
                case 3:
                    playerName = "Yellow";
                    break;
            }
            GameObject g = Instantiate(playerScorePanel, new Vector3(0, 0, 0), Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
            g.transform.localPosition = new Vector3(Random.Range(-200, 201), (-175 + i * 200), 0);
            g.GetComponent<ScorePanelSetValues>().SetValues(playerName, ge.GetComponent<GameRunner>().playerScores[i]);
        }
    }


    public IEnumerator DisplayRanking()
    {
        string playerName = "";
        List<int> scorePlayerList = new List<int>();
        scorePlayerList.Add(0);
        for (int i = 1; i < ge.GetComponent<LevelGenerator>().playerAmount; i++)
        {
            for (int j = 0; j < scorePlayerList.Count; j++)
            {
                if (ge.GetComponent<GameRunner>().playerScores[i] > scorePlayerList[j])
                {
                    scorePlayerList.Insert(j, i);
                } else
                {
                    scorePlayerList.Add(i);
                    break;
                }
            }
        }
        scorePlayerList.Reverse();
        int scoreCounter = 0;
        for (int k = 0; k < scorePlayerList.Count; k++)
        {
            switch (scorePlayerList[k])
            {
                case 0:
                    playerName = "Blue";
                    break;
                case 1:
                    playerName = "Red";
                    break;
                case 2:
                    playerName = "Green";
                    break;
                case 3:
                    playerName = "Yellow";
                    break;
            }
            GameObject g = Instantiate(playerScorePanel, new Vector3(0, 0, 0), Quaternion.identity);
            g.transform.SetParent(gameObject.transform);
            g.transform.position = new Vector3(Random.Range(-200, 201), (-175 + scoreCounter * 200), 0);
            g.GetComponent<ScorePanelSetValues>().SetValues(playerName, ge.GetComponent<GameRunner>().playerScores[scorePlayerList[k]]);
            scoreCounter++;
        }
        yield return null;
    }

}
