using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Collider2D col = null;

    [Header("Tiles type")]
    //Nature propeties
    [SerializeField] private Sprite[] desert;

    [SerializeField] private Sprite[] grass;
    [SerializeField] private Sprite[] woods;
    [SerializeField] private Sprite[] forest;

    //City propeties
    [SerializeField] private Sprite[] village;
    [SerializeField] private Sprite[] city;
    [SerializeField] private Sprite[] metropoly;

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
    }

    public void SetToNature()
    {
        if(isDesert)
        {
            isNature = true;
            isDesert = false;
            timer = Time.time;

            if (grass.Length > 0)
                rnd = (int)Random.Range(0, grass.Length);
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
                rnd = (int)Random.Range(0, village.Length);
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
                rnd = (int)Random.Range(0, desert.Length);
            else
                spriteRender.color = Color.yellow;
        }
    }

    public bool IsCity()
    {
        return isCity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            SetToNature();
    }
}
