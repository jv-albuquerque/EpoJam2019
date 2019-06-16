using UnityEngine;

public class GameController : MonoBehaviour
{
    private PauseController pause;
    private Score score;

    private bool paused = false;
    private bool gameOver = false;

    private float horizontalMove;
    private float verticalMove;

    private PlayerMovement playerMovement = null;

    private CreateMap createMap;

    private Cooldown canLose;

    // Start is called before the first frame update
    void Start()
    {
        canLose = new Cooldown(5f);
        canLose.Start();

        createMap = GetComponent<CreateMap>();

        pause = GetComponent<PauseController>();
        score = GetComponent<Score>();

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (createMap.NumberOfGrass == 0 && canLose.IsFinished)
            GameOver();

        // Pause when the player click "Cancel" (by default, Esc)
        if (Input.GetButtonDown("Cancel") && !gameOver)
        {
            paused = !paused;
            pause.Pause();
        }

        // Where the games go, only can call if isn't paused and isn't gameover
        if (!paused && !gameOver)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        playerMovement.Move(horizontalMove * Time.fixedDeltaTime, verticalMove *Time.fixedDeltaTime);
    }

    /// <summary>
    /// Fuction made to update all the things when the game is over
    /// </summary>
    private void GameOver()
    {
        gameOver = true;
        pause.GameOver();
    }
}
