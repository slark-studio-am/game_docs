using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseCardList", menuName = "Card/List/Base Card List")]
public class BaseCardListSO : ListSO<BaseCardSO> {
    [TextArea] [SerializeField] private string dedscription = default;
}
