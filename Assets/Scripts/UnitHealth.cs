using UnityEngine;

namespace DefaultNamespace
{
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HealthBar healthBar;
        public bool isDead = false;

        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        void Start()
        {
            foreach (Transform thing in transform)
            {
                if (thing.transform.name == "Canvas")
                {
                    healthBar = thing.Find("Health Bar").GetComponent<HealthBar>();
                }
            }

            CurrentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }
        
        public void TakeDamage(float number)
        {
            if (currentHealth > 0)
            {
                CurrentHealth -= number;
                healthBar.SetHealth(CurrentHealth);
            }

            if (currentHealth <= 0)
            {
                isDead = true;
            }
        }
        
        
        public void Heal(float number)
        {
            if (CurrentHealth >= maxHealth)
            {
                //TODO nachricht leben voll
                return;
            }
            if (CurrentHealth > 0)
            {
                if (CurrentHealth + number <= maxHealth)
                {
                    currentHealth += number;
                }
                else
                {
                    currentHealth = maxHealth;
                }
                healthBar.SetHealth(CurrentHealth);
            }
            
        }
    }
}