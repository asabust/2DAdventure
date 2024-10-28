using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Character Stats")] public float health = 100f;

    [SerializeField] private float maxHealth = 100f;

    public bool invulnerable;
    public float invulnerableTime = 2f;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;
    private float invulnerableTimer;

    private void Start()
    {
        health = maxHealth;
        invulnerable = false;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer <= 0)
            {
                invulnerable = false;
                invulnerableTimer = invulnerableTime;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) return;
        if (health <= attacker.damage)
        {
            health = 0;
            Death();
            return;
        }

        Debug.Log($"{attacker.name}:{attacker.damage}");
        health -= attacker.damage;
        invulnerable = true;
        OnTakeDamage?.Invoke(attacker.transform);
    }


    private void TriggerInvulnerability()
    {
        if (invulnerable) return;
        invulnerable = true;
    }

    private void Death()
    {
        OnDeath?.Invoke();
    }
}