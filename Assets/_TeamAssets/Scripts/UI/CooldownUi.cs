using UnityEngine;
using UnityEngine.UI;

public class CooldownUi : MonoBehaviour
{
    public bool IsPowerSpecial { get; private set; }

    [SerializeField] private Image cooldownImage;
    [SerializeField] private float maxCooldown = 5f;
    [SerializeField] private GameObject effect;
    private float currentCooldown = 0f;
    
    public float CurrentCooldown
    {
        get => currentCooldown;
        private set
        {
            currentCooldown = Mathf.Clamp(value, 0f, maxCooldown);
            UpdateUI();
        }
    }

    public float MaxCooldown => maxCooldown;

    private void Start()
    {
        IsPowerSpecial = false;
        ResetCooldown();
    }

    void Update()
    {
        if(CurrentCooldown > 0 && CurrentCooldown < maxCooldown)
            CurrentCooldown -= Time.deltaTime;
    }

    public void AddCooldown(float cooldown)
    {
        if (CurrentCooldown < maxCooldown)
            CurrentCooldown += cooldown;
    }

    public void ResetCooldown()
    {
        IsPowerSpecial = false;
        effect.SetActive(false);
        CurrentCooldown = 0f;
    }

    private void UpdateUI()
    {
        cooldownImage.fillAmount = CurrentCooldown / maxCooldown;

        if (CurrentCooldown >= maxCooldown)
        {
            IsPowerSpecial = true;
            effect.SetActive(true);
        }
    }
}
