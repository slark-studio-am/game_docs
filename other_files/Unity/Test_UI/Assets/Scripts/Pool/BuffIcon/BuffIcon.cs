using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuffIcon : MonoBehaviour {
    [SerializeField] private SpriteRenderer iconSpriteRenderer = default;
    [SerializeField] private TextMeshPro layerText = default;
    // SO
    [SerializeField] private BuffSO buffSo = default;
    [SerializeField] private BuffIconEventChannelSO buffIconEventChannel = default;
    [SerializeField] private BuffCardSO cardSo = default;

    public BuffCardSO CardSo => cardSo;
    private BuffBase _buff;

    public UnityAction<BuffIcon> OnLayerCountChanged = default;

    public int LayerCount = default;

    public void Show(BuffCardSO card, Unit unit, float layerCount) {
        iconSpriteRenderer.sprite = card.PreviewIcon;

        _buff = buffSo.Register(card, unit, layerCount);
        _buff.OnLayerCountChanged += SetLayerText;
        SetLayerText();
        cardSo = card;
    }

    private void SetLayerText() {
        if (null == _buff) return;
        var layerCount = (int) (_buff.LayerCount > 0? _buff.LayerCount + 0.99f: 0);
        if (layerCount == LayerCount) return;
        LayerCount = layerCount;
        layerText.SetText(LayerCount.ToString());
        OnLayerCountChanged?.Invoke(this);
    }

    private void OnDisable() {
        if (null == _buff) return;
        _buff.OnLayerCountChanged -= SetLayerText;
        _buff.Unregister();
        _buff = null;
    }

    public void AddLayerCount(float layerCount) {
        _buff?.AddLayerCount(layerCount);
    }
}
