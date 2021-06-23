using UnityEngine;
using TMPro;

public class PointBar : MonoBehaviour
{
    [SerializeField] private Transform backgroundBar = default;
    [SerializeField] private SpriteRenderer backgroundSprite = default;
    [SerializeField] private Transform foregroundBar = default;
    [SerializeField] private SpriteRenderer foregroundSprite = default;
    [SerializeField] private TextMeshPro _text = default;

    [Header("---- Config ----")]
    [SerializeField] private float aValue = default;
    [SerializeField] private float bValue = 1f;

    [SerializeField] private float scaleSpeed = 0.5f;
    [SerializeField] private float scaleTime = 0.1f;
    private float _scale = 1;
    private float _time  = 0f;

    private void UpdateBarName() {
        _text.SetText(aValue.ToString() + "/" + bValue.ToString());
    }

    private void UpdateBarPosition(float a, float b) { 
        _scale = a / b;

        if (_scale > foregroundBar.localScale.x) {
            backgroundBar.localScale = new Vector3(_scale, 1, 1);
        } else if (_scale < foregroundBar.localScale.x) {
            foregroundBar.localScale = new Vector3(_scale, 1, 1);
        }

        UpdateBarName();
    }

    public void UpdateBarPositionA(float a) {
        aValue = a;
        UpdateBarPosition(aValue, bValue);
    }

    public void UpdateBarPositionB(float b) {
        bValue = b;
        UpdateBarPosition(aValue, bValue);
    }

    public void UpdateBarColor(Color foregroundColor, Color backgroundColor) {
        foregroundSprite.color = foregroundColor;
        backgroundSprite.color = backgroundColor;
    }

    private void Update() {
        UpdateScale();
    }

    private void UpdateScale() {
        _time += Time.deltaTime;
        if (_time > scaleTime) {
            _time -= scaleTime;

            var localScaleX = foregroundBar.localScale.x;
            if (localScaleX < _scale) {
                foregroundBar.localScale = new Vector3(Mathf.Min(localScaleX + scaleSpeed, _scale), 1, 1);
            } else if (localScaleX > _scale) {
                foregroundBar.localScale = new Vector3(Mathf.Max(localScaleX - scaleSpeed, _scale), 1, 1);
            }

            localScaleX = backgroundBar.localScale.x;
            if (localScaleX < _scale) {
                backgroundBar.localScale = new Vector3(Mathf.Min(localScaleX + scaleSpeed, _scale), 1, 1);
            } else if (localScaleX > _scale) {
                backgroundBar.localScale = new Vector3(Mathf.Max(localScaleX - scaleSpeed, _scale), 1, 1);
            }
        }
    }
}
