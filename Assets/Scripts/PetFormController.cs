using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for parsing, storing, and using pet form change data.
/// </summary>
public class PetFormController : MonoBehaviour
{
    [Serializable]
    private class PetFormData
    {
        public float neediness_delta = 0;
        public string default_sprite_name = "";
        public string happy_sprite_name = "";
        public string hungry_sprite_name = "";
        public string angry_sprite_name = "";

        public Material getDefaultSprite()
        {
            return getSprite(default_sprite_name);
        }
        public Material getHappySprite()
        {
            return getSprite(happy_sprite_name);
        }
        public Material getHungrySprite()
        {
            return getSprite(hungry_sprite_name);
        }
        public Material getAngrySprite()
        {
            return getSprite(angry_sprite_name);
        }

        private Material getSprite(string spriteName)
        {
            return (Material)Resources.Load(spriteName, typeof(Material));
        }
    }

    [Serializable]
    public struct PetFormTree
    {
        public TextAsset petFormDataFile;
        public List<PetFormBranch> nextForms;
    }
    [Serializable]
    public struct PetFormBranch
    {
        public float minimumNeediness;
        public float maximumNeediness;
        public int nextFormTreeId;
    }
    
    public List<PetFormTree> petFormDataTree;

    private PetFormTree currentPetFormNode;
    private PetStatus petStatus;


    // Start is called before the first frame update
    void Start()
    {
        petStatus = GetComponent<PetStatus>();
        currentPetFormNode = petFormDataTree[0];
        loadPetFormDataFile(currentPetFormNode.petFormDataFile);
    }

    /// <summary>
    /// Updates the pet's form, using this class's current place in the pet
    /// form tree.
    /// </summary>
    public void advancePetForm()
    {
        if (currentPetFormNode.nextForms.Count == 0)
        {
            return;
        }

        // check which form the pet is in the neediness bounds for
        float current_neediness = petStatus.getNeediness();
        foreach (PetFormBranch branch in currentPetFormNode.nextForms)
        {
            float minBound = branch.minimumNeediness;
            float maxBound = branch.maximumNeediness;
            if ((minBound == -1 || current_neediness >= minBound) &&
                (maxBound == -1 || current_neediness < maxBound))
            {
                currentPetFormNode = petFormDataTree[branch.nextFormTreeId];
                break;
            }
        }

        loadPetFormDataFile(currentPetFormNode.petFormDataFile);
    }

    /// <summary>
    /// Extracts information from a pet form data file and applies it to the
    /// pet.
    /// </summary>
    /// <param name="petFormDataFile">A JSON file containing the appropriate
    /// fields for the PetFormData class.</param>
    private void loadPetFormDataFile(TextAsset petFormDataFile)
    {
        PetFormData loadedData = JsonUtility.FromJson<PetFormData>(petFormDataFile.text);

        petStatus.updatePetFormStats(loadedData.neediness_delta,
            loadedData.getDefaultSprite(), loadedData.getHappySprite(),
            loadedData.getHungrySprite(), loadedData.getAngrySprite());
    }
}
