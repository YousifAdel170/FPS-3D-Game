using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Used to add or remove InteractionEvent Component to this game object
    public bool useEvents;
    [SerializeField]
    // message displayed to the played when looking at a interactable
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }


    // This function will be called from the played
    public void BaseInteract()
    {
        /*
         events always run first , interaction run second
         */
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
       /*
            We won't have any code written in this function
            This is a template function to be overriden by our subclasses
        */
    }
}
