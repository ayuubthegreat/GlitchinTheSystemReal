
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
    public float distancefromPlayer;

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
                    FollowPlayer();

                }
                else
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
    public void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, playerScripts.transform.position + new Vector3(2, 0, 0), Time.deltaTime * 15);
    }
    public void DecreaseDistanceFromPlayer()
    {
        if (playerScripts.xInput == 0)
        {
            distancefromPlayer = 2;
            return;
        }
        if (playerScripts.xInput != 0)
        {
         distancefromPlayer -= Time.deltaTime;   
        }
        
        if (distancefromPlayer <= 0)
        {
            distancefromPlayer = 2;
            return;
        }
    }
}
