using UnityEngine;
using TMPro;

public class locationAnnouncer : fadebase
{
    public TextMeshProUGUI text;
    
   

    new
        // Start is called before the first frame update
        void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>(); // Initialize text
    }

    // Update is called once per frame
    
    public bool ChangeText()
    {
        if (text != null)
        {
            text.text = UIManager.instance.location;
           
           
            return true; // Successfully changed the text
        }
        return false; // Failed to change the text, text component is null
    }
}