using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class UIManagerRPG : MonoBehaviour
{
    public static UIManagerRPG instance;
    [Header("RPG Text Related Objects")]
    public GameObject rpgTextObject;
    public GameObject personNameObject;
    public GameObject phone;
    public GameObject dialogueAnimations;
    public Image cutsceneImageObject;
    [Header("Location Announcer Elements")]
    public RectTransform rectTransform;
    public TextMeshProUGUI text;
    public Vector3[] waypoints;
    public int waypointIndex = 0;
    public float yOffset = 10f;
    public float announcementSpeed = 5f;
    public string locationName = "Abdurahman's House";
    [Header("Image Cutscenes")]
    public Sprite[] cutsceneImages;
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
        ControlRPGUIElements(false);
        SetLocationAnnouncerElements();
        ChangeText(locationName);
        StartMoveLocationAnnouncer(2f);


    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, waypoints[waypointIndex], Time.deltaTime * announcementSpeed);
    }
    public void ControlRPGUIElements(bool isActive)
    {
        // rpgTextObject.SetActive(isActive);
        // personNameObject.SetActive(isActive);
        dialogueAnimations.SetActive(isActive);
        phone.SetActive(isActive);
    }
    public void SetLocationAnnouncerElements()
    {
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is not assigned in UIManagerRPG.");
            return;
        }
        waypoints = new Vector3[2];
        waypoints[0] = rectTransform.anchoredPosition;
        waypoints[1] = rectTransform.anchoredPosition + new Vector2(0, -yOffset); // Example offset for the second waypoint
    }
    public void ChangeLocationPosition()
    {
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, waypoints[waypointIndex], Time.deltaTime * 5f);
    }
    public bool ChangeText(string newText = "")
    {
        if (text != null)
        {
            text.text = newText;


            return true; // Successfully changed the text
        }
        return false; // Failed to change the text, text component is null
    }
    public IEnumerator MoveLocationAnnouncer(float duration)
    {
        yield return new WaitForSeconds(1f);

        waypointIndex = 1; 

        yield return new WaitForSeconds(duration); 

        waypointIndex = 0; 
    }
    public void StartMoveLocationAnnouncer(float duration)
    {
        rectTransform.anchoredPosition = waypoints[0]; // Reset position to the first waypoint
        waypointIndex = 0; // Start from the first waypoint
        StartCoroutine(MoveLocationAnnouncer(duration));
    }
}
