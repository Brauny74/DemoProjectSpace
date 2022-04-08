using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	[Tooltip("If true, the object will persist on loads.")]
	public bool persistent;

    protected static T _instance;
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					GameObject obj = new GameObject();
					_instance = obj.AddComponent<T>();
				}
			}
			return _instance;
		}
	}

    protected virtual void Awake()
    {
		if (!Application.isPlaying)
		{
			return;
		}

		if (_instance == null)
		{
			_instance = this as T;
			if(persistent)
				DontDestroyOnLoad(transform.gameObject);
		}
		else
		{
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}
	}
}
