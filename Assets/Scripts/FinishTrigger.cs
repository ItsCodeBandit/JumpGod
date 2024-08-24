using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject square0;  
    public GameObject Dope_shoe01;
    public GameObject Dope_shoe02;

    private void Start()
    {
        // should hide shoes and text
        square0.SetActive(false);
        Dope_shoe01.SetActive(false);
        Dope_shoe02.SetActive(false);
    }

   private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        //shows 
        square0.SetActive(true); 
        Dope_shoe01.SetActive(true);
        Dope_shoe02.SetActive(true);
    }
}


}