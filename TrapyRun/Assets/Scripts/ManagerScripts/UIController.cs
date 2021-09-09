using Assets.Scripts;
using System.Collections;
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

    #endregion

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt(Strings.currentLevelInx))
            SceneManager.LoadScene(PlayerPrefs.GetInt(Strings.currentLevelInx));

        ButtonClickListener.restartGame += restart;
        ButtonClickListener.nextLevel += nextLevel;

        screens = new List<GameObject>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Panel"))
        {
            screens.Add(item);
        }
    }

    private void Start()
    {
        TextMeshProUGUI txtLevel = transform.Find("txtLevel").GetComponentInChildren<TextMeshProUGUI>();
        txtLevel.text = "Level: " + (PlayerPrefs.GetInt(Strings.level) + 1);
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
        PlayerPrefs.SetInt(Strings.level, PlayerPrefs.GetInt(Strings.level) + 1);

        int sceneCount = SceneManager.sceneCount;

        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 1)
        {
            int randomLevelInx = Random.Range(0, sceneCount - 1);

            PlayerPrefs.SetInt(Strings.currentLevelInx, randomLevelInx);
            SceneManager.LoadScene(randomLevelInx);
        }
        else
        {
            PlayerPrefs.SetInt(Strings.currentLevelInx, SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
