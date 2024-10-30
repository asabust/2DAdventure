using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("InputDirection")]
    public Vector2 inputDirection;

    public float speed = 100;
    public float jumpForce = 16;

    public bool isHurt;
    public bool isDead;
    public bool isAttacking;
    public float hurtForce = 1;


    private PhysicsCheck _physicsCheck;
    private Rigidbody2D _rigidbody2D;
    private PlayerAnimation _playerAnimation;

    public PlayerInputControl InputControl;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;
        InputControl.Gameplay.Attack.started += Attack;
    }

    private void Update()
    {
        inputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isHurt)
            Move();
    }

    private void OnEnable()
    {
        InputControl.Enable();
    }

    private void OnDisable()
    {
        InputControl.Disable();
    }

    #region UnityEvent

    private void Move()
    {
        _rigidbody2D.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime,
            _rigidbody2D.velocity.y);
        if (inputDirection.x != 0) transform.localScale = new Vector3(Mathf.Sign(inputDirection.x), 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        // Debug.Log("Jump");
        if (_physicsCheck.isGrounded)
            _rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        _playerAnimation.PlayAttack();
        isAttacking = true;
    }

    #endregion

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        _rigidbody2D.velocity = Vector2.zero;
        var direction = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        _rigidbody2D.AddForce(direction * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        InputControl.Gameplay.Disable();
    }
}