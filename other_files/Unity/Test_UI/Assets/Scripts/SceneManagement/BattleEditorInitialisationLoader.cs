using System.Collections.Generic;
using UnityEngine;

public class BattleEditorInitialisationLoader : MonoBehaviour
{
    [SerializeField] private UnitCardListSO enemyUnitCardList = default;
    [SerializeField] private UnitCardListSO allyUnitCardList = default;

    private List<Unit> _enemyUnitList = default;
    private List<Unit> _allyUnitList = default;


    private void Start() {
        /*
        int length = default;
        length = Mathf.Min(_enemySeats.Length, _enemySeatListSO.Length);
        for (int i = 0; i < length; i++)
            _enemySeats[i].LinkSO(_enemySeatListSO[i]);
        length = Mathf.Min(_allySeats.Length, _allySeatListSO.Length);
        for (int i = 0; i < length; i++)
            _allySeats[i].LinkSO(_allySeatListSO[i]);

        int index = default;
        index = 0;
        _enemyUnitList = new List<Unit>();
        foreach (CardSO item in _enemyCardList) {
            Unit unit = RequestUnit(item);
            _enemyUnitList.Add(unit);
            if (index < _enemySeatListSO.Length) {
                unit.LinkBattleSeat(_enemySeatListSO[index]);
                ++index;
            unit.LinkCard(item);
            }
        }
        index = 0;
        _allyUnitList = new List<Unit>();
        foreach (CardSO item in _allyCardList) {
            Unit unit = RequestUnit(item);
            _allyUnitList.Add(unit);
            if (index < _allySeatListSO.Length) {
                unit.LinkBattleSeat(_allySeatListSO[index]);
                ++index;
            unit.LinkCard(item);
            }
        }
        */
    }
}
