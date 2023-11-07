using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestProgress {NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE }

    public string title;
    public int id;
    public QuestProgress progress;
    public string description;
    public string hint;
    public string congratulation;
    public string summary;
    public int nextQuest;

    public string questObjective; // name of the quest objective(also for remove)
    public int questObjectiveCount; // current number of questObjective count
    public int questObjectiveRequirement; // required amount of quest objective objects

    public int expReward;
    public int goldReward;
    public string itemReward;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
