using UnityEngine;

public class StandardEnemy : ComplexEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {

        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (isWallDetected || !isGroundDetected || isEnemyDetected)
        {
            Flip();
        }
    }
}
