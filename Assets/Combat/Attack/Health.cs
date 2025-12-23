using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int Damage)
    {
        if (health == 0) { return; }

        health = Mathf.Max(health - Damage, 0);
        Debug.Log(health);
    }
}
