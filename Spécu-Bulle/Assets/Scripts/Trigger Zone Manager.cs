using System;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvaInteractInformation;
    [SerializeField] private GameObject computerCanvas;
    [SerializeField] private float valueHungerGiven;
    [SerializeField] private float moneyTaken;

    [SerializeField]
    private float valueSleepGiven;

    [SerializeField] private float valueTimeGiven;
    private void OnTriggerEnter2D(Collider2D other) //for bed
    {
        canvaInteractInformation.SetActive(true);
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (other.gameObject.layer)
            {
                case 3 : GameManager.instance.sleep += valueSleepGiven;
                    GameManager.instance.elapsedTime += valueTimeGiven;
                    break;
                case 7 : computerCanvas.SetActive(true);
                    break;
                case 6 : GameManager.instance.hanger += valueHungerGiven;
                    GameManager.instance.money -= moneyTaken;
                    break;
            }
            
        }
        
    }
}
