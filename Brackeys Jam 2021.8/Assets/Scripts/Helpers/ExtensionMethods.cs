using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Labels = Label.Labels;

public static class ExtensionMethods
{
    public static bool HasLabel(this GameObject gameObject, Labels label)
    {
        bool hasFlag = false;

        if (gameObject.TryGetComponent(out TagManager tagManager))
        {
            hasFlag = tagManager.HasLabel(label);
        }

        return hasFlag;
    }

    public static bool HasLabel(this Collision2D collision, Labels label)
    {
        bool hasFlag = false;

        if (collision.gameObject.TryGetComponent(out TagManager tagManager))
        {
            hasFlag = tagManager.HasLabel(label);
        }

        return hasFlag;
    }

    public static bool HasLabel(this Collider2D collision, Labels label)
    {
        bool hasFlag = false;

        if (collision.TryGetComponent(out TagManager tagManager))
        {
            hasFlag = tagManager.HasLabel(label);
        }

        return hasFlag;
    }
}
