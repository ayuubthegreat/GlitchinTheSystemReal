using UnityEngine;

public class changeSortingLayer : MonoBehaviour
{

    public float detectionRadius = 0.1f;
    public string newSortingLayer = "NPCFront";
    public string originalSortingLayer;
    public Collider2D[] colliders;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSortingLayer();
    }
    public void ChangeSortingLayer()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (var collider in colliders)
        {
            if (collider == null) continue;
            SpriteRenderer sr = collider.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingLayerName = newSortingLayer;
            }
        }
    }
}
