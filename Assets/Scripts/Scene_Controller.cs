using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene_Controller : MonoBehaviour
{

    public static Scene_Controller instance; 

    public void Awake(){
        if(instance == null){
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }

        else{
            Destroy(gameObject); 
        }
    }
    
    public void NextLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
    
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadSceneAsync(sceneName); 
    }

    

    
 
}
