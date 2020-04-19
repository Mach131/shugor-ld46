using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A way to mark objects that should be dragged around by the mouse.
/// Also supports functions to be called on click and release, as with ClickableObject.
/// </summary>
public class DraggableObject : ClickableObject
{
    public bool initializeAsHeld;

    private bool currentlyHeld;
    private Camera sceneCamera;

    private static int LMB = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentlyHeld = initializeAsHeld;
        sceneCamera = FindObjectOfType<Camera>(); //might hurt performance
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyHeld && sceneCamera != null)
        {
            Vector3 mousePos = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
            float z = this.transform.position.z;
            this.transform.position = new Vector3(mousePos.x, mousePos.y, z);

            if (!Input.GetMouseButton(LMB))
            {
                onDragRelease();
            }
        }
    }

    /// <summary>
    /// Calls the given clickFunction when the object is clicked.
    /// </summary>
    public new void onClick()
    {
        currentlyHeld = true;
        if (clickFunction != null)
        {
            clickFunction.Invoke();
        }
    }

    /// <summary>
    /// This is ignored for draggables, in favor of a function that gets called regardless
    /// of whether this was the object that got clicked.
    /// </summary>
    public new void onRelease()
    {
        return;
    }

    /// <summary>
    /// Calls the given releaseFunction when the mouse is released.
    /// </summary>
    public void onDragRelease()
    {
        currentlyHeld = false;
        if (releaseFunction != null)
        {
            releaseFunction.Invoke();
        }
    }
}
