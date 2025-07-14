using UnityEngine;

public class ComplexEnemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected int moveSpeed;
    protected int coolDownTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
