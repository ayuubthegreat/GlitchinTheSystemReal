
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
    public bool followPlayer = true;
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

    void LateUpdate()
    {
        if (GameManager.instance != null)
        {
            playerScripts = GameManager.instance.player;

            if (platformingCamera && playerScripts != null)
            {
                if (followPlayer)
                {
                    transform.position = Vector3.Lerp(transform.position, playerScripts.transform.position, Time.deltaTime * 5);
                }

            }
            else if (playerpg != null)
            {
                if (GameManagerRPG.instance.isCutsceneActive == false)
                {
                    transform.position = playerpg.transform.position;
                }
                
            }
        }
    }
    public void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, playerScripts.transform.position + new Vector3(distancefromPlayer, 0, 0), Time.deltaTime * 15);
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
