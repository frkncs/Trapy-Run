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

    private List<GameObject> screens;

    #endregion Variables

    private void OnEnable()
    {
        Actions.OpenScreen += OpenScreen;
    }

    private void OnDisable()
    {
        Actions.OpenScreen -= OpenScreen;
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt(Strings.currentLevel))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(Strings.currentLevel));
        }

        FillList();
    }

    private void Start()
    {
        TextMeshProUGUI txtLevel = transform.Find("txtLevel").GetComponentInChildren<TextMeshProUGUI>();
        txtLevel.text = "Level " + (PlayerPrefs.GetInt(Strings.level) + 1);
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt(Strings.level, PlayerPrefs.GetInt(Strings.level) + 1);

        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (SceneManager.GetActiveScene().buildIndex == sceneCount - 1)
        {
            int randomLevelInx = Random.Range(0, sceneCount - 1);

            PlayerPrefs.SetInt(Strings.currentLevel, randomLevelInx);
            SceneManager.LoadScene(randomLevelInx);
        }
        else
        {
            PlayerPrefs.SetInt(Strings.currentLevel, SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OpenScreen(string screenName)
    {
        if (screenName == null)
        {
            PlayerController.gameStart = true;
        }

        foreach (GameObject screen in screens)
        {
            if (screen.name == screenName)
            {
                if (!screen.activeInHierarchy)
                {
                    screen.SetActive(true);
                }
            }
            else
            {
                if (screen.activeInHierarchy)
                {
                    screen.SetActive(false);
                }
            }
        }
    }

    private void FillList()
    {
        screens = new List<GameObject>();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Panel"))
        {
            screens.Add(item);
        }
    }
}