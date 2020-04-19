using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Gets information about the pet's current status and displays it on
/// the screen in some form.
/// </summary>
public class PetStatusUI : MonoBehaviour
{
    public PetStatus pet;

    // This might not be necessary if we update this to be more than just
    // text on the screen
    private TextMeshProUGUI displayTarget;

    // Start is called before the first frame update
    void Start()
    {
        this.displayTarget = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Feel free to replace all of this if you're updating the UI

        string updatedDisplayText = "";
        updatedDisplayText += "Healthiness: " + pet.getHealth() + "\n";
        // TODO: update when pet conditions are expanded
        // secondary conditions should be visually less important than health i think
        updatedDisplayText += "Entertainedness: " + pet.getEntertainment() + "\n";

        this.displayTarget.text = updatedDisplayText;
    }
}
