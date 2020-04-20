using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A way to mark objects that should be dragged around by the mouse.
/// Also supports functions to be called on click and release, as with ClickableObject.
/// Uses code from https://answers.unity.com/questions/566519/camerascreentoworldpoint-in-perspective.html
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
        sceneCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyHeld && sceneCamera != null)
        {
            Vector3 mousePos = mouseToWorldPosition(Input.mousePosition);
            float z = this.transform.position.z;
            this.transform.position = new Vector3(mousePos.x, mousePos.y, z);

            if (!Input.GetMouseButton(LMB))
            {
                onDragRelease();
            }
        }
    }

    private Vector3 mouseToWorldPosition(Vector3 screenPosition)
    {
        Ray ray = sceneCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    /// <summary>
    /// Calls the given clickFunction when the object is clicked.
    /// </summary>
    public override void onClick()
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
    public override void onRelease()
    {
        return;
    }

    /// <summary>
    /// Cancels being held without calling any relevant functions. Useful if
    /// the onClick function wants to reject something.
    /// </summary>
    public void cancelDrag()
    {
        currentlyHeld = false;
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
