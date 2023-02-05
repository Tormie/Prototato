using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public int playerID;
    [SerializeField]
    string playerName;
    string inputHAxis, inputVAxis;
    [SerializeField]
    float moveSpeed = 5;
    float birdSpeed = 7;
    [SerializeField]
    float rockSpeedModifier = 0.25f;
    [SerializeField]
    float mudSpeedModifier = 0.25f;
    [SerializeField]
    float bushSpeedModifier = 0.25f;
    [SerializeField]
    float currentSpeedMod = 1;
    RaycastHit hitinfo;
    Slider strSlider, lenSlider;
    TextMeshProUGUI scoreTMP;
    public List<GameObject> tiles = new List<GameObject>();
    bool isScanning = true;
    public float distanceToFinish;
    PlayerStats ps;
    public bool birding = false;
    Transform birdTarget;
    bool isWalking = false;
    bool hasWon = false;
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
        switch (playerID)
        {
            case 1:
                strSlider = GameObject.Find("P1StrSlider").GetComponent<Slider>();
                lenSlider = GameObject.Find("P1LenSlider").GetComponent<Slider>();
                scoreTMP = GameObject.Find("P1Score").GetComponent<TextMeshProUGUI>();
                break;
            case 2:
                strSlider = GameObject.Find("P2StrSlider").GetComponent<Slider>();
                lenSlider = GameObject.Find("P2LenSlider").GetComponent<Slider>();
                scoreTMP = GameObject.Find("P2Score").GetComponent<TextMeshProUGUI>();
                break;
            case 3:
                strSlider = GameObject.Find("P3StrSlider").GetComponent<Slider>();
                lenSlider = GameObject.Find("P3LenSlider").GetComponent<Slider>();
                scoreTMP = GameObject.Find("P3Score").GetComponent<TextMeshProUGUI>();
                break;
            case 4:
                strSlider = GameObject.Find("P4StrSlider").GetComponent<Slider>();
                lenSlider = GameObject.Find("P4LenSlider").GetComponent<Slider>();
                scoreTMP = GameObject.Find("P4Score").GetComponent<TextMeshProUGUI>();
                break;
        }
        
        InvokeRepeating("RemoveFog",0, 0.2f);
        Invoke("FindTiles", 0.15f);
    }

    void FindTiles()
    {
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Struiken"));
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Rotsen"));
        tiles.AddRange(GameObject.FindGameObjectsWithTag("Modder"));
        scoreTMP.text = GameObject.Find("GameEngine").GetComponent<GameRunner>().playerScores[playerID - 1].ToString();
    }

    private void Update()
    {
        if (Input.GetAxis(inputHAxis) != 0 || Input.GetAxis(inputVAxis) != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if (isWalking)
        {
            GetComponent<Animation>().Play("Walking");
        } else if (hasWon)
        {
            GetComponent<Animation>().Play("Winning");
        } else
        {
            GetComponent<Animation>().Play("Idle");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!birding)
        {
            transform.Translate((moveSpeed * currentSpeedMod * Input.GetAxis(inputHAxis) * Time.deltaTime), 0, (moveSpeed * currentSpeedMod * Input.GetAxis(inputVAxis) * Time.deltaTime));
        } else
        {
            if (birdTarget == null)
            {
                birdTarget = tiles[Random.Range(0, tiles.Count)].transform;
            } else
            {
                transform.Translate((birdTarget.transform.position - transform.position).normalized * birdSpeed * Time.deltaTime);
                if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),new Vector2(birdTarget.transform.position.x, birdTarget.transform.position.z)) <= 0.25f)
                {
                    birding = false;
                    birdTarget = null;
                }
            }
        }
        if (Physics.Raycast(transform.position+new Vector3(0,0.5f,0), Vector3.down, out RaycastHit hitinfo))
        {
            switch (hitinfo.collider.gameObject.tag)
            {
                case "Standaard":
                    currentSpeedMod = 1;
                    ps.isOnField = false;
                    break;
                case "Rotsen":
                    currentSpeedMod = Mathf.Clamp(rockSpeedModifier + ps.legLength / 100, 0.25f, 2);
                    ps.isOnField = false;
                    break;
                case "Struiken":
                    currentSpeedMod = Mathf.Clamp(bushSpeedModifier - ps.legLength / 100, 0.25f, 2);
                    ps.isOnField = false;
                    break;
                case "Modder":
                    currentSpeedMod = Mathf.Clamp(mudSpeedModifier + ps.legStrength / 100, 0.25f, 2);
                    ps.isOnField = false;
                    break;
                case "Akker":
                    currentSpeedMod = 1;
                    ps.isOnField = true;
                    break;
            }
        }
        strSlider.value = ps.legStrength;
        lenSlider.value = ps.legLength;
        distanceToFinish = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Finish").transform.position);

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
        if (GameObject.Find("GameEngine").GetComponent<GameRunner>().playerFinished == false)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                StartCoroutine(GameObject.Find("GameEngine").GetComponent<GameRunner>().FinishLevel(gameObject));
                GameObject.Find("GameEngine").GetComponent<GameRunner>().playerFinished = true;
            }
        }
    }
}
