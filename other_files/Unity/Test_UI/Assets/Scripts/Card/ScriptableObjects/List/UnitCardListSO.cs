using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitCardList", menuName = "Card/List/Unit Card List")]
public class UnitCardListSO : ListSO<UnitCardSO> {
    [TextArea] [SerializeField] private string description;
}
