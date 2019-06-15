using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float moveSpeed = 40f;

    private Rigidbody2D rb2D = null;
    private Vector2 velocity = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontal, float vertical)
    {
        Vector2 targetVelocity = new Vector2(horizontal * moveSpeed * 10, vertical * moveSpeed * 10);
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
    }
}
