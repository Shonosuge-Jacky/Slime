using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Event")]
public class DayNightEvent : ScriptableObject
{
    [SerializeField] private List<FloorGrid> listeners = new List<FloorGrid>();
    //TriggerEvent for Day Night Trigger
    public void TriggerDayNightChangeEvent(DayNight dayNight)
    {
        Debug.Log("TriggerDayNightChangeEvent");
        for (int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnDayNightChange(dayNight);
        }
    }

    public void AddListener(FloorGrid listener)
    {
        if(!listeners.Contains(listener)){
            listeners.Add(listener);
        }
        
    }

    public void RemoveListener(FloorGrid listener)
    {
        listeners.Remove(listener);
    }

    public void ClearEventListeners(){
        listeners.Clear();
    }
}