using UnityEditor;
// This means this editor script will also affect child classes of our interactable
[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    // This function get called everytime unity updates the editor interface
    public override void OnInspectorGUI()
    {
        // Cast the target object to Interactable
        Interactable interactable = (Interactable)target;

        if (target.GetType() == typeof(EventOnlyInteractable)) 
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();   
            }
        }
        else
        {
            // Draw the default inspector GUI
            base.OnInspectorGUI();

            // Check if useEvents is true
            if (interactable.useEvents)
            {
                // we are using events, add this component
                // If useEvents is true and InteractionEvent component is not already added, add it
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                // we are not using events, remove the component
                // If useEvents is false and InteractionEvent component is already added, remove it
                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }


    }
}

