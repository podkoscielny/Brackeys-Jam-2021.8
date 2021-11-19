using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Label = Labels.Label;

public static class ExtensionMethods
{
    public static bool HasLabel(this GameObject gameObject, Label label)
    {
        if (gameObject.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }

    public static bool HasLabel(this Collision2D collision, Label label)
    {
        if (collision.gameObject.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }

    public static bool HasLabel(this Collider2D collision, Label label)
    {
        if (collision.TryGetComponent(out TagManager tagManager))
        {
            return tagManager.labels.HasFlag(label);
        }

        return false;
    }
}
