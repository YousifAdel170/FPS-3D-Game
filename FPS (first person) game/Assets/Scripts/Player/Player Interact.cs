using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);


        /*
            3 parts needed to perform raycast
                1. Ray Variable         :   origin & direction
                2. RaycastHit variable  :   To store some info about the collison such as distance from origin
                3. Raycast() function   :   check for collisons along the ray

         */
        
        // 1. create a ray at center of the camera, shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);


        // 2. create variable to store our collison info
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            // check if the game object has an interactable component on it 
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>() ;
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
