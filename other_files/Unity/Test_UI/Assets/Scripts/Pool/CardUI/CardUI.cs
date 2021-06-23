using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI nameText = default;
    [SerializeField] private TextMeshProUGUI skill1Text = default;
    [SerializeField] private TextMeshProUGUI skill2Text = default;
    [SerializeField] private TextMeshProUGUI atkHpText = default;
    [SerializeField] private TextMeshProUGUI occupationText = default;
    [SerializeField] private Image backgroundImage = default;
    [SerializeField] private Image previewImage = default;
    
    [Header("Config")]
    [SerializeField]private float enlargeMultiple = 1.5f;

    [Header("SO")] 
    [SerializeField] private CardUIEventChannelSO cardUIEventChannel = default;
    [SerializeField] private CardTypeSO unitHeroCardType = default;
    
    
    // ----
    private BaseCardSO _card = default;
    
    
    public void LinkCard(BaseCardSO card) {
        nameText.SetText(card.CardName);
        skill1Text.SetText($"策略没提供");
        skill2Text.SetText($"策略没提供+1");
        occupationText.SetText($"策略没提供+2");
        backgroundImage.sprite = card.CardType.BackgroundSprite;
        previewImage.sprite = card.PreviewImage;

        _card = card;
        if (_card.CardType == unitHeroCardType) {
            LinkUnitCard((UnitCardSO)_card);
        }
    }

    private void LinkUnitCard(UnitCardSO card) {
        atkHpText.SetText($"ATK/{card.PhysicalPointMin},{card.PhysicalPointMax}\tHP/{card.HealthPointMax}");
    }

    public void OnPointerEnter() {
        var t = this.transform;
        var glg = t.parent.GetComponent<GridLayoutGroup>();
        if (!glg) return;
        t.localScale = new Vector3(enlargeMultiple, enlargeMultiple, 1f);
        var rectTransform = this.GetComponent<RectTransform>();
        var position = transform.position;
        position.y -= rectTransform.rect.y / enlargeMultiple;
        t.position = position;
        glg.SetLayoutHorizontal();
    }

    public void OnPointerExit() {
        var t = this.transform;
        var glg = t.parent.GetComponent<GridLayoutGroup>();
        if (!glg) return;
        var rectTransform = this.GetComponent<RectTransform>();
        
        var position = t.position;
        position.y += rectTransform.rect.y / enlargeMultiple;
        t.position = position;
        t.localScale = Vector3.one;
        glg.SetLayoutHorizontal();
        glg.SetLayoutVertical();
    }

    public void OnDrag() {
        var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        transform.position = p;
    }

    public void OnEndDrag() { 
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        var hits = Physics2D.RaycastAll(ray.origin, ray.direction, 10, -1); ;
        switch (hits.Length)
        {
            case 0:
                break;
            case 1:
                var hit = hits[0];
                var vacancyCard = hit.transform.GetComponent<VacancyCard>();
                if (vacancyCard)
                {
                    if (_card.CardType != unitHeroCardType) break;
                    var battleUnitCard = vacancyCard.battleUnitCard;
                    if (battleUnitCard.BattleUnitType != BattleUnitType.ally) break;
                    if (battleUnitCard.GetAvailable())
                    {
                    }
                    else {
                        battleUnitCard.gameObject.SetActive(true);
                        battleUnitCard.LinkCard((UnitCardSO) _card);
                            
                        this.transform.localScale = Vector3.one;
                        cardUIEventChannel.RaiseReturnEvent(this.gameObject);
                    }
                }
                else
                    Debug.Log(hit.transform.name);
                break;
        }
        
        OnPointerExit();
    }
}
