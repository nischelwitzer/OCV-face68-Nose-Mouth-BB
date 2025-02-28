using UnityEngine;

public class DoNotDestroyMe : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
