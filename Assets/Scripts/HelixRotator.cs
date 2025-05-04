using UnityEngine;

public class HelixRotator : MonoBehaviour
{
    public float rotationSpeed = 200f;
    private float mouseX;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * -mouseX * rotationSpeed * Time.deltaTime);
        }
    }
}
