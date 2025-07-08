using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour
{
    public int spriteLogoNum;
    public Sprite[] availableLogos;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = availableLogos[0];
    }

    // Update is called once per frame
    void Update()
    {
        spriteLogoNum = UIManager.instance.logoTransitions;
        if (spriteLogoNum >= 0 && spriteLogoNum < availableLogos.Length)
        {
            image.sprite = availableLogos[spriteLogoNum];
        }
        if (spriteLogoNum >= availableLogos.Length)
        {
            UIManager.instance.logosAreDone = true;
            UIManager.instance.toPlayMainMenu();
        } 
    }
}
