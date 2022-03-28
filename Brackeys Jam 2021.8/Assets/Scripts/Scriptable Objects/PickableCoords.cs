using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickableCoords", menuName = "ScriptableObjects/PickableCoords")]
public class PickableCoords : ScriptableObject
{
    [SerializeField] SpawnBounds[] positions;

    public Vector2 GetRandomPosition()
    {
        int randomPositionIndex = Random.Range(0, positions.Length);
        SpawnBounds vector2Pair = positions[randomPositionIndex];

        float xPosition = Random.Range(vector2Pair.leftBoundX, vector2Pair.rightBoundX);
        float yPosition = vector2Pair.boundY;

        Vector2 positionToReturn = new Vector2(xPosition, yPosition);

        return positionToReturn;
    }
}