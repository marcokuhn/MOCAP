using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    //[SerializeField]
    //private bool _persistent = true;

    public static T Instance
    {
        get
        {
            //if (applicationIsQuitting)
            //{
            //    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
            //        "' already destroyed on application quit." +
            //        " Won't create again - returning null.");
            //    return null;
            //}

            lock (_lock)
            {
                if (_instance == null)
                {
                    var _instances = FindObjectsOfType(typeof(T));
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (_instances.Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it." + "Destroying all but one Instance");
                        //for (var i = 1; i < _instances.Length; i++)
                        //    Destroy(_instances[i]);
                        return _instance = (T)_instances[0];
                    }

                    if (_instance == null)
                    {
                        //GameObject singleton = new GameObject();
                        //_instance = singleton.AddComponent<T>();
                        //singleton.name = "(singleton) " + typeof(T).ToString();



                        //Debug.Log("[Singleton] An instance of " + typeof(T) +
                        //    " is needed in the scene, so '" + singleton +
                        //    "' was created.");
                        //Debug.LogError("No Instance was found, returning null");
                    }
                    else
                    {
                        //Debug.Log("[Singleton] Using instance already created: " +
                        //    _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }
}

public abstract class Singleton : MonoBehaviour
{
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>

    #region  Properties
    public static bool applicationIsQuitting { get; private set; }
    #endregion

    #region  Methods
    private void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
    #endregion
}

