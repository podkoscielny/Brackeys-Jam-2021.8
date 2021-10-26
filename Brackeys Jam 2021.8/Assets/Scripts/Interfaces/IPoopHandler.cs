using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoopHandler
{
    public void HandlePoopHit(Vector2 splashPosition);
    public void HandleExplosion(Vector2 direction);
}
