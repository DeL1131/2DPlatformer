using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private float _smoothDecreaseDuration = 0.5f;
    [SerializeField] private Health _health;

    private Image _image;

    private void OnEnable()
    {
        _health.HealhChanged += TakeDamage;
    }

    private void OnDisable()
    {
        _health.HealhChanged -= TakeDamage;
    }

    private void Start()
    {
        _healthImage.fillAmount = _health.MaxHealth;
    }

    private void TakeDamage(float currentHealth)
    {
        float normalizedHealth = currentHealth / _health.MaxHealth;
        StartCoroutine(DecreaseHealthSmoothy(normalizedHealth));
    }

    private IEnumerator DecreaseHealthSmoothy(float target)
    {
        float elapsedTime = 0f;
        float previousValve = _healthImage.fillAmount;

        while (elapsedTime < _smoothDecreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedPosition = elapsedTime / _smoothDecreaseDuration;
            float intermediateValue = Mathf.Lerp(previousValve, target, normalizedPosition);
            _healthImage.fillAmount = intermediateValue;

            yield return null;
        }
    }
}