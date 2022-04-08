using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;
    


    private void Update()
    {
       

        if (Input.GetKey(KeyCode.R) && _isGameOver == true) /*CrossPlatformInputManager.GetButtonDown("Fire") && _isGameOver == true)*/ 
        {
            SceneManager.LoadScene(1);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

   

   

   
    

   

}
