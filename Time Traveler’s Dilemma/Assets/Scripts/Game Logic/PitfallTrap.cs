using UnityEngine;

public class PitfallTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Disappear();
        }
    }

    private void Disappear()
    {
        gameObject.SetActive(false); 
    }
}
