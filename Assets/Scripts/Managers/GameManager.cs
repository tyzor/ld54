using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GAMESTATE {
    START,
    PAUSED,
    PLAY,
    GAME_OVER
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LightManager lightManager;
    
    [SerializeField]
    private Elevator elevator;

    [SerializeField]
    private UIManager uiManager;

    public GAMESTATE state;

    private CursorLockMode saveCursorState;

    // Start is called before the first frame update
    void Start()
    {
        lightManager.AllLightsOn();
        Cursor.lockState = CursorLockMode.Locked;

        PauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && state != GAMESTATE.PAUSED)
        {
            PauseGame();
            return;
        }
        /*
        if(Input.GetKeyDown(keyC) && state == GAMESTATE.PAUSED)
        {
            UnpauseGame();
            return;
        }
        */

        // Process game state while we aren't paused
        if(state == GAMESTATE.PAUSED)
            return;

        // Check if elevator is in a good condition
        ELEVATOR_CONDITION condition = elevator.CheckCondition();
        if(condition == ELEVATOR_CONDITION.Failed)
        {
            uiManager.showGameEnd("GAME OVER");
            return;
        } else if(condition == ELEVATOR_CONDITION.Errors)
        {
            // Do nothing -- can't advance
            return;
        } else if(condition == ELEVATOR_CONDITION.Good)
        {
            if(elevator.floorNumber == 1)
            {
                uiManager.showGameEnd("YOU HAVE ESCAPED");
                return;
            }

            // Elevator can continue moving
            elevator.DoMove();
        }


    }

    public void PauseGame()
    {
        if(state == GAMESTATE.PAUSED)
            return;

        saveCursorState = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        uiManager.showPause();
        Time.timeScale = 0;
        state = GAMESTATE.PAUSED;
    }
    public void UnpauseGame()
    {
        if(state != GAMESTATE.PAUSED)
            return;

        Cursor.lockState = saveCursorState;
        uiManager.hidePause();
        Time.timeScale = 1f;
        state = GAMESTATE.PLAY;
    }



}
