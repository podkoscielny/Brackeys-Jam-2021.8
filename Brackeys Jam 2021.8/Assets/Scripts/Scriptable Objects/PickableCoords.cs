using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickableCoords", menuName = "ScriptableObjects/PickableCoords")]
public class PickableCoords : ScriptableObject
{
    [SerializeField] Vector2Pair[] positions;

    public Vector2 GetRandomPosition()
    {
        int randomPositionIndex = Random.Range(0, positions.Length);
        Vector2Pair vector2Pair = positions[randomPositionIndex];

        float xPosition = Random.Range(vector2Pair.leftBound.x, vector2Pair.rightBound.x);
        float yPosition = vector2Pair.leftBound.y;

        Vector2 positionToReturn = new Vector2(xPosition, yPosition);

        return positionToReturn;
    }
}