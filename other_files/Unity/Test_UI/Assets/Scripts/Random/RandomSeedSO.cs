using System;
using UnityEngine;

public class GameRandom : System.Random {
    public GameRandom(int seed): base(seed) {
    }

    public float NextFloat() {
        // TODO 解决精度问题
        /*
        // https://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp/3365388
        var mantissa = (NextDouble() * 2.0) - 1.0;
        var exponent = Math.Pow(2.0, Next(-126, 127));
        return (float)(mantissa * exponent);
        */
        return (float)NextDouble();
    }

    // Returns a random float within [minInclusive..maxInclusive) (range is inclusive).
    public float Range(float minInclusive, float maxInclusive) {
        return minInclusive + NextFloat() * (maxInclusive - minInclusive);
    }
}

[CreateAssetMenu(fileName = "NewRandomSeed", menuName = "Random/RandomSeed")]
public class RandomSeedSO : ScriptableObject 
{
    [SerializeField] private int _rootSeed = default;

    private System.Random _rootRandom = default;
    private System.Random _initRandom = default;
    private GameRandom _unitRandom = default;
    private System.Random _skillRandom = default;
    private System.Random _mapRandom = default;
    private System.Random _eventRandom = default;
    private System.Random _artifactRandom = default;

    public int MainSeed => _rootSeed;
    public System.Random InitRandom => _initRandom;
    public GameRandom UnitRandom=> _unitRandom;
    public System.Random SkillRandom => _skillRandom;
    public System.Random MapRandom => _mapRandom;
    public System.Random EventRandom => _eventRandom;
    public System.Random ArtifactRandom => _artifactRandom;

    public RandomSeedSO() {
        #if UNITY_EDITOR
        ChangeMainSeed(System.DateTime.Now.Millisecond);
        #endif
    }

    public void ChangeMainSeed(int rootSeed) {
        _rootSeed = rootSeed;
        _rootRandom = new System.Random(_rootSeed);

        _initRandom = new System.Random(_rootRandom.Next());
        _unitRandom = new GameRandom(_rootRandom.Next());
        _skillRandom = new System.Random(_rootRandom.Next());
        _mapRandom = new System.Random(_rootRandom.Next());
        _eventRandom = new System.Random(_rootRandom.Next());
        _artifactRandom = new System.Random(_rootRandom.Next());
    }
}
