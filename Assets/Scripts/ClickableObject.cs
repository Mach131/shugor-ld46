using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A way to mark objects that should respond when clicked on.
/// </summary>
public class ClickableObject : MonoBehaviour
{
    public UnityEvent clickFunction;
    public UnityEvent releaseFunction;

    /// <summary>
    /// Calls the given clickFunction when the object is clicked.
    /// </summary>
    public virtual void onClick()
    {
        if (clickFunction != null)
        {
            clickFunction.Invoke();
        }
    }
    
    /// <summary>
    /// Calls the given releaseFunction when the mouse is released on the object.
    /// </summary>
    public virtual void onRelease()
    {
        if (releaseFunction != null)
        {
            releaseFunction.Invoke();
        }
    }
}
