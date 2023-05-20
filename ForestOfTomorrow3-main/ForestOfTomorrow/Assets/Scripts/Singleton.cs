using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Singleton
    private static T m_instance;
    [SerializeField]
    private bool m_IsPersistent = true;
    public static T Instance
    {
        get
        {
            if (!m_instance)
            {
                var instances = FindObjectsOfType<T>();
                if (instances.Length > 0)
                {
                    if (instances.Length > 1)
                    {
                        for (int i = 1; i < instances.Length; i++)
                        {
                            Destroy(instances[i].gameObject);
                        }
                    }
                    m_instance = instances[0];
                }
                else
                {
                    m_instance = new GameObject($"{nameof(Singleton<T>)}{typeof(T)}").AddComponent<T>();
                }
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        var instances = FindObjectsOfType<T>();
        if (instances.Length > 1)
        {
            for (int i = 1; i < instances.Length; i++)
            {
                Debug.Log("<color=red>Already another " + this.name + " object, will destroy this </color>" + instances[i].GetInstanceID());
                Destroy(instances[i].gameObject);
            }
        }
        if (m_IsPersistent)
        {
            DontDestroyOnLoad(gameObject);
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }
    #endregion
}
