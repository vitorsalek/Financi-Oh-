using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoalSystem : MonoBehaviour
{
    [Serializable]
    public class Goal
    {
        public int goalAmount;
        public int turn;
    }
    public List<Goal> goals = new List<Goal>();


}


