using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private bool isDead = false;

        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        void Start()
        {
            CurrentHealth = maxHealth;

            if (healthBar != null)
            {
                healthBar.SetMaxHealth(maxHealth);
            }
        }

        public void TakeDamage(float number)
        {
            if (currentHealth > 0)
            {
                CurrentHealth -= number;

                if (healthBar != null)
                {
                    healthBar.SetHealth(CurrentHealth);
                }
            }

            if (currentHealth <= 0)
            {
                isDead = true;
            }
        }
    }
}