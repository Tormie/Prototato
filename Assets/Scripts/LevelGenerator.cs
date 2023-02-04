using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject spawnableTile;
    [SerializeField]
    Material mudMaterial;
    [SerializeField]
    Material fieldMaterial;
    [SerializeField]
    Material rockMaterial;
    [SerializeField]
    Material bushMaterial;
    [SerializeField]
    int mapWidth;
    [SerializeField]
    int mapHeight;
    int mudAmount, fieldAmount, rockAmount, bushAmount;
    List<int> tiles = new List<int>();

    Material m;
    string mTag;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTileList();
        GenerateMap();
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
                }
                else

                {
                    int k = tiles[Random.Range(0, tiles.Count)];

                    GameObject g = Instantiate(spawnableTile, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                    /*int k = Random.Range(0, 4);*/
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
    }
}
