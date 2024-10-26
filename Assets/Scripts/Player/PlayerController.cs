using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 InputDirection;
    public float speed = 100;
    public float jumpForce = 16;

    public PlayerInputControl InputControl;
    private PhysicsCheck physicsCheck;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        InputControl = new PlayerInputControl();
        InputControl.Gameplay.Jump.started += Jump;
    }

    private void Update()
    {
        InputDirection = InputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
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
        rigidbody2D.velocity = new Vector2(InputDirection.x * speed * Time.deltaTime,
            rigidbody2D.velocity.y);
        if (InputDirection.x != 0) transform.localScale = new Vector3(Mathf.Sign(InputDirection.x), 1, 1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        // Debug.Log("Jump");
        if (physicsCheck.isGrounded)
            rigidbody2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}