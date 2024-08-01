using UnityEngine;

public class MoveBackAndForth2D : MonoBehaviour
{
    public float pointA;
    public float pointB;
    public float speed = 1.0f;
    public Transform bridge; 

    private float target;
    private bool onBridge = false;
    private Vector3 lastBridgePosition;

    void Start()
    {
        target = pointB;
        if (bridge != null)
        {
            lastBridgePosition = bridge.position;
        }
    }

    void Update()
    {
        
        float newX = Mathf.MoveTowards(transform.position.x, target, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Mathf.Abs(transform.position.x - target) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
        }

        
        if (onBridge && bridge != null)
        {
            Vector3 bridgeDelta = bridge.position - lastBridgePosition;
            transform.position += bridgeDelta;
            lastBridgePosition = bridge.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.transform == bridge)
        {
            onBridge = true;
            lastBridgePosition = bridge.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.transform == bridge)
        {
            onBridge = false;
        }
    }
}
