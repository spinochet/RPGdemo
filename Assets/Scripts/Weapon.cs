using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attack;

    void OnTriggerEnter(Collider collider)
    {
        IsAttackable other = collider.GetComponent<IsAttackable>();
        Enemy enemy = collider.GetComponent<Enemy>();
        if (other != null && enemy != null)
        {
            other.TakeDamage(attack);
        }
    }
}
