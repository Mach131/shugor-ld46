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
    public GameController gameController;

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
        updatedDisplayText += "Healthiness: " + (int)pet.getHealth() + "\n";

        // secondary conditions should be visually less important than health i think
        foreach (KeyValuePair<string, float> entry in pet.getAllConditionValues())
        {
            updatedDisplayText += entry.Key + " " + (int)entry.Value + "\n";
        }

        // getting the remaining time as a percentage
        float remainingTimeFraction =
            gameController.getRemainingTIme() / gameController.getTotalDuration();
        updatedDisplayText += "\nBattery: "+(int)(remainingTimeFraction * 100)+"%";

        this.displayTarget.text = updatedDisplayText;
    }
}
