using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public float range;
    public float attackRadius;
    public float attackRate;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log(other.name);
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}