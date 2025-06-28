using System.Collections;
using UnityEngine;
using TMPro;


public class locationAnnouncer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public waypointScript[] waypoints;
    public int value = 1;
    public float moveDuration = 0.5f;
    public Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        text = GetComponentInChildren<TextMeshProUGUI>(); // Initialize UIManager.instance.locationAnnouncerBool based on UIManager setting
    }
    public IEnumerator ChangeLocationAnnouncerBoolTimer()
    {
        UIManager.instance.locationAnnouncerBool = false;
        yield return new WaitForSeconds(5);
        UIManager.instance.locationAnnouncerBool = true;
    }
    public void ChangeLocationAnnouncerBool(int value)
    {
        if (value == 1)
        {
            UIManager.instance.locationAnnouncerBool = true;
        }
        else
        {
            UIManager.instance.locationAnnouncerBool = false;
        }
    }
    public bool ChangeText()
    {
        if (text != null)
        {
            text.text = UIManager.instance.location; 
            return true; // Successfully changed the text
        }
        return false; // Failed to change the text, text component is null
    }
    void Update()
    {
        anim.SetBool("canMove", UIManager.instance.locationAnnouncerBool);
    }


}

    
   

