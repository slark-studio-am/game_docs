using UnityEngine;

[CreateAssetMenu(fileName =  "NewRewardCard", menuName = "Card/Reward Card")]
public class RewardCardSO : BaseCardSO {
    [SerializeField] private int rewardSelectCount = default;
    [SerializeField] private int rewardCount = default;
    [SerializeField] private BaseCardSO rewardCardSource = default;
}
