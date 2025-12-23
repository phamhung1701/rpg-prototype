using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Collider player;
    [SerializeField] private int damage;
    private List<Collider> damaged = new List<Collider>();

    private void OnEnable()
    {
        damaged.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == player) { return; }
        if (damaged.Contains(other)) { return; }

        damaged.Add(other);
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
