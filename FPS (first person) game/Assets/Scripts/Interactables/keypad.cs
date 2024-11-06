using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is where we will design our interaction using code
    // if we want to change color of something, play an animation, destroy object
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        Debug.Log("Door open state: " + doorOpen); 
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
