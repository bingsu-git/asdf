using UnityEngine;
using System;

public class MonsterHealth : MonoBehaviour
{
    public static event Action OnAnyMonsterDeath;

    public int maxHealth = 3;
    private int currentHealth;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} 피격! 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{gameObject.name} 사망! → 이벤트 트리거 시작");
        OnAnyMonsterDeath?.Invoke();
        Debug.Log($"{gameObject.name} → 이벤트 트리거 완료");
        Destroy(gameObject);
    }
}
