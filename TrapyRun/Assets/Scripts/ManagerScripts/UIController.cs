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
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject tutorialUI;

    private List<GameObject> screens;

    #endregion Variables

    private void OnEnable()
    {
        SignUpEvents();
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != PlayerPrefs.GetInt(Strings.currentLevel))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(Strings.currentLevel));
        }
    }

    private void Start()
    {
        TextMeshProUGUI txtLevel = transform.Find("txtLevel").GetComponentInChildren<TextMeshProUGUI>();
        txtLevel.text = "Level " + (PlayerPrefs.GetInt(Strings.level) + 1);

        StartTutorial();
    }

    private void OnDisable()
    {
        SignOutEvents();
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

    private void StartTutorial()
    {
        tutorialUI.SetActive(true);
        StartCoroutine(TutorialLoop());
    }

    private void FinishTutorial()
    {
        Actions.TutorialFinishEvent?.Invoke();
        tutorialUI.SetActive(false);
        EnemyController.canRun = true;
    }

    private IEnumerator TutorialLoop()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FinishTutorial();
                break;
            }
            yield return null;
        }
    }

    private void OnWinEvent()
    {
        winUI.SetActive(true);
        SignOutEvents();
    }

    private void OnLoseEvent()
    {
        loseUI.SetActive(true);
        SignOutEvents();
    }

    private void SignUpEvents()
    {
        Actions.WinEvent += OnWinEvent;
        Actions.LoseEvent += OnLoseEvent;
    }

    private void SignOutEvents()
    {
        Actions.WinEvent -= OnWinEvent;
        Actions.LoseEvent -= OnLoseEvent;
    }
}