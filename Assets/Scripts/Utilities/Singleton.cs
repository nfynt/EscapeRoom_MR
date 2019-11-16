using UnityEngine;

namespace Nfynt
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<T>();
                if (Instance == null)
                {
                    Instance = new GameObject(typeof(T).ToString()+"_singleton").AddComponent<T>();
                }
            }
            else
            {
                Destroy(this);
            }

        }
    }
}




/*
 __  _ _____   ____  _ _____  
|  \| | __\ `v' /  \| |_   _| 
| | ' | _| `. .'| | ' | | |   
|_|\__|_|   !_! |_|\__| |_|
 
*/
