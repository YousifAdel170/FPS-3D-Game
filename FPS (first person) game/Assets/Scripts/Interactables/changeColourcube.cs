using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColourcube : Interactable
{
    MeshRenderer mesh;
    public Color[] colors;
    private int colourIndex;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    // Update is called once per frame
    protected override void Interact()
    {
        colourIndex++;
        if(colourIndex > colors.Length -1)
        {
            colourIndex = 0;
        }
        mesh.material.color = colors[colourIndex];
    }
}
