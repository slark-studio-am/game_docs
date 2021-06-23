using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBarConfig : MonoBehaviour
{
    [SerializeField] private Color _backgroundColor = Color.gray;
    [SerializeField] private Color _foregroundColor = Color.black;
    [SerializeField] private PointBar _pointBar = default;

    private void Start() {
        _pointBar.UpdateBarColor(_foregroundColor, _backgroundColor);
    }

    public void UpdateBarPositionA(float a) {
        _pointBar.UpdateBarPositionA(a);
    }

    public void UpdateBarPositionB(float b) {
        _pointBar.UpdateBarPositionB(b);
    }
}

