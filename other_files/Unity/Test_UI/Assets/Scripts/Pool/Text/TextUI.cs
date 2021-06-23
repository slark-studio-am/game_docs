using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour {
    [SerializeField] private Text showText = default;
    [SerializeField] private Vector3 moveSpeed = default;
    [SerializeField] private TextUIEventChannelSO eventChannel;

    private void DestroySelf(float lifeTimer) {
        if (!float.IsPositiveInfinity(lifeTimer)) {
            StartCoroutine(ReturnSelf(lifeTimer));
        }
    }

    private  IEnumerator ReturnSelf(float lifeTimer) {
        yield return new WaitForSeconds(lifeTimer);
        eventChannel.RaiseReturnEvent(this.gameObject);
    }


    private void Update() {
        if (moveSpeed != Vector3.zero) {
            transform.position += moveSpeed* Time.deltaTime;
        }
    }

    public void Show(Vector3 position, string text, TextUIConfigurationSO settings) {
        this.transform.position = position;
        this.gameObject.SetActive(true);
        showText.text = text;

        if (settings) {
            DestroySelf(settings.lifeTimer);
            moveSpeed = settings.moveSpeed;
            showText.font = settings.textFont;
            showText.fontSize = settings.fontSize;
        }
    }
}
