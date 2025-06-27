using UnityEngine;

public class Course_FireButton : MonoBehaviour
{
    private Course_Fire course_Fire;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        course_Fire = GetComponentInParent<Course_Fire>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        player player = collision.gameObject.GetComponent<player>();
        if (player != null)
        {
            anim.SetTrigger("activate");
            course_Fire.SwitchoffFire();
        }

    }
}
