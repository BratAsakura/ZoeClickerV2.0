using System.Collections;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private float _speed = 100f;

    public IEnumerator Show(float damage)
    {
        _text.SetText(NumberFormatter.Format(damage));
        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = transform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = startPos + Vector3.up * elapsed * _speed;
            yield return null;
        }

        Destroy(gameObject);
    }
}
