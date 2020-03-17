using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager singleton;
    Animator GameSM;
    Animator Player;


    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        Setup();
    }

    #region GameSm Trigger Delegate
    /// <summary>
    /// Delegato che gestisce gli eventi lanciati dall'esterno per triggerare il cambio di stato della GameStateMachine
    /// </summary>
    public delegate void GameSMTriggerDelegate();

    //public static GameSMTriggerDelegate Setup;

    public static GameSMTriggerDelegate GoToMainMenu;

    public static GameSMTriggerDelegate GoToOption;

    public static GameSMTriggerDelegate GoTOGamePlay;
    #endregion


    #region GamePlay Trigger Delegate
    public delegate void GamePlayTriggerDelegate();

    public static GamePlayTriggerDelegate RemoveLife;
    public static GamePlayTriggerDelegate AddLife;

    #endregion

    #region Player Trigger Delegate
    public delegate void PlayerTriggerDelegate();

    public static PlayerTriggerDelegate GoToMove;
    public static PlayerTriggerDelegate GoToCrouch;
    public static PlayerTriggerDelegate GoToGetUp;
    public static PlayerTriggerDelegate GoToPoint;
    #endregion


    public static void Setup()
    {
        singleton.GameSM = singleton.GetComponent<Animator>();
        singleton.Player = singleton.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventSetup();
    }

    public static void EventSetup()
    {
        GoToMainMenu += singleton.HandleGoToMainMenu;
        GoToOption += singleton.HandleGoToOption;

        GoToMove += singleton.HandleMove;
        GoToCrouch += singleton.HandleCrouch;
        GoToGetUp += singleton.HandleGetUp;
        GoToPoint += singleton.HandlePoint;

    }

    void HandleGoToMainMenu()
    {
        if (!singleton.GameSM.GetCurrentAnimatorStateInfo(0).IsName("MainMenu"))
        {
            singleton.GameSM.SetTrigger("GoToMainMenu");
        }
    }
    void HandleGoToOption()
    {
        if (!singleton.GameSM.GetCurrentAnimatorStateInfo(0).IsName("Option"))
        {
            singleton.GameSM.SetTrigger("GoToOption");
        }
    }

    void HandleMove()
    {
        if (!singleton.Player.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            singleton.Player.SetTrigger("GoToMove");
        }
    }
    void HandleCrouch()
    {
        if (!singleton.Player.GetCurrentAnimatorStateInfo(0).IsName("Crouch"))
        {
            singleton.Player.SetTrigger("GoToCrouch");
        }
    }
    void HandleGetUp()
    {
        if (!singleton.Player.GetCurrentAnimatorStateInfo(0).IsName("GetUp"))
        {
            singleton.Player.SetTrigger("GoToGetUp");
        }
    }
    void HandlePoint()
    {
        if (!singleton.Player.GetCurrentAnimatorStateInfo(0).IsName("Point"))
        {
            singleton.Player.SetTrigger("GoToPoint");
        }
    }

    private void OnDisable()
    {
        GoToMainMenu -= singleton.HandleGoToMainMenu;
        GoToOption -= singleton.HandleGoToOption;

        GoToMove -= singleton.HandleMove;
        GoToCrouch -= singleton.HandleCrouch;
        GoToGetUp -= singleton.HandleGetUp;
        GoToPoint -= singleton.HandlePoint;
    }
}
