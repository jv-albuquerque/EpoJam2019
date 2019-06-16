using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private float timeToDestroyForest = 1f;
    [SerializeField] private float speed;

    private BoxCollider2D col = null;

    private Vector2 home;
    private Tile destiny;

    private bool isInTheForest = false;
    private bool canGoHome = false;

    private Cooldown destroyForestCD;
    private Cooldown waitToGo;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();

        destroyForestCD = new Cooldown(timeToDestroyForest);
        waitToGo = new Cooldown(0.5f);
        waitToGo.Start();
        destroyForestCD.Stop();
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
            destiny.SetToDesert();
            canGoHome = true;
            col.enabled = true;
        }
        else if(!isInTheForest && waitToGo.IsFinished)
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
        if (Vector2.Distance(home, transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, home, Time.deltaTime * speed);
    }

    public Vector2 SetHome
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
