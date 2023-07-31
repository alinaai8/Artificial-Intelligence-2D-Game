using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("End");
    }

    public void replay()
    {
        SceneManager.LoadScene("Game");
    }
}
