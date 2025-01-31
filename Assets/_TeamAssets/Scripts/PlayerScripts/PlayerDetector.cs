using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private Transform[] hitPoints;
    [SerializeField] private float hitRadius = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private int damage = 2;
    [SerializeField] private CooldownUi cooldownUi;

    public int Damage => damage;

    public void DetectHit(int value, string type)
    {
        damage = value;
        foreach (Transform hitPoint in hitPoints)
        {
            Collider[] enemiesHit = Physics.OverlapSphere(hitPoint.position, hitRadius, enemyLayer);

            foreach (Collider enemy in enemiesHit)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    if (enemy.TryGetComponent<EnemyHealth>(out EnemyHealth enemyScript))
                    {
                        enemyScript.TakeDamage(damage);
                        cooldownUi.AddCooldown(value);
                        
                        if (type.Equals("normal"))
                            AudioManager.Instance.PlaySound(SoundType.Punch);
                        else if (type.Equals("special"))
                            AudioManager.Instance.PlaySound(SoundType.SpecialPower);

                    }
                    if (hitEffectPrefab != null)
                    {
                        GameObject hitEffect = Instantiate(hitEffectPrefab, hitPoint.position, Quaternion.identity);
                        hitEffect.transform.LookAt(enemy.transform.position);
                        Destroy(hitEffect, 1f);
                    }
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (hitPoints == null || hitPoints.Length == 0) return;

        Gizmos.color = Color.red;
        foreach (Transform hitPoint in hitPoints)
        {
            Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
        }
    }
}