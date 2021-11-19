using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static bool HasLabel(this GameObject gameObject, Labels label)
    {
        if (gameObject.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }

    public static bool HasLabel(this Collision2D collision, Labels label)
    {
        if (collision.gameObject.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }

    public static bool HasLabel(this Collider2D collision, Labels label)
    {
        if (collision.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }
}
