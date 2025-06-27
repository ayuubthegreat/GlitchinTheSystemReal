using UnityEngine;

public class Traps_SpikedBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float pushForce = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (pushForce != 0)
        {
            Vector2 pushVec = new Vector2(pushForce, 0);
           rb.AddForce(pushVec, ForceMode2D.Impulse); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
