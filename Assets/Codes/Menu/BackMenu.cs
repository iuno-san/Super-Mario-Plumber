using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Codes.Menu
{
    public class BackMenu : MonoBehaviour
    {
        public GameObject backFirstButton, optionsFirstButton, optionsClosedButton;
    
        
        
        public void BackToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        
            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(backFirstButton);
        }
    }
}
