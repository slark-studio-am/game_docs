using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BuffType {
    Positive,
    Debuff
};

[CreateAssetMenu(fileName = "NewBuffCard", menuName = "Card/Buff Card")]
public class BuffCardSO : BaseCardSO {
    [SerializeField] private BuffType buffType = default;
    [SerializeField] private Sprite previewIcon = default;

    public Sprite PreviewIcon => previewIcon;
}
