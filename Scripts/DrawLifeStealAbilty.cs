using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SkillLifeSteal))]

public class DrawLifeStealAbilty : MonoBehaviour
{
    [SerializeField] private Image _rangeSkillimage;
    [SerializeField] private Image _cooldownSkillImage;

    private SkillLifeSteal _skillLifeSteal;

    private float _converteredFillAmount;

    private void Awake()
    {
        _skillLifeSteal = GetComponent<SkillLifeSteal>();
    }

    private void OnEnable()
    {
        float normalizedRange = 2.3f;

        _rangeSkillimage.transform.localScale = new Vector3(_skillLifeSteal.AbilityRange * normalizedRange, _skillLifeSteal.AbilityRange * normalizedRange, 0);
        _skillLifeSteal.ActiveAbilityChange += ChangeAbilityActive;
    }

    private void OnDisable()
    {
        _skillLifeSteal.ActiveAbilityChange -= ChangeAbilityActive;

    }

    private void Update()
    {
        _converteredFillAmount = _skillLifeSteal.CurrentCooldown / _skillLifeSteal.Cooldown;
        _cooldownSkillImage.fillAmount = _converteredFillAmount;
    }

    private void ChangeAbilityActive(bool isAbilityActive)
    {
        _rangeSkillimage.gameObject.SetActive(isAbilityActive);
    }
}