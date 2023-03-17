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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                TakeDamage(20);
            }
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
    }
}