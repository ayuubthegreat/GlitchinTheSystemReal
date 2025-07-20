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
            gameManagerPlatformer.instance.coinNumbers++;
            gameManagerPlatformer.instance.soundEffectSource.PlayOneShot(gameManagerPlatformer.instance.coinSound);
            gameObject.SetActive(false);
        
        }


    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    
    }
    


