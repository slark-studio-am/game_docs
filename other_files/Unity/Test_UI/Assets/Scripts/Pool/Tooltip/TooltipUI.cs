using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour {
    [SerializeField] private Image image = default;
    [SerializeField] private TextMeshProUGUI captionText = default;
    [SerializeField] private TextMeshProUGUI messageText = default;
    // ----
    private Vector2 _follow;
    public void Show(Vector3 position, Vector2 follow, string caption,string message) {
        FixPositionInView(position);
        _follow = follow;
        
        this.gameObject.SetActive(true);

        captionText.SetText(caption);
        messageText.SetText(message);
    }

    private void Update() {
        if (!(_follow.x > 0f) && !(_follow.y > 0f)) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var position = this.transform.position;
        if (_follow.x > 0f)
            position.x = mousePosition.x;
        if (_follow.y > 0f)
            position.y = mousePosition.y;
        FixPositionInView(position);
    }

    private void FixPositionInView(Vector3 position) {
        var fixPosition = position;

        Vector2 sizeDelta = image.rectTransform.sizeDelta;
        Vector2 leftDown = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 rightUp = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (position.x + sizeDelta.x > rightUp.x)
            fixPosition.x -= sizeDelta.x;
        if (position.y - sizeDelta.y < leftDown.y)
            fixPosition.y += sizeDelta.y;
        this.transform.position = fixPosition;
    }
}
