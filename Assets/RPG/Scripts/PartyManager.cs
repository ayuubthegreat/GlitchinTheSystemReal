using UnityEngine;
using TMPro;

// [CreateAssetMenu(fileName = "New Party Member", menuName = "RPG/Party Member")]
public enum MoveCategory
{
    Physical,
    Special,
    Status
}

public enum MoveTarget
{
    Opponent,
    Self,
    All, 
}

[System.Serializable]
public struct Move
{
    public string moveName;
    public int power; // formerly attackDamage
    public int accuracy; // 0-100
    public int priority; // 0 is normal, higher goes first
    public MoveCategory category; // Physical, Special, Status
    public string effectDescription; // e.g., "May lower defense"
    public MoveTarget target; // e.g., Opponent, Self

    // Stat changes (if any)
    public int attackChange;
    public int defenseChange;
    public int speedChange;
    public int accuracyChange;
}
[System.Serializable]
public struct PartyMember
{
    public string memberName;
    public int level;
    public int health;
    public int originalHealth;
    public int attack;
    public int defense;
    public int speed;
    public Move[] assignedMoves;
    public RuntimeAnimatorController battleAnimator;
}

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    public PartyMember[] partyMembers;
    // Battle Move UI Elements
    public GameObject[] moveButtons;
    public TextMeshProUGUI[] moveButtonTitles;
    public TextMeshProUGUI[] moveButtonDescriptions;
    // Party Member Details UI Elements
    public TextMeshProUGUI partyMemberNameTextUI;
    public TextMeshProUGUI partyMemberLevelTextUI;
    public TextMeshProUGUI partyMemberHealthTextUI;
    public TextMeshProUGUI XPtoNextLevelTextUI;
    public GameObject[] moveDetailsPanels;
    public TextMeshProUGUI[] moveDetailsTitlesUIs;
    public TextMeshProUGUI[] moveDetailsTextUIs;

    public Move[] allMoves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        CreateAllMoves();
        // Example party member setup
        partyMembers[0].assignedMoves = new Move[]
        {
            allMoves[0], // Slash
            allMoves[1], // Surge
            allMoves[2], // Fireball
            allMoves[3]  // Quick Attack
        };
        UpdateMoveButtons(partyMembers[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateMoveButtons(PartyMember member)
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            if (i < member.assignedMoves.Length)
            {
                moveButtons[i].SetActive(true);
                moveButtons[i].GetComponent<UnityEngine.UI.Button>().interactable = true;
                moveButtonTitles[i].text = member.assignedMoves[i].moveName;
                moveButtonDescriptions[i].text = member.assignedMoves[i].effectDescription;
            }
            else
            {
                moveButtons[i].SetActive(false);
                moveButtons[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
                moveButtons[i].GetComponentInChildren<UnityEngine.UI.Text>().text = "";
            }
        }
    }
    public void CreateAllMoves()
    {
        allMoves = new Move[]
        {
            new Move
            {
                moveName = "Slash",
                power = 5,
                accuracy = 95,
                priority = 0,
                category = MoveCategory.Physical,
                effectDescription = "A quick slash attack.",
                target = MoveTarget.Opponent,
                attackChange = 0,
                defenseChange = 0,
                speedChange = 0,
                accuracyChange = 0
            },
            new Move
            {
                moveName = "Surge",
                power = 4, 
                accuracy = 100,
                priority = 0,
                category = MoveCategory.Physical,
                effectDescription = "Quickly attack!",
                target = MoveTarget.Opponent,
                attackChange = 0,
                defenseChange = 0,
                speedChange = 0,
                accuracyChange = 0
            },
            new Move
            {
                moveName = "Fireball",
                power = 8,
                accuracy = 90,
                priority = 0,
                category = MoveCategory.Physical,
                effectDescription = "A fiery blast that may burn the opponent.",
                target = MoveTarget.Opponent,
                attackChange = 0,
                defenseChange = -1, // May lower opponent's defense
                speedChange = 0,
                accuracyChange = 0
            },
            new Move
            {
                moveName = "Quick Attack",
                power = 4,
                accuracy = 100,
                priority = 1, // Higher priority to go first
                category = MoveCategory.Physical,
                effectDescription = "A fast attack that always strikes first.",
                target = MoveTarget.Opponent,
                attackChange = 0,
                defenseChange = 0,
                speedChange = 0,
                accuracyChange = 0
            }
        };
    }
}
