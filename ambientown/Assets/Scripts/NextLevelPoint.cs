using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string lvlName;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("FalarDnv", 1);
            SceneManager.LoadScene(lvlName);
            FindObjectOfType<AudioManager>().Play("Transition");
        }
    }
}
