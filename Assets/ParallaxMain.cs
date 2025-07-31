using UnityEngine;

public class ParallaxMain : MonoBehaviour
{
    public GameObject grassLayersPrefab;
    public float xOffset;
    public Parallax_Effect[] parallax_Effects;
    public Transform followPoint;
    public float distanceFromPlayer;
    public float distanceLimit = 10f;
    public bool reachedTheLimit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parallax_Effects = GetComponentsInChildren<Parallax_Effect>();
        followPoint = transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(gameManagerPlatformer.instance.player.transform.position, followPoint.position);
        reachedTheLimit = distanceFromPlayer >= distanceLimit;
        if (reachedTheLimit)
        {
            return;
        }
        else
        {

            if (reachedTheLimit && gameManagerPlatformer.instance.player != null)
            {
                parallax_Effects = GetComponentsInChildren<Parallax_Effect>();
                foreach (Parallax_Effect effect in parallax_Effects)
                {
                    effect.enabled = false;
                }
                CreateGrassLayers();


            }
            else
            {
                distanceFromPlayer = Vector3.Distance(gameManagerPlatformer.instance.player.transform.position, transform.position);
                foreach (Parallax_Effect effect in parallax_Effects)
                {
                    effect.enabled = true;
                }
                reachedTheLimit = false;
            }
        }
        
        
        
        

    }
    private void CreateGrassLayers()
    {
        GameObject grassLayers = Instantiate(grassLayersPrefab, transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity);
        followPoint = grassLayers.transform;
    }
}
