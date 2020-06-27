using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState { MENU, SELECT, HELP, CREDITS, PLAY }

    private LevelManager levelManager;
    private PlayerController playerController;
    private UIManager uiManager;

    private GameState currentState
    {
        get
        {
            return currentState;
        }
        set
        {
            switch(value)
            {
                case GameState.MENU:
                    SwitchToMenu();
                    break;
                case GameState.SELECT:
                    SwitchToSelection();
                    break;
                case GameState.HELP:
                    SwitchToHelp();
                    break;
                case GameState.CREDITS:
                    SwitchToCredits();
                    break;
                case GameState.PLAY:
                    SwitchToPlay();
                    break;
                default:
                    SwitchToMenu();
                    break;
            }
            currentState = value;
        }
    }

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerController = FindObjectOfType<PlayerController>();
        uiManager = FindObjectOfType<UIManager>();

        currentState = GameState.MENU;
    }

    private void SwitchToMenu()
    {
        uiManager.DisplayMenu();
    }

    private void SwitchToSelection()
    {
        uiManager.DisplayLevelSelect();
    }

    private void SwitchToHelp()
    {
        uiManager.DisplayHelp();
    }

    private void SwitchToCredits()
    {
        uiManager.DisplayCredits();
    }
    private void SwitchToPlay()
    {
        uiManager.DisplayGameUI();
    }
}
