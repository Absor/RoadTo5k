using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

public class GameStateManagerScript : Singleton<GameStateManagerScript>
{
    private GameState currentGameState;

    public class GameStateUpdate : UnityEvent {}
    public GameStateUpdate OnGameStateUpdate = new GameStateUpdate();

    void Start()
    {
        currentGameState = new GameState();
        setGameStateDefaults();
        informAboutUpdate();
    }

    private void setGameStateDefaults()
    {
        currentGameState.assignAmount = 4;
        currentGameState.skillPointsAvailable = 100;
        currentGameState.statuses = new Dictionary<StatusType,Status>();
        StatusType[] types = Enum.GetValues(typeof(StatusType)) as StatusType[];
        foreach (StatusType type in types)
        {
            Status status = new Status(type, 0, 100);
            currentGameState.statuses.Add(type, status);
        }

        currentGameState.time = new GameTime(1, 17, 30);
        currentGameState.startOfNewDayTime = new GameTimeRange();
        currentGameState.startOfNewDayTime.min = new GameTime(0, 17, 0);
        currentGameState.startOfNewDayTime.max = new GameTime(0, 18, 0);
    }

    public void ApplyOutcome(GameEventOutcome outcome)
    {
        informAboutUpdate();
    }

    private void informAboutUpdate()
    {
        OnGameStateUpdate.Invoke();
    }

    public int GetAvailableSkillPoints()
    {
        return currentGameState.skillPointsAvailable / currentGameState.assignAmount;
    }

    public Status GetStatus(StatusType type)
    {
        return currentGameState.statuses[type];
    }

    public void AssignPoint(StatusType type)
    {
        if (GameStateManagerScript.Instance.GetAvailableSkillPoints() > 0)
        {
            Status status = GetStatus(type);
            status.points += currentGameState.assignAmount;
            currentGameState.skillPointsAvailable -= currentGameState.assignAmount;
            informAboutUpdate();
        }
    }

    internal GameTime GetGameTime()
    {
        return currentGameState.time;
    }

    internal void AdvanceToNextDay()
    {
        currentGameState.time.day += 1;
        GameTime newDayStartTime = currentGameState.startOfNewDayTime.GetTimeInRange();
        currentGameState.time.hour = newDayStartTime.hour;
        currentGameState.time.minute = newDayStartTime.minute;
        informAboutUpdate();
    }
}
