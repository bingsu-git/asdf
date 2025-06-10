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
        Debug.Log($"{gameObject.name} �ǰ�! ���� ü��: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{gameObject.name} ���! �� �̺�Ʈ Ʈ���� ����");
        OnAnyMonsterDeath?.Invoke();
        Debug.Log($"{gameObject.name} �� �̺�Ʈ Ʈ���� �Ϸ�");
        Destroy(gameObject);
    }
}
