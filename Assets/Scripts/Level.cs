using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;
    public ParticleSystem finishParticle;
    public Transform picker;
    public Transform particlePool;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UIManager.instance.UpdateLevelText();
    }
}