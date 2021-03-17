using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Enemy : MonoBehaviour, IsAttackable
{
    [Header("UI")]
    [SerializeField] private Image healthBar;

    [Header("Combat Fields")]
    [SerializeField] private float maxHP = 100.0f;
    private float currentHP;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    // ------
    // COMBAT
    // ------
    public void TakeDamage(float damage)
    {
        Debug.Log("Damage received: " + damage);
        currentHP -= damage;
        Debug.Log("Current HP" + currentHP);

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHP / maxHP;
    }
}
