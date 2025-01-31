using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private PlayerDetector playerDetector;

    [SerializeField] private int maxHealth = 350;
    [SerializeField] private Image healthFill;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if(currentHealth <= 0 )
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        healthFill.fillAmount = (float)currentHealth / maxHealth;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} foi derrotado!");
    }
}
