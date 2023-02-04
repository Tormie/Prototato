using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int playerNumber = 2;
    int mapSize = 5;
    int rounds = 5;
    GameRunner gr;
    LevelGenerator lg;
    public void NewGame()
    {
        SceneManager.LoadScene("InitialPotatoPicker");
    }
    private void Start()
    {
        lg = GameObject.Find("GameEngine").GetComponent<LevelGenerator>();
        gr = GameObject.Find("GameEngine").GetComponent<GameRunner>();
    }

    public void SetMapSize(int size)
    {
        mapSize = size;
        lg.mapHeight = size;
        lg.mapWidth = size;
    }
    public void SetPlayers(int players)
    {
        playerNumber = players;
        lg.playerAmount = players;
    }
    public void SetRounds(int _rounds)
    {
        rounds = _rounds;
        gr.levelAmount = _rounds;
    }
}
