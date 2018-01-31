using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        TIME, EVENT
    }

    public enum EventType
    {
        NO_ENEMIES, NO_GOOD_ITEMS,
        CUSTOM
    }

    public TriggerType triggerType;
    public EventType eventType;

    private PenaltyManager PM;

    public int InitialDelay = 0;

    /// <summary>
    /// This is the time passed between a check of event and another
    /// </summary>
    public int SecondsBetweenCheck = 1;

    /// <summary>
    /// This is used only if TriggerType is set to Time.
    /// This will increase the penaltyInfoNow every tot.
    /// </summary>
    public int SecondsBetweenIncrease = 1;

    private bool TimeGoing = true;

    void Start()
    {
        PM = gameObject.GetComponent<PenaltyManager>();
        if (PM.PolicyIncreaseType == GamePolicy.IncreaseType.DYNAMIC)
        {
            switch (triggerType)
            {
                case TriggerType.TIME: InvokeRepeating("TimeTrigger", InitialDelay, SecondsBetweenIncrease); break;
                case TriggerType.EVENT: InvokeRepeating("EventTriggerIncrease", InitialDelay, SecondsBetweenCheck); break;
                default: Debug.Log("Unknown TriggerType"); break;
            }
        }
        else Destroy(this);
    }

    void TimeTrigger()
    {
        if(TimeGoing)
        {
            Debug.Log("Time is going");
            PM.IncreasePenaltyNow();
        }
        else
        {
            Debug.Log("Time is stopped");
        }
    }

    public void EventDeath()
    {
        PM.ResetPenalty();
    }

    void EventTriggerIncrease()
    {
        switch(eventType)
        {
            case (EventType.NO_ENEMIES):
                {
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("EnemyTag");
                    if(gos.Length == 0)
                    {
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
            case (EventType.NO_GOOD_ITEMS):
                {
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("GoodItemTag");
                    if (gos.Length == 0)
                    {
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
            default:
                {
                    if (Condition())
                    {
                        Debug.Log("Custom Condition reached");
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// Override this is your son of PenaltyTrigger and write
    /// your own condition. When this is true, the PenaltyManager will increase the penalty
    /// </summary>
    /// <returns></returns>
    protected virtual bool Condition()
    {
        return false;
    }
   
}
