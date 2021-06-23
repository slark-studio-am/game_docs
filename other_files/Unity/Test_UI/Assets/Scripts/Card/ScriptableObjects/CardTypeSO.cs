using UnityEngine;
using UnityEngine.Localization;

public enum CardType {
    None,
    UnitAttendant,
    UnitHero,
    UnitEquipment,
    Reward,
    Artifact,
    StrengthenBase,
    StrengthenAdvanced,
    Buff
}

[CreateAssetMenu(fileName = "NewCardType", menuName = "Card/Card Type")]
public class CardTypeSO : ScriptableObject {
    [SerializeField] private string typeName = default;
    [SerializeField] private LocalizedString typeLocalizedName = default;
    [SerializeField] private Sprite typeBackground = default;
    [SerializeField] private CardType cardType = default;

    public LocalizedString TypeLocalizedName => typeLocalizedName;
    public CardType Type => cardType;
    public Sprite BackgroundSprite => typeBackground;
}
