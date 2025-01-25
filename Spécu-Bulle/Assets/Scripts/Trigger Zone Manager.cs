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
    public float timer = 1.0f;
    public bool isSleep = false;

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
        if (GameManager.instance.sleep > 100)GameManager.instance.sleep = 100;
        
        {canvaInteractInformation.SetActive(inTriggerZone);}
        
        if (inTriggerZone && Input.GetKeyDown(KeyCode.F) && isSleep == false)
        {
            switch (triggerState)
            {
                case TriggerState.Bed:
                    isSleep = true;
                    PlayerCOntroller.instance.enabled = false;
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

        if (!isSleep) return;
        if (Input.GetKeyUp(KeyCode.F))
        {
            isSleep = false;
            PlayerCOntroller.instance.rbPlayer.linearVelocity = Vector2.zero;
        }
                        
        if (timer <= 0)
        {
            timer = 1.0f;
            GameManager.instance.sleep += 10;
        }
        else
        {
            Debug.Log("Sleep");
            timer -= Time.deltaTime;
        }
    }
}
