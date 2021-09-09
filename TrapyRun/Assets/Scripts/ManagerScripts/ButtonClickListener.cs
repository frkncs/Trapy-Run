using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickListener : MonoBehaviour
{
	#region Variables

	// Public Variables
	[HideInInspector] public static Action restartGame;
	[HideInInspector] public static Action nextLevel;

    // Private Variables
    UIController uc;

    #endregion

    private void Start()
    {
        uc = FindObjectOfType<UIController>().GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

			if (!PlayerController.isGameStart && clickedGO == null)
				uc.openScreen(null);
		}
		else if (Input.GetMouseButtonUp(0))
        {
			GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

            if (clickedGO == null) return;

            switch (clickedGO.name)
            {
                case "btnRetry":
                    restartGame();
                    break;
                case "btnNext":
                    nextLevel();
                    break;
                default:
                    break;
            }
        }
    }
}
