using UnityEngine;

public class Tile : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        
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
}
