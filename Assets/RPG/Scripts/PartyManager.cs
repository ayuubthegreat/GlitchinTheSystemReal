using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New Party Member", menuName = "RPG/Party Member")]
public class PartyMember : ScriptableObject
{
    public string memberName;
    public int level;
    public int health;
    public int attack = 1;
    public int defense = 1;
    public int speed = 1;
    public Move[] assignedMoves;
}

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;
    public PartyMember[] partyMembers;
    public GameObject[] moveButtons;
    public TextMeshProUGUI[] moveButtonTitles;
    public TextMeshProUGUI[] moveButtonDescriptions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateMoveButtons(PartyMember member)
    {
        for (int i = 0; i < PartyManager.instance.moveButtons.Length; i++)
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
}
