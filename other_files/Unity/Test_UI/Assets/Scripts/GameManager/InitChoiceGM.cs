using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitChoiceGM : MonoBehaviour
{
    [SerializeField] private CardUI[] commonHeroCards;
    [SerializeField] private CardUI[] commonMonsterCards;
    [SerializeField] private CardUI[] commonRelicCards;
    [Header("----Global SO----")]
    [SerializeField] private RandomSeedSO randomSeed;
    [SerializeField] private UnitCardListSO unlockHeroUnitCardList;
    [SerializeField] private UnitCardListSO unlockMonsterUnitCardList;
    [SerializeField] private UnitCardListSO unlockRelicUnitCardList;
    
    [SerializeField] private UnitCardListSO myUnitCardList;
    [SerializeField] private IntEventChannelSO onCardClickChannel;
    [SerializeField] private VoidEventChannelSO onMyCardListInitChannel;
    
    private List<UnitCardSO> _heroCardList = default;
    private List<UnitCardSO> _monsterCardList = default;
    private List<UnitCardSO> _relicCardList = default;

    /*
    private void Start()
    {
        // 选中第一张
        var first = true;
        foreach (var card in commonHeroCards) {
            card.IsSelected = first;
            if (first) first = false;
        }

        first = true;
        foreach (var card in commonMonsterCards) {
            card.IsSelected = first;
            if (first) first = false;
        }
        
        first = true;
        foreach (var card in commonRelicCards) {
            card.IsSelected = first;
            if (first) first = false;
        }
        // 随机选牌
        _heroCardList = unlockHeroCardList.GetShuffleList(randomSeed.InitRandom, commonHeroCards.Length);
        _monsterCardList = unlockMonsterCardList.GetShuffleList(randomSeed.InitRandom, commonMonsterCards.Length);
        _relicCardList = unlockRelicCardList.GetShuffleList(randomSeed.InitRandom, commonRelicCards.Length);
        // Card 数据绑定
        var cardNum = 0;
        var length = Mathf.Min(_heroCardList.Count, commonHeroCards.Length);
        for (var i = 0; i < length; i++) {
            commonHeroCards[i].LinkCard(_heroCardList[i]);
            commonHeroCards[i].no = cardNum++;
        }
        length = Mathf.Min(_monsterCardList.Count, commonMonsterCards.Length);
        for (var i = 0; i < length; i++) {
            commonMonsterCards[i].LinkCard(_monsterCardList[i]);
            commonMonsterCards[i].no = cardNum++;
        }
        length = Mathf.Min(_relicCardList.Count, commonRelicCards.Length);
        for (var i = 0; i < length; i++) {
            commonRelicCards[i].LinkCard(_relicCardList[i]);
            commonRelicCards[i].no = cardNum++;
        }
        onCardClickChannel.OnEventRaised += OnCardClick;
    }

    private void OnDestroy() {
        onCardClickChannel.OnEventRaised -= OnCardClick;
    }

    private void OnCardClick(int no) {
        var check = commonHeroCards.Any(item => item.no == no);
        if (check) {
            foreach (var item in commonHeroCards) {
                item.IsSelected = item.no == no;
            }
            return;
        }

        check = commonMonsterCards.Any(item => item.no == no);
        if (check) {
            foreach (var item in commonMonsterCards) {
                item.IsSelected = item.no == no;
            }
            return;
        }
        
        check = commonRelicCards.Any(item => item.no == no);
        if (check) {
            foreach (var item in commonRelicCards) {
                item.IsSelected = item.no == no;
            }
            return;
        }
    }

    public void OnStartButtonClick() {
        myCardList.Clear();
        
        var heroCardListCount = _heroCardList.Count;
        for (var i = 0; i < commonHeroCards.Length; i++) {
            if (commonHeroCards[i].IsSelected && i < heroCardListCount) {
                myCardList.Add(_heroCardList[i]);
            }
        }
        
        var monsterCardListCount = _monsterCardList.Count;
        for (var i = 0; i < commonMonsterCards.Length; i++) {
            if (commonMonsterCards[i].IsSelected && i < monsterCardListCount) {
                myCardList.Add(_monsterCardList[i]);
            }
        }
        
        var relicCardListCount = _relicCardList.Count;
        for (var i = 0; i < commonRelicCards.Length; i++) {
            if (commonRelicCards[i].IsSelected && i < relicCardListCount) {
                myCardList.Add(_relicCardList[i]);
            }
        }
        
        onMyCardListInitChannel.RaiseEvent();
        gameObject.SetActive(false);
    }
    */
}
