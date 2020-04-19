using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A way to mark objects that should respond when clicked on.
/// Following the design pattern of putting scripts on an empty game object to which
/// things like colliders are made a child, this assumes that the colliders involved
/// in click checking are children of the object that this script is attached to.
/// This assumption mostly comes into play with a script that will be attached to the
/// camera though.
/// </summary>
public class ClickableObject : MonoBehaviour
{
    public UnityEvent clickFunction;
    public UnityEvent releaseFunction;

    public void onClick()
    {
        if (clickFunction != null)
        {
            clickFunction.Invoke();
        }
    }

    public void onRelease()
    {
        if (releaseFunction != null)
        {
            releaseFunction.Invoke();
        }
    }
}
