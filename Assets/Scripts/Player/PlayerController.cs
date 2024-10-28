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
    public float hurtForce = 1;


    private PhysicsCheck _physicsCheck;

    private Rigidbody2D _rigidbody2D;

    public PlayerInputControl InputControl;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;
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