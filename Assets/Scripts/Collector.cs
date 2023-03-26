using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collectable") && !other.CompareTag("Obstacle")) return;

        CharacterControl.instance.collected.Add(other.GetComponent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Collectable") && !other.CompareTag("Obstacle")) return;

        for (int i = 0; i < CharacterControl.instance.collected.Count; i++)
        {
            CharacterControl.instance.collected.Remove(other.attachedRigidbody);
        }
    }
}