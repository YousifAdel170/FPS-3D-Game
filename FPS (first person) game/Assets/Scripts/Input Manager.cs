using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// import the input system 
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    

    private bool gameEnded = false;


    // Reference to c# (player Input) that created
    private PlayerInput playerInput;

    // Reference to onFoot map
    public PlayerInput.OnFootActions onFoot;

    // create a property for our motor script
    private PlayerMotor motor;

    // create a property for our player Look script
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        // create an instance of our player input class
        playerInput = new PlayerInput();     
        onFoot = playerInput.OnFoot;    
        // We need to enable action map to use these so we created OnEnable() & OnDisable()


        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();


        // Anytime our onfoot.jump is performed we use call back context (ctx) to call our motor.jump function
        onFoot.Jump.performed += ctx => motor.Jump();

        // Anytime our onfoot.crouch is performed we use call back context (ctx) to call our motor.crouch function
        onFoot.Crouch.performed += ctx => motor.Crouch();

        // Anytime our onfoot.sprint is performed we use call back context (ctx) to call our motor.sprint function
        onFoot.Sprint.performed += ctx => motor.Sprint();


        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("More than one InputManager instance found!");
            Destroy(gameObject);
        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        // tell the playerlook to look using the value from our look action
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }


    // create an enable to use the player input
    private void OnEnable()
    {
        // call the enable 
        onFoot.Enable();     
    }
    // create an disable
    private void OnDisable()
    {
        // call the disable 
        onFoot.Disable();
    }


    public void EndGame()
    {


        if (!gameEnded)
        {
            gameEnded = true;
            Debug.Log("You won the game!");
            // Here you can add code to show a victory message, stop gameplay, etc.
            // For example, you can load a new scene or display a UI message.
            // For demonstration purposes, let's reload the current scene after a delay.
            Invoke("RestartGame", 2f);
        }
    }
    void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
