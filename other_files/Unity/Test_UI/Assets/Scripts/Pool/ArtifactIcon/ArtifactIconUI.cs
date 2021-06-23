using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

public class ArtifactIconUI : MonoBehaviour {
    [SerializeField] private Image iconImage = default;
    [Header("SO")] 
    [SerializeField] private TooltipUIEventChannelSO tooltipUIEventChannel;
    // ----
    private string _caption = default;
    private string _message = default;
    private GameObject _tooltipUI = default;

    public void LinkCard(ArtifactCardSO artifactCard) {
        iconImage.sprite = artifactCard.PreviewIcon;

        _caption = artifactCard.CardName;
        _message = artifactCard.Description;
    }

    public void OnPointerEnter() {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z -= Camera.main.transform.position.z;
        position.y = this.transform.position.y - iconImage.rectTransform.sizeDelta.y /2f + 1f;
        _tooltipUI = tooltipUIEventChannel.RaiseRequestEvent(position,new Vector2(1,0), _caption, _message);
    }
    
    public void OnPointerExit() {
        tooltipUIEventChannel.RaiseReturnEvent(_tooltipUI);
    }
}
