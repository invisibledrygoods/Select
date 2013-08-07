using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

public partial class Select : IEnumerable<GameObject>
{
    Dictionary<GameObject, bool> gameObjects;

    string query;
    public string selector
    {
        get
        {
            return query;
        }
    }
    
    public Select(params GameObject[] objects)
    {
        gameObjects = new Dictionary<GameObject, bool>();
        this.query = "";

        foreach (GameObject obj in objects)
        {
            gameObjects[obj] = true;
        }
    }

    public Select(string query)
    {
        gameObjects = new Dictionary<GameObject, bool>();
        this.query = query;

        string head = query; 
        string tail = "";

        if (query.Contains(','))
        {
            head = query.Substring(0, query.IndexOf(','));
            tail = query.Substring(query.IndexOf(',') + 1);
        }

        foreach (Match match in Regex.Matches(" " + head.Trim(), @"(#[\w-]+|.[\w_]+|\s+#[\w-]+|\s+\w+|\s+.[\w_]+)"))
        {
            if (match.Value.StartsWith(" ."))
            {
                FindByClass(match.Value.Trim(" .".ToCharArray()));
            }
            else if (match.Value.StartsWith(" #"))
            {
                FindByName(match.Value.Trim(" #".ToCharArray()));
            }
            else if (match.Value.StartsWith(" "))
            {
                FindByTag(match.Value.Trim());
            } else if (match.Value.StartsWith("."))
            {
                PruneWithoutClass(match.Value.Trim(" .".ToCharArray()));
            }
            else if (match.Value.StartsWith("#"))
            {
                PruneWithoutName(match.Value.Trim("#".ToCharArray()));
            }
        }

        if (tail != "")
        {
            Union(new Select(tail.Trim()));
        }
    }

    void Union(Select selection)
    {
        foreach (GameObject obj in selection)
        {
            gameObjects[obj] = true;
        }
    }

    void FindByClass(string className)
    {
        System.Type type = System.Type.GetType(className) ?? Assembly.Load("UnityEngine").GetType("UnityEngine." + className);

        if (type != null)
        {
            if (gameObjects.Count != 0)
            {
                List<GameObject> parents = gameObjects.Keys.ToList();
                gameObjects = new Dictionary<GameObject, bool>();
                foreach (GameObject parent in parents)
                {
                    foreach (Component component in parent.GetComponentsInChildren(type))
                    {
                        if (component.gameObject != parent)
                        {
                            gameObjects[component.gameObject] = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Component component in Resources.FindObjectsOfTypeAll(type))
                {
                    gameObjects[component.gameObject] = true;
                }
            }
        }
    }

    void FindByName(string name)
    {
        if (gameObjects.Count != 0)
        {
            List<GameObject> parents = gameObjects.Keys.ToList();
            gameObjects = new Dictionary<GameObject, bool>();
            foreach (GameObject parent in parents)
            {
                foreach (Transform transform in parent.GetComponentsInChildren<Transform>())
                {
                    if (transform.gameObject != parent && transform.name == name)
                    {
                        gameObjects[transform.gameObject] = true;
                    }
                }
            }
        }
        else
        {
            foreach (Transform transform in Resources.FindObjectsOfTypeAll(typeof(Transform)))
            {
                if (transform.name == name)
                {
                    gameObjects[transform.gameObject] = true;
                }
            }
        }
    }

    void FindByTag(string tag)
    {
        // TODO: if gameObjects isn't empty find in children
        if (gameObjects.Count != 0)
        {
            List<GameObject> parents = gameObjects.Keys.ToList();
            gameObjects = new Dictionary<GameObject, bool>();
            foreach (GameObject parent in parents)
            {
                foreach (Transform transform in parent.GetComponentsInChildren<Transform>())
                {
                    if (transform.gameObject != parent && transform.tag == tag)
                    {
                        gameObjects[transform.gameObject] = true;
                    }
                }
            }
        }
        else
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                gameObjects[obj] = true;
            }
        }
    }

    void PruneWithoutClass(string className)
    {
        System.Type type = System.Type.GetType(className) ?? Assembly.Load("UnityEngine").GetType("UnityEngine." + className);

        List<GameObject> toRemove = gameObjects.Keys.ToList().Where((GameObject obj) =>
            {
                return obj.GetComponent(type) == null;
            }).ToList();

        foreach (GameObject remove in toRemove)
        {
            gameObjects.Remove(remove);
        }
    }

    void PruneWithoutName(string name)
    {
        List<GameObject> toRemove = gameObjects.Keys.ToList().Where((GameObject obj) =>
            {
                return obj.name != name;
            }).ToList();

        foreach (GameObject remove in toRemove)
        {
            gameObjects.Remove(remove);
        }
    }

    public GameObject this[int i]
    {
        get
        {
            return gameObjects.Keys.ToArray()[i];
        }
    }

    #region IEnumerable<GameObject> Members

    public IEnumerator<GameObject> GetEnumerator()
    {
        return gameObjects.Keys.ToList().GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return gameObjects.Keys.ToList().GetEnumerator();
    }

    #endregion
}
