using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SkillLifeSteal))]

public class DrawLifeStealAbilty : MonoBehaviour
{
    [SerializeField] private Image _rangeSkillimage;
    [SerializeField] private Image _cooldownSkillImage;

    private SkillLifeSteal _skillLifeSteal;

    private float _converteredFillAmount;
    private float _normalizedRange = 2.3f;

    private void Awake()
    {
        _skillLifeSteal = GetComponent<SkillLifeSteal>();
    }

    private void OnEnable()
    {
        _rangeSkillimage.transform.localScale = new Vector3(_skillLifeSteal.AbilityRange * _normalizedRange, _skillLifeSteal.AbilityRange * _normalizedRange, 0);
    }

    private void Update()
    {
        _converteredFillAmount = _skillLifeSteal.CurrentCooldown / _skillLifeSteal.Cooldown;
        _cooldownSkillImage.fillAmount = _converteredFillAmount;

        if (_skillLifeSteal.IsAbilityActive)
        {
            _rangeSkillimage.gameObject.SetActive(true);
        }
        else
        {
            _rangeSkillimage.gameObject.SetActive(false);
        }
    }
}