using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickListener : MonoBehaviour
{
	#region Variables

	// Public Variables

    // Private Variables

    #endregion

    void Update()
    {
        if (!PlayerController.isGameStart && Input.GetMouseButtonDown(0))
        {
			GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

			if (clickedGO == null)
				Actions.OpenScreen(null);
		}
    }
}
