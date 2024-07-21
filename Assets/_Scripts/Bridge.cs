using UnityEngine;

public class MoveBackAndForth2D : MonoBehaviour
{
    public float pointA;
    public float pointB;
    public float speed = 1.0f;

    private float target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        
        float newX = Mathf.MoveTowards(transform.position.x, target, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        
        if (Mathf.Abs(transform.position.x - target) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
