using UnityEngine;

public class StartingSmokePartical : MonoBehaviour
{
    public void DisconnectObject() 
    {
        transform.SetParent(null);
    }
}