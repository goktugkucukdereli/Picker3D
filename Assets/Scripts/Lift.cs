using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public Checkpoint check;

    private void OnCollisionEnter(Collision collision)
    {

        if (check.noMoreCheck) return;

        if (collision.gameObject.CompareTag("Collectable"))
        {
            check.totalCollecteds++;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            check.gotObstacle = true;
        }

        collision.transform.parent = transform;
        collision.gameObject.SetActive(false);

        Transform part = Level.instance.particlePool.GetChild(0).transform;
        part.gameObject.SetActive(true);
        part.parent = Level.instance.transform;
        part.position = collision.transform.position;

        ParticleSystem parts = part.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.startLifetime;

        StartCoroutine(RestoreParticle(totalDuration, part));
    }

    IEnumerator RestoreParticle(float d, Transform particle)
    {
        yield return new WaitForSeconds(d);

        particle.parent = Level.instance.particlePool;
        particle.position = Vector3.zero;
        particle.gameObject.SetActive(false);
    }
}