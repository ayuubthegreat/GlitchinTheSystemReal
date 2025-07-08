
using UnityEngine;

public class CameraControllerRPG : MonoBehaviour
{
    public GameObject player;
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip clipOld;
    public bool platformingCamera;
    public player playerScripts;
    public playerpg playerpg;
    public bool startFollowingPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            clip = audioSource.clip;
        }

        GameObject foundPlayer = GameObject.Find("player");
        if (foundPlayer != null)
        {
            
            playerpg = foundPlayer.GetComponent<playerpg>();
        }
    }

    void Update()
    {
        if (GameManager.instance != null)
        {
            playerScripts = GameManager.instance.player;

            if (platformingCamera && playerScripts != null)
            {
                if (startFollowingPlayer)
                {
                    transform.position = playerScripts.transform.position;  
                }
                
            }
            else if (playerpg != null)
            {
                transform.position = playerpg.transform.position;
            }
        }
    }
}
