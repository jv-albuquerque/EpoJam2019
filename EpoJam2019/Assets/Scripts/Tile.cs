using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Collider2D col = null;
    [SerializeField] private GameObject cityObj = null;

    [Header("Tiles type")]
    //Nature propeties
    [SerializeField] private Material[] desert;

    [SerializeField] private Material[] grass;

    //City propeties
    [SerializeField] private Material[] village;

    [Header("City Propeties")]
    [SerializeField] private GameObject human = null;
    [SerializeField] private float timeToSpawnHuman = 5f;
    [SerializeField] private float timeToTransformToCity = 3f;


    private CreateMap createMap = null;

    private Cooldown spawnHumanCD;
    private Cooldown transformToCityCD;

    private List<Tile> grassCanGo;

    private SpriteRenderer spriteRender = null;

    private bool isCity = false;
    private bool isNature = false;
    private bool isDesert = true;

    private bool canBeCity = false;

    private float timer;

    int rnd;

    // Start is called before the first frame update
    void Start()
    {
        grassCanGo = new List<Tile>();

        createMap = GameObject.FindGameObjectWithTag("GameController").GetComponent<CreateMap>();

        spawnHumanCD = new Cooldown(timeToSpawnHuman);
        spawnHumanCD.Start();

        transformToCityCD = new Cooldown(timeToTransformToCity);
        transformToCityCD.Stop();

        spriteRender = GetComponent<SpriteRenderer>();

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z));

        if (desert.Length > 0)
        {
            rnd = (int)Random.Range(0, desert.Length);
            spriteRender.material = desert[rnd];
        }
        else
            spriteRender.color = Color.yellow;
    }

    private void Update()
    {
        if(isCity && spawnHumanCD.IsFinished)
        {
            Tile tile;

            if (createMap.FindGrass((int)transform.position.x, (int)transform.position.y, out tile))
            {
                GameObject newHuman = Instantiate(human, transform.position, Quaternion.identity);

                newHuman.GetComponent<Human>().SetHome = transform.position;

                newHuman.GetComponent<Human>().SetDestiny = tile;
            }
            else if(createMap.SetToDesert((int)transform.position.x, (int)transform.position.y, out tile))
            {
                GameObject newHuman = Instantiate(human, transform.position, Quaternion.identity);

                newHuman.GetComponent<Human>().SetHome = transform.position;

                newHuman.GetComponent<Human>().SetDestiny = tile;
            }

            spawnHumanCD.Restart();
        }
        else if (isDesert && transformToCityCD.IsFinished)
        {
            SetToCity();
        }
    }

    public void SetToNature()
    {
        if(isDesert)
        {
            isNature = true;
            isDesert = false;
            timer = Time.time;

            createMap.AddGrass = this;

            if (grass.Length > 0)
            {
                rnd = (int)Random.Range(0, grass.Length);
                spriteRender.material = grass[rnd];
            }
            else
                spriteRender.color = Color.green;
        }
    }

    public void SetToCity()
    {
        if (!isCity)
        {
            
            isCity = true;
            isNature = false;
            isDesert = false;
            timer = Time.time;
            col.enabled = true;

            cityObj.SetActive(true);

            if (village.Length > 0)
            {
                rnd = (int)Random.Range(0, village.Length);
                spriteRender.material = village[rnd];
            }
            else
                spriteRender.color = Color.grey;
        }
    }

    public void SetToDesert()
    {
        if(isNature)
        {
            isDesert = true;
            isCity = false;
            isNature = false;

            canBeCity = true;
            transformToCityCD.Start();

            createMap.RemoveGrass = this;

            if (desert.Length > 0)
            {
                rnd = (int)Random.Range(0, desert.Length);
                spriteRender.material = desert[rnd];
            }
            else
                spriteRender.color = Color.yellow;
        }
    }

    public bool IsDesert
    {
        get
        {
            return isDesert;
        }
    }

    public bool IsCity
    {
        get
        {
            return isCity;
        }
    }

    public bool IsNature
    {
        get
        {
            return isNature;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetToNature();
    }

    public Vector2 position
    {
        get
        {
            return transform.position;
        }
    }


}
