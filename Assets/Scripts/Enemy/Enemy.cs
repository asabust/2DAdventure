using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("基本参数")]
    public float normalSpeed;

    public float dashSpeed;
    public float currentSpeed;
    public Vector3 direction;

    private Rigidbody2D rb;
    protected Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(-transform.localScale.x, 0, 0);
    }

    void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(normalSpeed * direction.x * Time.deltaTime, rb.velocity.y);
    }
}