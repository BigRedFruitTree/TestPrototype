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

    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction switchAction;

    public Vector2 LookInput {get; private set;}
    public bool JumpTriggered {get; private set;}
    public bool SprintTriggered {get; private set;}

    public static InputHandler Instance {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
