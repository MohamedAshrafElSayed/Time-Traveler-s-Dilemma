using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    public void PlayerFell()
    {
        StartCoroutine(GameOver());
    }

    public void PlayerWin()
    {
        StartCoroutine(Win());
    }

    private IEnumerator GameOver()
    {
        resultText.text = "You Lose!";
        Debug.Log("Game Over!");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private IEnumerator Win()
    {
        resultText.text = "You Win!";
        Debug.Log("You Win!");
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerFell();
        }
    }
}
