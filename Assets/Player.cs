using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(input * speed * Time.deltaTime * Vector3.right);
    }
}
