using System;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvaInteractInformation;
    [SerializeField] private GameObject computerCanvas;
    [SerializeField] private float valueHungerGiven;
    [SerializeField] private float moneyTaken;

    [SerializeField] private float valueSleepGiven;
    [SerializeField] private float valueTimeGiven;

    [SerializeField] private TriggerState triggerState; 

    private bool inTriggerZone = false;

    public enum TriggerState
    {
        Bed,
        Fridge,
        Computer
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        inTriggerZone = true; 
        Debug.Log("Entered Trigger Zone: " + triggerState);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        inTriggerZone = false; 
        Debug.Log("Exited Trigger Zone");
    }

    private void Update()
    {
            canvaInteractInformation.SetActive(inTriggerZone);
        
        if (inTriggerZone && Input.GetKeyDown(KeyCode.F))
        {
            switch (triggerState)
            {
                case TriggerState.Bed:
                    Debug.Log("Sleeping...");
                    GameManager.instance.sleep += valueSleepGiven;
                    GameManager.instance.elapsedTime += valueTimeGiven;
                    break;

                case TriggerState.Fridge:
                    Debug.Log("Eating...");
                    GameManager.instance.hanger += valueHungerGiven;
                    GameManager.instance.money -= moneyTaken;
                    break;

                case TriggerState.Computer:
                    Debug.Log("Using computer...");
                    if (computerCanvas != null)
                    {
                        canvaInteractInformation.SetActive(false);
                        computerCanvas.SetActive(true);
                    }
                    break;

                default:
                    Debug.Log("No valid trigger state.");
                    break;
            }
        }
    }
}
