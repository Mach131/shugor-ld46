﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the detection of clicks and drags, notifying the relevant scripts.
/// Functionality adapted from https://answers.unity.com/questions/332085/how-do-you-make-an-object-respond-to-a-click-in-c.html
/// </summary>
public class ClickController : MonoBehaviour
{
    public Camera sceneCamera;

    private static int LMB = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(LMB))
        {
            sendClickMessage(true);
        } else if (Input.GetMouseButtonUp(LMB))
        {
            sendClickMessage(false);
        }
    }

    private void sendClickMessage(bool wasButtonDown)
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            /*
            DraggableObject dragTarget = hit.transform.GetComponentInParent<DraggableObject>();
            if (dragTarget != null)
            {
                if (wasButtonDown)
                {
                    dragTarget.onClick();
                }
                else
                {
                    dragTarget.onRelease();
                }
            }*/

            ClickableObject clickTarget = hit.transform.GetComponentInParent<ClickableObject>();
            if (clickTarget != null)
            {
                if (wasButtonDown)
                {
                    clickTarget.onClick();
                }
                else
                {
                    clickTarget.onRelease();
                }
            }
        }
    }
}
