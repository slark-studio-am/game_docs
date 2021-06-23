using UnityEngine;

[CreateAssetMenu(fileName = "NewArtifactCardList", menuName = "Card/List/Artifact Card List")]
public class ArtifactCardListSO : ListSO<ArtifactCardSO> {
    [TextArea] [SerializeField] private string description = default;
}
