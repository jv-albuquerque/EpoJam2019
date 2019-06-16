using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Collider2D col = null;

    [Header("Tiles type")]
    //Nature propeties
    [SerializeField] private Material[] desert;

    [SerializeField] private Material[] grass;

    //City propeties
    [SerializeField] private Material[] village;

    private SpriteRenderer spriteRender = null;

    private bool isCity = false;
    private bool isNature = false;
    private bool isDesert = true;

    private float timer;

    int rnd;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();

        if (desert.Length > 0)
        {
            rnd = (int)Random.Range(0, desert.Length);
            spriteRender.material = desert[rnd];
        }
        else
            spriteRender.color = Color.yellow;
    }

    public void SetToNature()
    {
        if(isDesert)
        {
            isNature = true;
            isDesert = false;
            timer = Time.time;

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

            if (desert.Length > 0)
            {
                rnd = (int)Random.Range(0, desert.Length);
                spriteRender.material = desert[rnd];
            }
            else
                spriteRender.color = Color.yellow;
        }
    }

    public bool IsCity()
    {
        return isCity;
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
}
