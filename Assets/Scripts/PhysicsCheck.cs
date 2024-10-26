using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public Vector2 bottomOffset;
    public bool isGrounded;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    // Update is called once per frame
    private void Update()
    {
        Check();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2) transform.localPosition + bottomOffset, checkRadius);
    }

    private void Check()
    {
        isGrounded =
            Physics2D.OverlapCircle((Vector2) transform.localPosition + bottomOffset, checkRadius, groundLayer);
    }
}