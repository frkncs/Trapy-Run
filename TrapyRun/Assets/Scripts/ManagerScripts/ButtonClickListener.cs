using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickListener : MonoBehaviour
{
	#region Variables

	// Public Variables

    // Private Variables
    UIController uic;

    #endregion

    private void Start()
    {
        uic = FindObjectOfType<UIController>().GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

			if (!PlayerController.isGameStart && clickedGO == null)
				uic.openScreen(null);
		}
		else if (Input.GetMouseButtonUp(0))
        {
			GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

            if (clickedGO == null) return;

            switch (clickedGO.name)
            {
                case "btnRetry":
                    uic.restart();
                    break;
                case "btnNext":
                    uic.nextLevel();
                    break;
                default:
                    break;
            }
        }
    }
}
