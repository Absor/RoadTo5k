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

    void Awake()
    {
        currentGameState = new GameState();
        setGameStateDefaults();
        informAboutUpdate();
    }

    private void setGameStateDefaults()
    {
        currentGameState.statuses = new Dictionary<StatusType, Status>();
        currentGameState.temporaryChanges = new List<StatusChange>();
        StatusType[] types = Enum.GetValues(typeof(StatusType)) as StatusType[];
        foreach (StatusType type in types)
        {
            Status status = new Status(type, 0, 100);
            currentGameState.statuses.Add(type, status);
        }
        currentGameState.statuses[StatusType.Skillpoints_Assign_Amount].points = 4;
        currentGameState.statuses[StatusType.Skillpoints_Available].points = 0;
        currentGameState.statuses[StatusType.Time].points = 24 * 60 + 17 * 60 + 30;
        currentGameState.statuses[StatusType.Day_Start_Time_Min].points = 17 * 60;
        currentGameState.statuses[StatusType.Day_Start_Time_Max].points = 18 * 60;
        currentGameState.statuses[StatusType.Rage_Gain_Modifier].points = 100;
		currentGameState.statuses[StatusType.Rating].points = 2200;
    }

    private void informAboutUpdate()
    {
        OnGameStateUpdate.Invoke();
    }

    public int GetAvailableSkillPoints()
    {
        return currentGameState.statuses[StatusType.Skillpoints_Available].points / currentGameState.statuses[StatusType.Skillpoints_Assign_Amount].points;
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
            status.points += currentGameState.statuses[StatusType.Skillpoints_Assign_Amount].points;
            currentGameState.statuses[StatusType.Skillpoints_Available].points -= currentGameState.statuses[StatusType.Skillpoints_Assign_Amount].points;
            informAboutUpdate();
        }
    }

    public GameTime GetGameTime()
    {
        int minutes = currentGameState.statuses[StatusType.Time].points;
        return new GameTime(minutes / (60 * 24), minutes / 60 % 24, minutes % 60);
    }

    public void AdvanceToNextDay()
    {
        int nextDay = currentGameState.statuses[StatusType.Time].points / (60 * 24) + 1;
        int startMinutes = UnityEngine.Random.Range(currentGameState.statuses[StatusType.Day_Start_Time_Min].points, currentGameState.statuses[StatusType.Day_Start_Time_Max].points);
        currentGameState.statuses[StatusType.Time].points = nextDay * 24 * 60 + startMinutes;

        while (currentGameState.temporaryChanges.Count > 0)
        {
            StatusChange change = currentGameState.temporaryChanges[0];
            applyStatusChange(change);
            currentGameState.temporaryChanges.RemoveAt(0);
        }

        StatusChangeLoggerScript.Instance.AddDay();
        StatusChangeLoggerScript.Instance.PrintLog();
        informAboutUpdate();
    }

    public void ApplyStatusChanges(List<StatusChange> changes)
    {
        foreach (StatusChange change in changes)
        {
            if (!change.permanent)
            {
                if (change.replace)
                {
                    Status old = GetStatus(change.statusType);
                    int oldValue = old.points;
                    applyStatusChange(change);
                    change.value = oldValue;
                    currentGameState.temporaryChanges.Add(change);
                }
                else
                {
                    applyStatusChange(change);
                    change.value = -change.value;
                    currentGameState.temporaryChanges.Add(change);
                }
            }
            else
            {
                applyStatusChange(change);
            }
        }
        informAboutUpdate();
    }

    private void applyStatusChange(StatusChange change)
    {
        Status status = GetStatus(change.statusType);
        if (change.replace)
        {
            status.points = change.value;
        }
        else
        {
            if (change.statusType == StatusType.Rage)
            {
                GainRage(change.value);
            }
            else if (change.statusType.IsAPlayerAttribute())
            {
                status.points += change.value * currentGameState.statuses[StatusType.Skillpoints_Assign_Amount].points;
                StatusChangeLoggerScript.Instance.LogStatusChange(change.statusType, change.value);
            }
            else
            {
                status.points += change.value;
                StatusChangeLoggerScript.Instance.LogStatusChange(change.statusType, change.value);
            }
        }
        AttributeUiManagerScript.Instance.PointsGained((change.value > 0 ? "+" + change.value : "" + change.value) + " " + change.statusType.ToNiceString());
    }

    public void GainRage(int points)
    {
        Status status = GetStatus(StatusType.Rage);
        int gain = Mathf.RoundToInt(points * currentGameState.statuses[StatusType.Rage_Gain_Modifier].GetNormalizedPoints());
        status.points += gain;
        StatusChangeLoggerScript.Instance.LogStatusChange(StatusType.Rage, gain);
        informAboutUpdate();
    }

    public void GainRating(int points)
    {
        Status status = GetStatus(StatusType.Rating);
        status.points += points;
        StatusChangeLoggerScript.Instance.LogStatusChange(StatusType.Rating, points);
        informAboutUpdate();
    }

    public Player GetRealPlayer()
    {
        string name = "You";
        float rage = currentGameState.statuses[StatusType.Rage].GetNormalizedPoints();
        float charisma = currentGameState.statuses[StatusType.Charisma].GetNormalizedPoints();
        float luck = currentGameState.statuses[StatusType.Luck].GetNormalizedPoints();
        float talent = currentGameState.statuses[StatusType.Talent].GetNormalizedPoints();
        float knowledgeCarry = currentGameState.statuses[StatusType.Knowledge_Carry].GetNormalizedPoints();
        float knowledgeGanker = currentGameState.statuses[StatusType.Charisma].GetNormalizedPoints();
        float knowledgeSupport = currentGameState.statuses[StatusType.Charisma].GetNormalizedPoints();

        return new Player(name, rage, charisma, luck, talent, knowledgeCarry, knowledgeGanker, knowledgeSupport);
    }

    public void AdvanceTime(int minutes)
    {
        currentGameState.statuses[StatusType.Time].points += minutes;
        informAboutUpdate();
    }
}
