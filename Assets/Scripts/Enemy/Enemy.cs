using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [Header("基本参数")]
    public float normalSpeed;

    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDirection;

    [Header("计时器")]
    public float waitTime;

    public float waitTimer;
    public bool waiting;

    private Rigidbody2D rb;
    protected Animator anim;
    private PhysicsCheck physicsCheck;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        faceDirection = new Vector3(-transform.localScale.x, 0, 0);
        if ((physicsCheck.touchLeftWall && faceDirection.x < 0) || (physicsCheck.touchRigntWall && faceDirection.x > 0))
        {
            waiting = true;
            anim.SetBool("isWalk", false);
        }

        Timer();
    }

    void FixedUpdate()
    {
        if (!waiting)
        {
            Move();
        }
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(normalSpeed * faceDirection.x * Time.deltaTime, rb.velocity.y);
    }

    public void TurnAround()
    {
        transform.localScale = new Vector3(faceDirection.x, 1, 1);
    }

    public void Timer()
    {
        if (waiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                TurnAround();
                waiting = false;
                waitTimer = waitTime;
            }
        }
    }
}