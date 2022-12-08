using UnityEngine;

public class SingletonObject<T> where T : SingletonObject<T>, new() {
	private static T _instance;

	private static object _lock = new object ();

	public static T Instance {
		get {
			lock (_lock) {
				if (_instance == null) {
					_instance = new T ();
					_instance.OnCreated();
				}

				return _instance;
			}
		}
	}

	public virtual void OnCreated () {
		
	}
}