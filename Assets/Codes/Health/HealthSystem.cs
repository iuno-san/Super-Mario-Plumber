using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{

    private void Update()
    {
        if (gameObject.transform.position.y < -6)
        {
            ReloadLevel();
        }

    }
    void ReloadLevel()
    {
        SceneManager.LoadScene ("Level-1");
    }

    
}
