using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private float timeToDestroyForest = 1f;
    [SerializeField] private float speed;

    private Collider2D col = null;

    private Tile home = null;
    private Tile destiny = null;

    private bool isInTheForest = false;
    private bool canGoHome = false;

    private Cooldown destroyForestCD;

    // Start is called before the first frame update
    void Start()
    {
        destroyForestCD = new Cooldown(timeToDestroyForest);
        destroyForestCD.Stop();

        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canGoHome)
        {
            BackToHome();
            col.enabled = false;
        }
        else if(destroyForestCD.IsFinished)
        {
            destroyForestCD.Stop();
            canGoHome = true;
            col.enabled = true;
        }
        else if(!isInTheForest)
        {
            GoToForest();
        }
    }

    private void GoToForest()
    {
        if(Vector2.Distance(destiny.position, transform.position) < 0.1f)
        {
            isInTheForest = true;
            destroyForestCD.Start();
        }
        transform.position = Vector2.MoveTowards(transform.position, destiny.position, Time.deltaTime * speed);
    }

    private void BackToHome()
    {
        if (Vector2.Distance(home.position, transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, home.position, Time.deltaTime * speed);
    }

    public Tile SetHome
    {
        set
        {
            home = value;
        }
    }

    public Tile SetDestiny
    {
        set
        {
            destiny = value;
        }
    }
}
