using UnityEngine;

public class UIManagerRPG : MonoBehaviour
{
    public static UIManagerRPG instance;
    [Header("RPG Text Related Objects")]
    public GameObject rpgTextObject;
    public GameObject personNameObject;
    public GameObject phone;
    public GameObject dialogueAnimations;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rpgTextObject.SetActive(false);
        personNameObject.SetActive(false);
        dialogueAnimations.SetActive(false);
        phone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
