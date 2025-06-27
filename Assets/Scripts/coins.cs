using UnityEngine;

public class coins : MonoBehaviour
{

    private GameManager gameManager;
    public GameObject pickupVFX;
    private Animator anim;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<player>())
        {
            GameManager.instance.player.coinNumbers++;
            Destroy(gameObject);
            Gone();
        }


    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        SetRandomLook();
    }
    private void SetRandomLook()
    {
        if (GameManager.instance.FruitsAreRandom() == false)
        {
            return;
        }
        int randomIndex = Random.Range(0, 10);
        anim.SetFloat("fruitNum", randomIndex);
    }
    private void Gone()
    {
        GameObject newFX = Instantiate(pickupVFX);
        newFX.transform.position = transform.position;
        Destroy(newFX, 1f);
    }
}

