using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject characterSelectionWindow; // Pop-up window for character selection
    public Button warriorButton; // Warrior button
    public Button magicianButton; // Magician button
    public Button adventurerButton; // Adventurer button
    public Button conjurerButton; // Conjurer button

    void Start()
    {
        // Initialize button listeners
        warriorButton.onClick.AddListener(() => OnCharacterSelected("Warrior"));
        magicianButton.onClick.AddListener(() => OnCharacterSelected("Magician"));
        adventurerButton.onClick.AddListener(() => OnCharacterSelected("Adventurer"));
        conjurerButton.onClick.AddListener(() => OnCharacterSelected("Conjurer"));

        // Hide the character selection window initially
        characterSelectionWindow.SetActive(false);
    }

    // Show the character selection window
    public void ShowCharacterSelectionWindow()
    {
        characterSelectionWindow.SetActive(true);
    }

    // Called when a character is selected
    void OnCharacterSelected(string characterName)
    {
        Debug.Log("Character selected: " + characterName);

        // You can add your character selection logic here (assign abilities, stats, etc.)

        // Hide the character selection window after the choice
        characterSelectionWindow.SetActive(false);
    }
}
