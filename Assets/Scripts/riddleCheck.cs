using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RiddleManager : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public Button cycleLeftButton;
    public GameObject solvedWindow;
    public Button yesButton;
    public Button noButton;
    public Button showSolve;
    public GameObject riddleLoadingGameObject;
    public GameObject riddleGameObject;
    public GameObject riddleAnswerGameObject;
    public GameObject roleSelectionWindow;
    public GameObject abilityScreen;

    public Button warriorButton;
    public Button magicianButton;
    public Button adventurerButton;
    public Button conjurerButton;

    public TextMeshProUGUI abilityRoleText;
    public TextMeshProUGUI abilityDescriptionText;
    public TextMeshProUGUI cyclesLeftText;
    public Button endTurnButton;

    public TextMeshProUGUI answerText;

    private int cycleCount = 1;
    private bool isSolving = false;
    private string currentGod;
    private string selectedRole;
    private int abilityCyclesLeft;
    private RandomSentenceManager randomSentenceManager;

    public AudioSource audioSource;
    public AudioClip wisdomReveal;
    public AudioClip warReveal;
    public AudioClip wealthReveal;

    void Start()
    {
        cycleLeftButton.onClick.AddListener(OnCycleLeftClicked);
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);
        showSolve.onClick.AddListener(OnShowSolveClicked);

        warriorButton.onClick.AddListener(() => OnRoleSelected("Warrior"));
        magicianButton.onClick.AddListener(() => OnRoleSelected("Magician"));
        adventurerButton.onClick.AddListener(() => OnRoleSelected("Adventurer"));
        conjurerButton.onClick.AddListener(() => OnRoleSelected("Conjurer"));

        endTurnButton.onClick.AddListener(OnEndTurnClicked);

        solvedWindow.SetActive(false);
        riddleAnswerGameObject.SetActive(false);
        roleSelectionWindow.SetActive(false);
        abilityScreen.SetActive(false);

        randomSentenceManager = FindObjectOfType<RandomSentenceManager>();
    }

    void OnCycleLeftClicked()
    {
        if (isSolving) return;

        cycleCount--;
        counterText.text = cycleCount.ToString();

        if (cycleCount <= 0)
        {
            ShowRiddleAnswer();
        }
    }

    void ShowRiddleAnswer()
    {
        riddleGameObject.SetActive(false);
        riddleAnswerGameObject.SetActive(true);

        string answer = randomSentenceManager.GetCurrentAnswer();
        answerText.text = answer;

        currentGod = randomSentenceManager.GetCurrentGod();
    }

    void OnShowSolveClicked()
    {
        solvedWindow.SetActive(true);
        riddleAnswerGameObject.SetActive(false);
    }
    void OnYesClicked()
    {
        solvedWindow.SetActive(false);
        ShowRoleSelectionWindow();
    }

    void OnNoClicked()
    {
        solvedWindow.SetActive(false);
        RestartRiddleProcess();
    }

    void RestartRiddleProcess()
    {
        cycleCount = 1;
        counterText.text = cycleCount.ToString();

        riddleGameObject.SetActive(false);
        riddleLoadingGameObject.SetActive(true);

        randomSentenceManager.ShowRandomSentence();
    }

    void ShowRoleSelectionWindow()
    {
        roleSelectionWindow.SetActive(true);
    }

    void OnRoleSelected(string role)
    {
        selectedRole = role;
        roleSelectionWindow.SetActive(false);
        ShowAbilityScreen();
    }

    void ShowAbilityScreen()
    {
        abilityScreen.SetActive(true);
        abilityRoleText.text = selectedRole;

        string abilityDescription = GetAbilityDescription(selectedRole, currentGod);
        abilityDescriptionText.text = abilityDescription;

        abilityCyclesLeft = 2;
        cyclesLeftText.text = $"Cycles Left: {abilityCyclesLeft}";
    }

    string GetAbilityDescription(string role, string god)
    {



        if (audioSource.isPlaying) {
            audioSource.Stop();
        }

        switch (currentGod)
        {
            case "God of Wisdom":
                audioSource.clip = wisdomReveal;
                break;
            case "God of War":
                audioSource.clip = warReveal;
                break;
            case "God of Wealth":
                audioSource.clip = wealthReveal;
                break;
            default:
                Debug.LogWarning("Unknown God: " + currentGod);
                break;
        }

        audioSource.loop = true;

        audioSource.Play();



        switch (role)
        {
            case "Warrior":
                if (god == "God of Wisdom") return "Fully Infused: You now use the range of the magic card when imbuing a physical card.";
                if (god == "God of War") return "Bladeworks:\nAll your physical cards now costs 2 physical resources, has 2 range, and deals 3 damage.";
                if (god == "God of Wealth") return "Greed's Blade:\nEvery time you successfully deal damage, draw 1 card.";
                break;

            case "Magician":
                if (god == "God of Wisdom") return "Overload:\nWhen doing a magic combination, double the imbue effect of one of the magic used in the combination.";
                if (god == "God of War") return "Overcharge:\nWhen using a magic by itself, double its effect.";
                if (god == "God of Wealth") return "Golem Construct:\nOnce per turn, you may discard an Item card to summon a Golem, represented by a token/marker, that has the following effect:\nThey have 1 speed and you move them during your turn. When they come in contact with a player, the golem deals 2 to every player in that tile. They persists even when awakening has ended.";
                break;

            case "Adventurer":
                if (god == "God of Wisdom") return "Secret Passage:\nAll field tiles now act as a Portal only to you. You may move from one field tile to another at the cost of 1 movement.";
                if (god == "God of War") return "Bear Trap:\nYour physical cards all have the same attributes:\nCost: 2\nEffect: Place a Trap Marker on the tile you're standing on which will turn the tile into a Trap tile that deals 2 damage to any player who enters. They persists even when awakening has ended.";
                if (god == "God of Wealth") return "Efficiency:\nDouble the effects of every Item Card";
                break;

            case "Conjurer":
                if (god == "God of Wisdom") return "Expend:\nIncrease the damage of an imbued attack by half of the amount of Magic Resources used rounded up.";
                if (god == "God of War") return "Exhaust:\nIncrease the damage of an imbued attack by half of the amount of Physical Resources used rounded up.";
                if (god == "God of Wealth") return "Transmute:\nYou may use Item Cards as a substitute for a single Physical or Magic Resource";
                break;
            case "Warden":
                if (god == "God of Widsom") return "";
                if (god == "God of War") return "";
                if (god == "God of Wealth") return "";
                break; 
        }

        return "Unknown Ability";
    }

    void OnEndTurnClicked()
    {
        abilityCyclesLeft--;
        cyclesLeftText.text = $"Cycles Left: {abilityCyclesLeft}";

        if (abilityCyclesLeft <= 0)
        {
            abilityScreen.SetActive(false);
            RestartRiddleProcess();
        }
    }
}
