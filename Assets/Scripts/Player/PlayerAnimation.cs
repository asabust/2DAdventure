using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PhysicsCheck _physicsCheck;
    private PlayerController _playerController;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        _animator.SetFloat("velocityX", Mathf.Abs(_rigidbody2D.velocity.x));
        _animator.SetFloat("velocityY", _rigidbody2D.velocity.y);
        _animator.SetBool("isGround", _physicsCheck.isGrounded);
        _animator.SetBool("isDead", _playerController.isDead);
        _animator.SetBool("isAttack", _playerController.isAttacking);
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger("hurt");
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("attack");
    }
}