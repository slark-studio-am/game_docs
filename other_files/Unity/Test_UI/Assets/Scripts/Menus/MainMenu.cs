using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject exitButton = default;
    [SerializeField] private GameObject loadButton = default;
    [SerializeField] private GameObject newButton = default;

    [Header("----Global SO----")]
    [SerializeField] private LoadEventChannelSO loadLocationChannel = default;

    [SerializeField] private GameSceneSO[] locationsToLoad = default;
    private void Start() {
        var systemName = SystemInfo.operatingSystem;
        if (systemName.StartsWith("iPhone OS") || systemName.StartsWith("Android OS"))
            if (exitButton != null)
                exitButton.SetActive(false);

        // TODO 判断存档
        bool hasSaver = false;
        if (hasSaver) {
            newButton.SetActive(false);
            loadButton.SetActive(false);
        } else {
            newButton.SetActive(true);
            loadButton.SetActive(false);
        }
    }

    public void ClickNewGameButton() {
        loadLocationChannel.RaiseEvent(locationsToLoad, false);
    }

    public void ClickContinueGameButton()
    {
        Debug.Log("TODO");
    }
}
