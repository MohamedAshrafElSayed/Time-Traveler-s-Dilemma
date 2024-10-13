using UnityEngine;

public class PitfallTrap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Disappear();
        }
    }

    private void Disappear()
    {
        gameObject.SetActive(false); 
    }
}
