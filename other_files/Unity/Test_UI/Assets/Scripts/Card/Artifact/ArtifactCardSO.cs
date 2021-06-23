using UnityEngine;
using UnityEngine.Events;

public enum ArtifactRarity {Common, Boss};

[CreateAssetMenu(fileName = "NewArtifactCard", menuName = "Card/Artifact Card")]
public class ArtifactCardSO : BaseCardSO {
    [SerializeField] private ArtifactRarity rarity = default;
    [SerializeField] private Sprite previewIcon = default;
    [Header("Parameter")] 
    [SerializeField] private BuffCardSO[] linkBuffCards = default;
    // ----
    public Sprite PreviewIcon => previewIcon;
    public BuffCardSO[] LinkBuffCards => linkBuffCards;
}
