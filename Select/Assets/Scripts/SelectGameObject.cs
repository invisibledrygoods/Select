using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public partial class Select
{
    public string[] names
    {
        get
        {
            return this.Select(i => i.name).ToArray();
        }
    }

    public T[] Get<T>() where T : Component
    {
        return this.Select(i => i.GetComponent<T>()).Where(i => i != null).ToArray();
    }

    public Select Each(Action<GameObject> act)
    {
        foreach (GameObject obj in this)
        {
            act(obj);
        }
        return this;
    }

    public Select Each<T>(Action<T> act) where T : Component
    {
        foreach (T behaviour in Get<T>())
        {
            act(behaviour);
        }

        return this;
    }

    public T First<T>() where T : Component
    {
        foreach (T behaviour in Get<T>())
        {
            return behaviour;
        }

        return null;
    }

    public Select Disable<T>() where T : MonoBehaviour
    {
        return Each<T>(component => component.enabled = false);
    }

    public Select Enable<T>() where T : MonoBehaviour
    {
        return Each<T>(component => component.enabled = true);
    }

    public Select Activate()
    {
        return Each(obj => obj.SetActive(true));
    }

    public Select Deactivate()
    {
        return Each(obj => obj.SetActive(false));
    }

    public Select Show()
    {
        return Each<Renderer>(renderer => renderer.enabled = true);
    }

    public Select Hide()
    {
        return Each<Renderer>(renderer => renderer.enabled = false);
    }

    public Select SetTag(string tag)
    {
        return Each(obj => obj.tag = tag);
    }

    public Select SetLayer(int layer)
    {
        return Each(obj => obj.layer = layer);
    }

    public Select SendMessage(string message, SendMessageOptions options)
    {
        return Each(obj => obj.SendMessage(message, options));
    }

    public Select SendMessage(string message)
    {
        return SendMessage(message, SendMessageOptions.DontRequireReceiver);
    }

    public Select AddComponent<T>() where T : Component
    {
        return Each(obj => obj.AddComponent<T>());
    }

    public Select RemoveComponent<T>() where T : Component
    {
        return Each<T>(component => Component.Destroy(component));
    }

    public Select Destroy()
    {
        return Each(obj => GameObject.Destroy(obj));
    }

    public Select DontDestroyOnLoad()
    {
        return Each(obj => GameObject.DontDestroyOnLoad(obj));
    }

    public Select Instantiate()
    {
        List<GameObject> clones = new List<GameObject>();

        foreach (GameObject obj in this)
        {
            GameObject clone = UnityEngine.Object.Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
            clone.name = obj.name;
            clones.Add(clone);
        }

        return new Select(clones.ToArray());
    }
}
