using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickListener : MonoBehaviour
{
    #region Variables

    // Public Variables

    // Private Variables

    #endregion Variables

    private void Update()
    {
        if (GameManager.currentState == GameManager.GameStates.Stop && Input.GetMouseButtonDown(0))
        {
            GameObject clickedGO = EventSystem.current.currentSelectedGameObject;

            if (clickedGO == null)
            {
                GameManager.currentState = GameManager.GameStates.Start;
                Actions.OpenScreen();
            }
        }
    }
}