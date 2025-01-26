using System;
using System.Collections;
using UnityEngine;

public class TriggerZoneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvaInteractInformation;
    [SerializeField] private GameObject computerCanvas;
    [SerializeField] private float valueHungerGiven;
    [SerializeField] private int moneyTaken;

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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        inTriggerZone = true; 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        inTriggerZone = false; 
    }
    
    
    private void Update()
    {
        if (GameManager.instance.sleep > 100)GameManager.instance.sleep = 100;
        if(GameManager.instance.hanger > 100)GameManager.instance.hanger = 100;
        canvaInteractInformation.SetActive(inTriggerZone && !computerCanvas.activeSelf);

        
        if (inTriggerZone && Input.GetKeyDown(KeyCode.F) && isSleep == false)
        {
            switch (triggerState)
            {
                case TriggerState.Bed:
                    isSleep = true;
                    SoundManager.instance.PlaySound(SoundManager.instance.sleepSound, transform.position);
                    PlayerCOntroller.instance.enabled = false;
                    break;

                case TriggerState.Fridge:
                    GameManager.instance.hanger += valueHungerGiven;
                    StockManager.instance.playerMoney-= moneyTaken;
                    SoundManager.instance.PlaySound(SoundManager.instance.eatingSound, transform.position);
                    break;

                case TriggerState.Computer:
                    inTriggerZone = false;
                    SoundManager.instance.PlaySound(SoundManager.instance.computeurSound, transform.position);
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
