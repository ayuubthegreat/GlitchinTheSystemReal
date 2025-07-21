using UnityEngine;

public class StandardEnemy : ComplexEnemy
{
    public float yVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {

        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        yVelocity = rb.linearVelocity.y;
    }
}
