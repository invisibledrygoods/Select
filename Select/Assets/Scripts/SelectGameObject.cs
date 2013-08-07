using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public partial class Select
{
    public string[] names
    {
        get
        {
            return gameObjects.Keys.Select(i => i.name).ToArray();
        }
    }

    public Rigidbody[] rigidbodies
    {
        get
        {
            return gameObjects.Keys.Select(i => i.rigidbody).Where(i => i != null).ToArray();
        }
    }

    public Camera[] cameras
    {
        get
        {
            return gameObjects.Keys.Select(i => i.camera).Where(i => i != null).ToArray();
        }
    }

    public Light[] lights
    {
        get
        {
            return gameObjects.Keys.Select(i => i.light).Where(i => i != null).ToArray();
        }
    }

    public Animation[] animations
    {
        get
        {
            return gameObjects.Keys.Select(i => i.animation).Where(i => i != null).ToArray();
        }
    }

    public ConstantForce[] constantForces
    {
        get
        {
            return gameObjects.Keys.Select(i => i.constantForce).Where(i => i != null).ToArray();
        }
    }

    public Renderer[] renderers
    {
        get
        {
            return gameObjects.Keys.Select(i => i.renderer).Where(i => i != null).ToArray();
        }
    }

    public AudioSource[] audios
    {
        get
        {
            return gameObjects.Keys.Select(i => i.audio).Where(i => i != null).ToArray();
        }
    }

    public Collider[] colliders
    {
        get
        {
            return gameObjects.Keys.Select(i => i.collider).Where(i => i != null).ToArray();
        }
    }

    public T[] Get<T>()
        where T : Component
    {
        return gameObjects.Keys.Select(i => i.GetComponent<T>()).Where(i => i != null).ToArray();
    }

    public Select Disable<T>()
        where T : MonoBehaviour
    {
        foreach (T behaviour in Get<T>())
        {
            behaviour.enabled = false;
        }

        return this;
    }

    public Select Enable<T>()
        where T : MonoBehaviour
    {
        foreach (T behaviour in Get<T>())
        {
            behaviour.enabled = true;
        }

        return this;
    }

    public Select Activate()
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.SetActive(true);
        }

        return this;
    }

    public Select Deactivate()
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.SetActive(false);
        }

        return this;
    }

    public Select Show()
    {
        foreach (Renderer renderer in Get<Renderer>())
        {
            renderer.enabled = true;
        } 

        return this;
    }

    public Select Hide()
    {
        foreach (Renderer renderer in Get<Renderer>())
        {
            renderer.enabled = false;
        }

        return this;
    }

    public Select SetTag(string tag)
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.tag = tag;
        }
        return this;
    }

    public Select SetLayer(int layer)
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.layer = layer;
        }
        return this;
    }

    public Select SendMessage(string message, SendMessageOptions options)
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.SendMessage(message, options);
        }
        return this;
    }

    public Select SendMessage(string message)
    {
        return SendMessage(message, SendMessageOptions.DontRequireReceiver);
    }

    public Select AddComponent<T>()
        where T : Component
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            obj.AddComponent<T>();
        }
        return this;
    }

    public Select Destroy<T>()
        where T : Component
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                Component.Destroy(component);
            }
        }
        return this;
    }

    public Select Destroy()
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            GameObject.Destroy(obj);
        }
        return this;
    }

    public Select DontDestroyOnLoad()
    {
        foreach (GameObject obj in gameObjects.Keys)
        {
            GameObject.DontDestroyOnLoad(obj);
        }
        return this;
    }

    public Select Instantiate()
    {
        List<GameObject> clones = new List<GameObject>();

        foreach (GameObject obj in gameObjects.Keys)
        {
            GameObject clone = Object.Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
            clone.name = obj.name;
            clones.Add(clone);
        }

        return new Select(clones.ToArray());
    }
}
