using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    [Header("Input Action Stuff")]
    [SerializeField] private InputActionAsset playerCtrls;
    [SerializeField] private string actionMapName = "Player";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string switchW = "Switch";

    [SerializeField] public InputAction lookAction;
    [SerializeField] public InputAction jumpAction;
    [SerializeField]  public InputAction sprintAction;
    [SerializeField] public InputAction switchAction;

    public Vector2 LookInput {get; private set;}
    public bool JumpTriggered {get; private set;}
    public bool SprintTriggered {get; private set;}
    public bool SwitchTriggered {get; private set;}

    public static InputHandler Instance {get; private set;}


    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        
        lookAction = playerCtrls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerCtrls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerCtrls.FindActionMap(actionMapName).FindAction(sprint);
        switchAction = playerCtrls.FindActionMap(actionMapName).FindAction(switchW);
        RegisterInputActions();
    }

    public void RegisterInputActions()
    {
        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        sprintAction.performed += context => SprintTriggered = true;
        sprintAction.canceled += context => SprintTriggered = false;

        switchAction.performed += context => SwitchTriggered = true;
        switchAction.canceled += context => SwitchTriggered = false;
    }

    public void OnEnable()
    {
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        switchAction.Enable();
    }

    public void OnDisable()
    {
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        switchAction.Disable();
    }

}
