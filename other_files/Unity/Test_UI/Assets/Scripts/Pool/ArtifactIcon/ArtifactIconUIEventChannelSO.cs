using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/ArtifactIcon Event Channel")]
public class ArtifactIconUIEventChannelSO : ScriptableObject {
    [SerializeField] private PrefabPoolSO pool = default;
    // ----
    private Dictionary<Transform, Transform> _parentTable = default;

    public ArtifactIconUIEventChannelSO():base() {
        _parentTable = new Dictionary<Transform, Transform>();
    }

    public void RaiseRequestEvent(Transform iconRoot, ArtifactCardSO artifactCard) {
        var x = pool.Request();
        if (_parentTable.ContainsKey(x.transform))
            return;
        _parentTable.Add(x.transform, x.transform.parent);
        x.transform.SetParent(iconRoot);

        var iconUI = x.GetComponent<ArtifactIconUI>();
        iconUI.LinkCard(artifactCard);
        iconUI.gameObject.SetActive(true);
        
    }

    public void RaiseReturnEvent(GameObject member) {
        if (_parentTable.ContainsKey(member.transform)) {
            member.transform.SetParent(_parentTable[member.transform]);
            _parentTable.Remove(member.transform);
        }
        pool.Return(member);
    }
}
