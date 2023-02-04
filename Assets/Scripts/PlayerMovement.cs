using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public int playerID;
    string inputHAxis, inputVAxis;
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    float rockSpeedModifier = 0.5f;
    [SerializeField]
    float mudSpeedModifier = 0.5f;
    [SerializeField]
    float bushSpeedModifier = 0.5f;
    [SerializeField]
    float currentSpeedMod = 1;
    RaycastHit hitinfo;
    public List<GameObject> tiles = new List<GameObject>();
    bool isScanning = true;
    PlayerStats ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerStats>();
        switch (playerID)
        {
            case 1:
                inputHAxis = "Horizontal1";
                inputVAxis = "Vertical1";
                break;
            case 2:
                inputHAxis = "Horizontal2";
                inputVAxis = "Vertical2";
                break;
            case 3:
                inputHAxis = "Horizontal3";
                inputVAxis = "Vertical3";
                break;
            case 4:
                inputHAxis = "Horizontal4";
                inputVAxis = "Vertical4";
                break;
        }
        InvokeRepeating("RemoveFog",0, 0.2f);
        Invoke("FindTiles", 0.1f);
    }

    void FindTiles()
    {
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Akker"));
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Struiken"));
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Rotsen"));
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Modder"));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate((moveSpeed * currentSpeedMod * Input.GetAxis(inputHAxis) * Time.deltaTime), 0, (moveSpeed * currentSpeedMod * Input.GetAxis(inputVAxis) * Time.deltaTime));
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitinfo))
        {
            switch (hitinfo.collider.gameObject.tag)
            {
                case "Standaard":
                    currentSpeedMod = 1;
                    ps.isOnField = false;
                    break;
                case "Rotsen":
                    currentSpeedMod = Mathf.Clamp(rockSpeedModifier + ps.legLength / 2, 0.25f, 2);
                    ps.isOnField = false;
                    break;
                case "Struiken":
                    currentSpeedMod = Mathf.Clamp(bushSpeedModifier - ps.legLength / 2, 0.25f, 2);
                    ps.isOnField = false;
                    break;
                case "Modder":
                    currentSpeedMod = mudSpeedModifier + ps.legStrength / 2;
                    ps.isOnField = false;
                    break;
                case "Akker":
                    currentSpeedMod = 1;
                    ps.isOnField = true;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TeleportToTile();
        }

    }
    void RemoveFog()
    {
        if (isScanning)
        {
            Collider[] tiles = Physics.OverlapSphere(transform.position, 3);
            foreach (Collider c in tiles)
            {

                if (c.gameObject.name.Contains("Tile"))
                {
                    c.gameObject.GetComponent<TileScript>().DisableFog();
                }
            }
        }
    }

    void TeleportToTile()
    {
        GameObject tile = tiles[Random.Range(0, tiles.Count)];
        transform.position = tile.transform.position + new Vector3(0, 1.29f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(GameObject.Find("GameEngine").GetComponent<GameRunner>().FinishLevel(gameObject));
        }
    }
}
