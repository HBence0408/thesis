using UnityEngine;

public class CamaraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  this.transform.Rotate(0, 0, 1);

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Rotate(0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(-0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(0, -0.5f, 0);
        }
    }
}
