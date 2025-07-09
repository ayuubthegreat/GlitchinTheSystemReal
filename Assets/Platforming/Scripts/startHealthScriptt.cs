using UnityEngine;

public class startHealthScriptt : MonoBehaviour
{

    public Animator anim;
    public hearts[] hearts;
    public bool showHealth = false;
    public bool GameStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        hearts = FindObjectsByType<hearts>(FindObjectsSortMode.None);
        SetShowHealth(0); // Initialize health display to off
        GameStarted = true; // Initialize game state
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("start", UIManager.instance.start);
        anim.SetBool("isDead", UIManager.instance.isDead);
        anim.SetBool("isAlive", UIManager.instance.isAlive);
        
    }
    public void SetIsDeadandIsAlive2(bool isDeadinner) => UIManager.instance.SetIsDeadandIsAliveBool(isDeadinner);
    public void SetShowHealth(int value) {
        showHealth = value == 1 ? true : false;
        foreach (hearts heart in hearts)
        {
            heart.gameObject.SetActive(showHealth);
        }
    }
    public void SetDestroyIndividualHealth(int value)
    {
        GameStarted = false;
        if (value >= 0 && value < hearts.Length)
        {
            hearts[value].gameObject.SetActive(false);
        }
    }
}
