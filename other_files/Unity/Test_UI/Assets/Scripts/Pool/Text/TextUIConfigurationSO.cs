using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/TextUI Configuration")]
public class TextUIConfigurationSO :ScriptableObject {
    [SerializeField] public float lifeTimer = default;
    [SerializeField] public Vector3 moveSpeed = default;

    [SerializeField] public Font textFont = default;
    [SerializeField] public int fontSize = default;
}
