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
    public float hurtFource = 4;

    private Transform target;

    [Header("计时器")]
    public float waitTime;

    private float waitTimer;
    public bool waiting;

    [Header("状态")]
    public bool isHurt;


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
        if (!waiting && !isHurt)
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

    public void OnTakeDamage(Transform attacker)
    {
        target = attacker;
        transform.localScale = attacker.position.x - transform.position.x > 0
            ? new Vector3(-1, 1, 1)
            : new Vector3(1, 1, 1);
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(dir * hurtFource, ForceMode2D.Impulse);
        StartCoroutine(OnHurt());
    }

    IEnumerator OnHurt()
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetTrigger("die");
    }

    public void DestryOnDeath()
    {
        Destroy(this.gameObject);
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