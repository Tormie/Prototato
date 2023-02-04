using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject invisWall;
    [SerializeField]
    GameObject spawnableTile;
    [SerializeField]
    GameObject finish;
    [SerializeField]
    Material mudMaterial;
    [SerializeField]
    Material fieldMaterial;
    [SerializeField]
    Material rockMaterial;
    [SerializeField]
    Material bushMaterial;
    [SerializeField]
    Material finishMat;
    [SerializeField]
    public int mapWidth;
    [SerializeField]
    public int mapHeight;
    int mudAmount, fieldAmount, rockAmount, bushAmount;
    List<int> tiles = new List<int>();
    public int playerAmount = 1;
    public GameObject playerPrefab;
    public Material[] playerMats;
    GameRunner gr;

    static LevelGenerator instance;

    Material m;
    string mTag;

    public void Init()
    {
        gr = GetComponent<GameRunner>();
        if (SceneManager.GetActiveScene().name == "TileGen")
        {
            SpawnPlayers();
            GenerateTileList();
            GenerateMap();
        }
    }

    void SpawnPlayers()
    {
        for (int i = 0; i < playerAmount; i++)
        {
            GameObject p = Instantiate(playerPrefab, new Vector3(-1 + (i * 0.5f), 1, 1 - (i * 0.5f)), Quaternion.identity);
            p.GetComponent<PlayerMovement>().playerID = i + 1;
            p.GetComponent<MeshRenderer>().material = playerMats[i];
            p.GetComponent<PlayerStats>().legLength = gr.pStats[i, 0];
            Debug.Log(gr.pStats[i, 0]);
            Debug.Log(gr.pStats[i, 1]);
            p.GetComponent<PlayerStats>().legStrength = gr.pStats[i, 1];
        }
    }

    void GenerateTileList()
    {
        int totalTiles = mapWidth * mapHeight - 2;
        mudAmount = totalTiles / 6;
        bushAmount = totalTiles / 6;
        rockAmount = totalTiles / 6;
        fieldAmount = totalTiles / 6;
        for (int m = 0; m < mudAmount; m++)
        {
            tiles.Add(0);
        }
        for (int b = 0; b < bushAmount; b++)
        {
            tiles.Add(3);
        }
        for (int r = 0; r < rockAmount; r++)
        {
            tiles.Add(2);
        }
        for (int f = 0; f < fieldAmount; f++)
        {
            tiles.Add(1);
        }
        for (int rand = 0; rand < totalTiles - (mudAmount + fieldAmount + bushAmount + rockAmount); rand++)
        {
            tiles.Add(Random.Range(0, 4));
        }
    }

    void GenerateMap()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                if (i == 0 && j == 0)
                {

                }
                else if (i == mapWidth - 1 && j == mapHeight - 1)
                {
                    GameObject g = Instantiate(spawnableTile, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                    g.tag = "Standaard";
                    g.GetComponent<MeshRenderer>().material = finishMat;
                    Instantiate(finish, new Vector3(i * 4, 0.5f, j * 4), Quaternion.identity);
                }
                else

                {
                    int k = tiles[Random.Range(0, tiles.Count)];

                    GameObject g = Instantiate(spawnableTile, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                    switch (k)
                    {
                        case 0:
                            m = mudMaterial;
                            mTag = "Modder";
                            break;
                        case 1:
                            m = fieldMaterial;
                            mTag = "Akker";
                            break;
                        case 2:
                            m = rockMaterial;
                            mTag = "Rotsen";
                            break;
                        case 3:
                            m = bushMaterial;
                            mTag = "Struiken";
                            break;
                    }
                    g.GetComponent<MeshRenderer>().material = m;
                    g.tag = mTag;
                    tiles.Remove(k);
                }

            }
        }
        GameObject bw = Instantiate(invisWall, new Vector3(mapWidth * 2 - 2, 0, -2.5f), Quaternion.identity);
        bw.transform.localScale = new Vector3(mapWidth * 4, 10, 1);
        GameObject tw = Instantiate(invisWall, new Vector3(mapWidth * 2 - 2, 0, 18.5f), Quaternion.identity);
        tw.transform.localScale = new Vector3(mapWidth * 4, 10, 1);
        GameObject lw = Instantiate(invisWall, new Vector3(-2.5f, 0, mapWidth * 2 - 2), Quaternion.identity);
        lw.transform.localScale = new Vector3(1, 10, mapWidth * 4);
        GameObject rw = Instantiate(invisWall, new Vector3(18.5f, 0, mapWidth * 2 - 2), Quaternion.identity);
        rw.transform.localScale = new Vector3(1, 10, mapWidth * 4);
    }
}
