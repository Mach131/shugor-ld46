using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Keeps track of the different ending scenes, and transitions
/// to them as necessary.
/// </summary>
public class EndingManager : MonoBehaviour
{
    public string hungryEndingSceneName;

    [Serializable]
    public struct PetFormEndings
    {
        public int petFormId;
        public string goodEndingSceneName;
        public string badEndingSceneName;
    }
    [SerializeField]
    public List<PetFormEndings> petFormEndings;

    private Dictionary<int, string> goodEndingScenes;
    private Dictionary<int, string> badEndingScenes;

    private PetFormController petForm;
    private PetStatus petStatus;

    // Start is called before the first frame update
    void Start()
    {
        goodEndingScenes = new Dictionary<int, string>();
        badEndingScenes = new Dictionary<int, string>();

        foreach (PetFormEndings ending in petFormEndings)
        {
            goodEndingScenes.Add(ending.petFormId, ending.goodEndingSceneName);
            badEndingScenes.Add(ending.petFormId, ending.badEndingSceneName);
        }

        petForm = GetComponent<GameController>().petFormController;
        petStatus = petForm.GetComponent<PetStatus>();
    }

    /// <summary>
    /// Changes to the hungry ending.
    /// </summary>
    public void toHungryEnding()
    {
        changeScenes(hungryEndingSceneName);
    }
    /// <summary>
    /// Changes to an ending depending on the current state of the pet.
    /// </summary>
    public void toStandardEnding()
    {
        int formIndex = petForm.getPetFormIndex();
        string targetSceneName = petStatus.isSatisfied() ?
            goodEndingScenes[formIndex] :
            badEndingScenes[formIndex];
        changeScenes(targetSceneName);
    }

    private void changeScenes(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
