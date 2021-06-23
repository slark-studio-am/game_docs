using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFloat :object {
    public float basePoint = default;
    public float appendPoint = default;
    public float appendRate = default;
    
    public static implicit operator GameFloat(float f) {
        return new GameFloat {basePoint = f, appendPoint = 0f, appendRate = 0f};
    }

    public static implicit operator float(GameFloat gf) {
        return gf.basePoint * (1+gf.appendRate/100f)+ gf.appendPoint;
    }

    public static GameFloat operator +(GameFloat a, GameFloat b) {
        return new GameFloat {basePoint = a.basePoint+b.basePoint, 
            appendPoint = a.appendPoint+b.appendPoint, 
            appendRate = a.appendRate+b.appendRate};
    }

    public float Percent() {
        return (float) this / 100f;
    }
}
