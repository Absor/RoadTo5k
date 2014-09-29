using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FightAnimatorScript : MonoBehaviour {

    public GameObject characterPrefab;

    public float secondsPerSquare;

    public int tileHeight = 96;
    public int tileWidth = 96;

    private Vector2 areaSize;
    private FightCharacter[] fightCharacters;

    // middle row is for fighting, second from middle is for heroes fighting, 3rd from middle is for whole team
    private int team1TeamRow = -2;
    private int team1FightRow = -1;
    private int team2TeamRow = 2;
    private int team2FightRow = 1;
    private int combatRow = 0;
    private int centerColumn = 0;

	void Awake () {
        RectTransform rectTransform = GetComponent<RectTransform>();
        areaSize = rectTransform.sizeDelta;
        fightCharacters = new FightCharacter[10];

        for (int i = 0; i < 10; i++)
        {
            GameObject character = Instantiate(characterPrefab) as GameObject;
            FightCharacterAnimatorScript animatorScript = character.GetComponent<FightCharacterAnimatorScript>();
            character.transform.parent = transform;
            character.transform.localRotation = Quaternion.identity;
            int gridX, gridY;
            if (i < 5)
            {
                // -2 because we have 5 heroes per side so the third goes to middle
                // i:th hero goes from 0 to 5.
                gridX = centerColumn - 2 + i;
                gridY = team1TeamRow;
            }
            else
            {
                // same as in if true part but -5 more because we are handling next team of heroes
                gridX = centerColumn - 7 + i;
                gridY = team2TeamRow;
            }
            Vector3 newPosition = getWorldPosition(gridX, gridY);
            character.transform.localPosition = newPosition;
            // Scale char to tile size
            character.transform.localScale = Vector3.one * tileHeight;

            FightCharacter fightCharacter = new FightCharacter();
            fightCharacter.animatorScript = animatorScript;
            fightCharacter.gridX = gridX;
            fightCharacter.gridY = gridY;
            fightCharacter.teamGridX = gridX;
            fightCharacter.teamGridY = gridY;
            fightCharacter.transform = character.transform;
            fightCharacters[i] = fightCharacter;
        }
	}

    private Vector3 getWorldPosition(int gridX, int gridY)
    {
        float offSetX = areaSize.x % tileWidth;
        float offSetY = areaSize.y % tileHeight;
        return new Vector3(gridX * tileWidth, gridY * tileHeight, 0);
    }

    public void Reset()
    {
        for (int i = 0; i < 10; i++)
        {
            FightCharacterAnimatorScript script = fightCharacters[i].animatorScript;
            script.Reset();
            if (i < 5)
            {
                script.Face(script.UP);
            }
            else
            {
                script.Face(script.DOWN);
            }
        }
    }

    public IEnumerator PlayFight(List<FightEvent> fightEvents, Action Done)
    {
        // TODO REMOVE BELOW
        fightEvents = new List<FightEvent>();
        FightEvent join = new FightEvent();
        join.eventType = FightEventType.JOIN_FIGHT;
        FightEventTarget target = new FightEventTarget();
        target.targetType = FightEventTargetType.HERO;
        target.id = 0;
        join.targets.Add(target);
        target = new FightEventTarget();
        target.targetType = FightEventTargetType.HERO;
        target.id = 3;
        join.targets.Add(target);
        target = new FightEventTarget();
        target.targetType = FightEventTargetType.HERO;
        target.id = 6;
        join.targets.Add(target);
        fightEvents.Add(join);
        FightEvent leave = new FightEvent();
        leave.eventType = FightEventType.LEAVE_FIGHT;
        fightEvents.Add(leave);
        // TODO REMOVE ABOVE

        foreach (FightEvent fightEvent in fightEvents)
        {
            float animationDuration = playEventAnimation(fightEvent);
            yield return new WaitForSeconds(animationDuration);
        }

        Done();
    }

    // returns fight event animation duration
    private float playEventAnimation(FightEvent fightEvent)
    {
        switch (fightEvent.eventType)
        {
            case FightEventType.JOIN_FIGHT:
                return playJoinAnimations(fightEvent.targets);
            case FightEventType.LEAVE_FIGHT:
                return playLeaveAnimations();
        }
        return 0;
    }

    private float playLeaveAnimations()
    {
        float maxAnimationDuration = 0;

        foreach (FightCharacter character in fightCharacters)
        {
           if (character.gridX != character.teamGridX || character.gridY != character.teamGridY)
           {
               // Old as animation start
               character.animationFromX = character.gridX;
               character.animationFromY = character.gridY;
               // Set new location (target)
               character.gridX = character.teamGridX;
               character.gridY = character.teamGridY;
               // Set animation target
               character.animationToX = character.gridX;
               character.animationToY = character.gridY;
               float animationDuration = moveAnimationDuration(character);
               StartCoroutine(moveAnimation(character, true));
               if (animationDuration > maxAnimationDuration)
               {
                   maxAnimationDuration = animationDuration;
               }
           }
        }

        return maxAnimationDuration;
    }

    // returns max join animation duration
    private float playJoinAnimations(List<FightEventTarget> targetList)
    {
        float maxAnimationDuration = 0;

        foreach (FightEventTarget target in targetList)
        {
            FightCharacter character = fightCharacters[target.id];
            // Old as animation start
            character.animationFromX = character.gridX;
            character.animationFromY = character.gridY;
            // Set new location (target)
            character.gridY = character.teamGridY == team1TeamRow ? team1FightRow : team2FightRow;
            // TODO if we want to group together in X
            // Set animation target
            character.animationToX = character.gridX;
            character.animationToY = character.gridY;
            float animationDuration = playJoinAnimation(character);
            if (animationDuration > maxAnimationDuration)
            {
                maxAnimationDuration = animationDuration;
            }
        }

        return maxAnimationDuration;
    }

    // returns join animation duration
    private float playJoinAnimation(FightCharacter character)
    {
        StartCoroutine(moveAnimation(character, false));

        return moveAnimationDuration(character);
    }

    private IEnumerator moveAnimation(FightCharacter character, bool horizontalFirst)
    {
        int startDirection = character.animatorScript.GetFacing();
        int verticalDirection, horizontalDirection;
        if (character.animationToX > character.animationFromX)
        {
            horizontalDirection = character.animatorScript.RIGHT;
        }
        else if (character.animationToX < character.animationFromX)
        {
            horizontalDirection = character.animatorScript.LEFT;
        }
        else
        {
            horizontalDirection = startDirection;
        }

        if (character.animationToY > character.animationFromY)
        {
            verticalDirection = character.animatorScript.UP;
        }
        else if (character.animationToY < character.animationFromY)
        {
            verticalDirection = character.animatorScript.DOWN;
        }
        else
        {
            verticalDirection = startDirection;
        }

        character.animatorScript.Walk();

        if (horizontalFirst)
        {
            character.animatorScript.Face(horizontalDirection);

            float animationDuration = horizontalMoveAnimationDuration(character);
            StartCoroutine(horizontalMoveAnimation(character));
            yield return new WaitForSeconds(animationDuration);

            character.animatorScript.Face(verticalDirection);

            animationDuration = verticalMoveAnimationDuration(character);
            StartCoroutine(verticalMoveAnimation(character));
            yield return new WaitForSeconds(animationDuration);
        }
        else
        {
            character.animatorScript.Face(verticalDirection);

            float animationDuration = verticalMoveAnimationDuration(character);
            StartCoroutine(verticalMoveAnimation(character));
            yield return new WaitForSeconds(animationDuration);

            character.animatorScript.Face(horizontalDirection);

            animationDuration = horizontalMoveAnimationDuration(character);
            StartCoroutine(horizontalMoveAnimation(character));
            yield return new WaitForSeconds(animationDuration);
        }
        character.animatorScript.Face(startDirection);
        character.animatorScript.Idle();
    }

    private float moveAnimationDuration(FightCharacter character)
    {
        return horizontalMoveAnimationDuration(character) + verticalMoveAnimationDuration(character);
    }

    private IEnumerator horizontalMoveAnimation(FightCharacter character)
    {
        if (character.animationFromX != character.animationToX)
        {
            Vector3 newWorldPosition = getWorldPosition(character.animationToX, character.animationToY);
            Vector3 startingPos = character.transform.localPosition;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / (secondsPerSquare * Mathf.Abs(character.animationToX - character.animationFromX)));
                float newX = Mathf.Lerp(startingPos.x, newWorldPosition.x, t);
                character.transform.localPosition = new Vector3(newX, character.transform.localPosition.y, character.transform.localPosition.z);
                yield return 0;
            }
        }        
    }

    private float horizontalMoveAnimationDuration(FightCharacter character)
    {
        return Mathf.Abs(character.animationFromX - character.animationToX) * secondsPerSquare;
    }

    private IEnumerator verticalMoveAnimation(FightCharacter character)
    {
        if (character.animationFromY != character.animationToY)
        {
            Vector3 newWorldPosition = getWorldPosition(character.animationToX, character.animationToY);
            Vector3 startingPos = character.transform.localPosition;
            float t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / (secondsPerSquare * Mathf.Abs(character.animationToY - character.animationFromY)));
                float newY = Mathf.Lerp(startingPos.y, newWorldPosition.y, t);
                character.transform.localPosition = new Vector3(character.transform.localPosition.x, newY, character.transform.localPosition.z);
                yield return 0;
            }
        }
}

    private float verticalMoveAnimationDuration(FightCharacter character)
    {
        return Mathf.Abs(character.animationFromY - character.animationToY) * secondsPerSquare;
    }
}
