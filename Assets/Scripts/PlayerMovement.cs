using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    int playerID;
    string inputHAxis, inputVAxis;
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    float legStrength = 0;
    [SerializeField]
    float legLength = 0;
    [SerializeField]
    float rockSpeedModifier = 0.5f;
    [SerializeField]
    float mudSpeedModifier = 0.5f;
    [SerializeField]
    float bushSpeedModifier = 0.5f;
    [SerializeField]
    float currentSpeedMod = 1;
    [SerializeField]
    float stealthMeter = 1;
    RaycastHit hitinfo;
    public List<GameObject> tiles = new List<GameObject>();
    bool isScanning = true;
    bool isOnField = false;
    [SerializeField]
    GameObject dangerShadow;
    SpriteRenderer shadowRenderer;
    Color shadowStartColor;
    // Start is called before the first frame update
    void Start()
    {
        shadowRenderer = dangerShadow.GetComponent<SpriteRenderer>();
        shadowStartColor = shadowRenderer.color;
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
        if (isOnField)
        {
            shadowRenderer.enabled = true;
            stealthMeter -= Time.deltaTime;
            float shadowAlpha = 0.4f + 0.6f * (1 - stealthMeter);
            shadowRenderer.color = new Color(0,0,0,shadowAlpha);
            dangerShadow.transform.localScale = new Vector3(5 - 4 * (1 - stealthMeter), 5 - 4 * (1 - stealthMeter), 5 - 4 * (1 - stealthMeter));
        } else
        {
            shadowRenderer.enabled = false;
            stealthMeter += Time.deltaTime;
        }
        stealthMeter = Mathf.Clamp(stealthMeter, 0, 1);
        transform.Translate((moveSpeed * currentSpeedMod * Input.GetAxis(inputHAxis) * Time.deltaTime), 0, (moveSpeed * currentSpeedMod * Input.GetAxis(inputVAxis) * Time.deltaTime));
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitinfo))
        {
            switch (hitinfo.collider.gameObject.tag)
            {
                case "Standaard":
                    currentSpeedMod = 1;
                    isOnField = false;
                    break;
                case "Rotsen":
                    currentSpeedMod = Mathf.Clamp(rockSpeedModifier + legLength / 2, 0.25f, 2);
                    isOnField = false;
                    break;
                case "Struiken":
                    currentSpeedMod = Mathf.Clamp(bushSpeedModifier - legLength / 2, 0.25f, 2);
                    isOnField = false;
                    break;
                case "Modder":
                    currentSpeedMod = mudSpeedModifier + legStrength / 2;
                    isOnField = false;
                    break;
                case "Akker":
                    currentSpeedMod = 1;
                    isOnField = true;
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
}
