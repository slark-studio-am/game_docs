using UnityEngine;

[CreateAssetMenu(menuName = "Events/CardUI Event Channel")]
public class CardUIEventChannelSO : ScriptableObject {
    [SerializeField] private PrefabPoolSO pool = default;

    public void RaiseRequestEvent(BaseCardSO card, Transform parentRoot) {
        var x = pool.Request();
        var cardUI = x.GetComponent<CardUI>();
        cardUI.LinkCard(card);
        x.transform.SetParent(parentRoot);
        x.SetActive(true);
    }

    public void RaiseReturnEvent(GameObject member) {
        pool.Return(member);
    }

    public bool RaiseContainsEvent(GameObject member) {
        return pool.Contains(member);
    }
}
