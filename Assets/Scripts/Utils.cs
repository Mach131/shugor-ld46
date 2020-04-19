using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains helpful functions that might not be exclusively useful in one class.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Checks if the bounding boxes of two colliders overlap from the perspective
    /// of a camera looking along the positive z axis.
    /// </summary>
    /// <param name="a">The first collider</param>
    /// <param name="b">The second collider</param>
    /// <returns>Whether or not the rectangular region outlined by the x-y plane of
    /// each collider's bounding box overlap</returns>
    public static bool checkOverlap(Collider a, Collider b)
    {
        Rect boundRectA = boundsToRect(a.bounds);
        Rect boundRectB = boundsToRect(b.bounds);
        return boundRectA.Overlaps(boundRectB);
    }

    private static Rect boundsToRect(Bounds bounds)
    {
        return new Rect(bounds.min, bounds.size);
    }
}
