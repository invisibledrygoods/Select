using UnityEngine;
using System.Collections;
using Require;

public class UsesSelect : MonoBehaviour
{
    GameObject[] restrictedTo;

    public void SetRestriction(params GameObject[] objects)
    {
        restrictedTo = objects;
    }

    public Select Select(string query) {
        return RestrictedSelect(new Select(query));
    }

    public Select Select(params GameObject[] objects)
    {
        return RestrictedSelect(new Select(objects));
    }

    Select RestrictedSelect(Select selection)
    {
        if (restrictedTo == null)
        {
            return selection;
        }

        Select restrictedSelection = new Select();

        foreach (GameObject selected in selection)
        {
            foreach (GameObject restricted in restrictedTo)
            {
                if (restricted == selected)
                {
                    restrictedSelection.Union(new Select(restricted));
                }
            }
        }

        return restrictedSelection;
    }
}
