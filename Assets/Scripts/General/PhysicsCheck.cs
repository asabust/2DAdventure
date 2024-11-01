using System;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("状态")]
    public bool isGrounded;

    public bool touchLeftWall;
    public bool touchRigntWall;


    private CapsuleCollider2D _capsule;

    private void Start()
    {
        _capsule = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2(_capsule.offset.x + _capsule.size.x / 2, _capsule.offset.y);
            leftOffset = new Vector2(_capsule.offset.x - _capsule.size.x / 2, _capsule.offset.y);
        }
    }

    private void Update()
    {
        Check();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2) transform.localPosition + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2) transform.localPosition + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2) transform.localPosition + rightOffset, checkRadius);
    }

    private void Check()
    {
        isGrounded =
            Physics2D.OverlapCircle((Vector2) transform.localPosition + bottomOffset, checkRadius, groundLayer);
        touchLeftWall =
            Physics2D.OverlapCircle((Vector2) transform.localPosition + leftOffset, checkRadius, groundLayer);
        touchRigntWall =
            Physics2D.OverlapCircle((Vector2) transform.localPosition + rightOffset, checkRadius, groundLayer);
    }
}