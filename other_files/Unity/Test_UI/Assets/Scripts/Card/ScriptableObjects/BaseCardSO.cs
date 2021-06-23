using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewBaseCard", menuName = "Card/Base Card")]
public class BaseCardSO : ScriptableObject
{
    [SerializeField] private int cardId = default;
    [Tooltip("卡牌名称")]
    [SerializeField] private string cardName = default;
    [SerializeField] private LocalizedString cardLocalizedName = default;
    [Tooltip("卡牌类型")]
    [SerializeField] private CardTypeSO cardType = default;
    [Tooltip("卡牌目标")]
    [SerializeField] private int cardTarget = default;
    [Tooltip("卡牌描述")]
    [TextArea][SerializeField] private string description = default;
    [SerializeField] private LocalizedString localizedDescription = default;
    [Tooltip("卡牌立绘")] 
    [SerializeField] private Sprite previewImage = default; 
    
    // ----
    public int CardId => cardId;
    public string CardName => cardName;
    public string Description => description;
    public CardTypeSO CardType => cardType; 
    public Sprite PreviewImage => previewImage;
}
