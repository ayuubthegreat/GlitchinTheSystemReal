using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class StreetChanger : LocationChanger
{
    public StreetChanger[] streets;
    public bool isIntersection = false;
    public bool isFullStreet = true;
    public TextMeshProUGUI text;
    public Collider2D[] collisions;

    void Start()
    {
        b2d = GetComponent<BoxCollider2D>();
        GetCollisions();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerpg player = collision.gameObject.GetComponent<playerpg>();

        if (player != null)
        {
            GetCollisions();
            if (isFullStreet)
            {
                UIManager.instance.streetName = backLocation;
            }
            else
            {
                UIManager.instance.streetName = backLocation + "and" + frontLocation;
            }

            text.text = UIManager.instance.streetName;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision) {
        UIManager.instance.streetName = string.Empty;
        text.text = UIManager.instance.streetName;
    }
    private void GetCollisions()
    {
        collisions = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0f);
        if (isIntersection)
        {
            List<StreetChanger> streetList = new List<StreetChanger>();
            foreach (Collider2D collider in collisions)
            {
                StreetChanger streetChanger = collider.gameObject.GetComponent<StreetChanger>();
                if (streetChanger != null)
                {
                    streetList.Add(streetChanger);
                }
            }
            streets = streetList.ToArray();
        }
    }
    
}