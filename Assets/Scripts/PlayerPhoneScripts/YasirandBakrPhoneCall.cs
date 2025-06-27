using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YasirandBakrPhoneCall : MonoBehaviour
{
    public GameObject phoneUI;
    public Animator phoneAnimator;
    public TextMeshProUGUI dialogueText;

    private void Start()
    {
        phoneAnimator = GetComponent<Animator>();
        phoneUI.SetActive(false);
    }

    public void StartPhoneCall()
    {
        phoneUI.SetActive(true);
    }

    public void EndPhoneCall()
    {
        phoneUI.SetActive(false);
    }
    public void Update()
    {
        phoneAnimator.SetBool("player2talking", DialogueProcessor.instance.person2turn);
        phoneAnimator.SetInteger("DP", DialogueManager.instance.DialogueProgression);
    }
}
