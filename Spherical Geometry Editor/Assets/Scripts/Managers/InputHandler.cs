using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler instance = null;
    public static InputHandler Instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("multiple input handlers, deleting self");
            Destroy(this.gameObject);
        }
    }
}
