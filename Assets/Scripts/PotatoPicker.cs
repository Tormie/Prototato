using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PotatoPicker : MonoBehaviour
{
    GameObject ge;
    GameObject cv;
    [SerializeField]
    GameObject potatoOption;
    public int playerTurn = 1;
    public float[,] pStats;
    [SerializeField]
    GameObject nextLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        pStats = new float[GameObject.Find("GameEngine").GetComponent<LevelGenerator>().playerAmount, 2];
        nextLevelButton.GetComponent<Button>().interactable = false;
        ge = GameObject.Find("GameEngine");
        cv = GameObject.Find("Canvas");
        Invoke("Init", 0.2f);
    }

    void Init()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject b = Instantiate(potatoOption, new Vector3(0, 0, 0), Quaternion.identity);
            b.transform.SetParent(cv.transform);
            b.transform.localPosition = new Vector3(-720 + (i * 480), 0, 0);
        }
    }

    public void EnableButton()
    {
        foreach (float f in pStats)
        {
            Debug.Log(f);
        }
        for (int i = 0; i < GameObject.Find("GameEngine").GetComponent<LevelGenerator>().playerAmount; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                ge.GetComponent<GameRunner>().pStats[i, j] = pStats[i, j];
            }
        }
        ge.GetComponent<GameRunner>().pStats = pStats;
        nextLevelButton.GetComponent<Button>().interactable = true;
    }
    public void NextLevel()
    {
        
        SceneManager.LoadScene("TileGen");
    }
}
