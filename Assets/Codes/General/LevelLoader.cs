using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LoadNextLevel();
            
        }
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int LevelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(2);
        //Load Scene
        SceneManager.LoadScene(LevelIndex);
    }
}
