using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables
    List<GameObject> screens;

    string levelKey, currentLevelKey;

    #endregion

    private void Awake()
    {
        currentLevelKey = Strings.PlayerPrefsKeys.current_level.ToString();
        levelKey = Strings.PlayerPrefsKeys.level.ToString();

        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt(currentLevelKey))
            SceneManager.LoadScene(PlayerPrefs.GetInt(currentLevelKey));

        listenMethods();
        fillList();
    }

    private void Start()
    {
        TextMeshProUGUI txtLevel = transform.Find("txtLevel").GetComponentInChildren<TextMeshProUGUI>();
        txtLevel.text = "Level: " + (PlayerPrefs.GetInt(levelKey) + 1);
    }

    public void openScreen(string screenName)
    {
        if (screenName == null) PlayerController.isGameStart = true;

        foreach (GameObject screen in screens)
        {
            if (screen.name == screenName)
            {
                if (!screen.activeInHierarchy) screen.SetActive(true);
            }
            else
            {
                if (screen.activeInHierarchy) screen.SetActive(false);
            }
        }
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void nextLevel()
    {
        PlayerPrefs.SetInt(levelKey, PlayerPrefs.GetInt(levelKey) + 1);

        int sceneCount = SceneManager.sceneCount;

        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 1)
        {
            int randomLevelInx = Random.Range(0, sceneCount - 1);

            PlayerPrefs.SetInt(currentLevelKey, randomLevelInx);
            SceneManager.LoadScene(randomLevelInx);
        }
        else
        {
            PlayerPrefs.SetInt(currentLevelKey, SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void fillList()
    {
        screens = new List<GameObject>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Panel"))
        {
            screens.Add(item);
        }
    }

    void listenMethods()
    {
        ButtonClickListener.restartGame += restart;
        ButtonClickListener.nextLevel += nextLevel;
    }
}
