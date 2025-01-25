using System;
using System.Collections;
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
    private float timer = 1.0f;
    private bool  isSleep = false;

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
    }
    
    
    private void Update()
    {
        {canvaInteractInformation.SetActive(inTriggerZone);}
        
        if (inTriggerZone && Input.GetKeyDown(KeyCode.F) && isSleep == false)
        {
            switch (triggerState)
            {
                case TriggerState.Bed:
                    bool isSleep = true;
                    while (isSleep)
                    {
                        Debug.Log("IN");
                        if (timer <= 0)
                        {
                            timer = 1.0f;
                            GameManager.instance.sleep += 10;
                        }
                        else
                        {
                            timer -= Time.deltaTime;
                        }
                        PlayerCOntroller.instance.enabled = false;
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            isSleep = false;
                            PlayerCOntroller.instance.enabled = true;
                        }
                    }
                    break;

                case TriggerState.Fridge:
                    GameManager.instance.hanger += valueHungerGiven;
                    GameManager.instance.money -= moneyTaken;
                    break;

                case TriggerState.Computer:
                    inTriggerZone = false;
                    if (computerCanvas != null)
                    {
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
