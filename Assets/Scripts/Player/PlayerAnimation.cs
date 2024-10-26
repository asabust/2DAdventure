using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PhysicsCheck PhysicsCheck;
    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
    }

    // Update is called once per frame

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        animator.SetFloat("velocityX", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetFloat("velocityY", rigidbody2D.velocity.y);
        animator.SetBool("isGround", PhysicsCheck.isGrounded);
    }
}