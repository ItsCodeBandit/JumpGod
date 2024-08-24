using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Flag : MonoBehaviour{

    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player")){
           Scene_Controller.instance.NextLevel();
        }
    } 
    





}
