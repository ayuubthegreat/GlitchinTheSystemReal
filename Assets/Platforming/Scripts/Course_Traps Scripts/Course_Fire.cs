using System.Collections;
using UnityEngine;

public class Course_Fire : MonoBehaviour
{
    public float offDuration;
    private Course_FireButton course_FireButton;
    private Animator anim;
    private BoxCollider2D[] bc2D;
    public bool isActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        anim = GetComponent<Animator>();
        bc2D = GetComponents<BoxCollider2D>();
        course_FireButton = GetComponentInChildren<Course_FireButton>();
    }
    void Start()
    {
        SetBool(true);
    }
    public void SwitchoffFire() {
        if (!isActive)
        {
            return;
        }
        StartCoroutine(SetBooler());
    }
    private IEnumerator SetBooler()
    {
        SetBool(false);
        yield return new WaitForSeconds(offDuration);
        SetBool(true);
    }
    public void SetBool(bool Active)
    {
        anim.SetBool("active", Active);
        foreach (BoxCollider2D boxes in bc2D)
        {
            boxes.enabled = Active;
        }
        isActive = Active;
    }
}
