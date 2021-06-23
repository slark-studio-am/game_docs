using UnityEngine;

public class GameplayGM : MonoBehaviour
{
    [SerializeField] private GameObject commonUIGameObject;
    
    private void Start()
    {
        commonUIGameObject.SetActive(false);
    }
}
