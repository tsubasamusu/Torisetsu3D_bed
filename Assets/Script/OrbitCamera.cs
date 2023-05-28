using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float zoomSpeed = 1.0f;
    public float minDistance = 1.0f;
    public float maxDistance = 20.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    private float x = 0.0f;
    private float y = 0.0f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    private void Update()
    {
         float scroll = Input.GetAxis("Mouse ScrollWheel");
    distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

    if (Input.touchSupported && Input.touchCount == 2)
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
        {
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;
            Vector2 prevTouch2 = touch2.position - touch2.deltaPosition;

            float prevTouchDeltaMag = (prevTouch1 - prevTouch2).magnitude;
            float touchDeltaMag = (touch1.position - touch2.position).magnitude;

            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag; //ここを修正しました

            distance = Mathf.Clamp(distance + deltaMagnitudeDiff * zoomSpeed * 0.01f, minDistance, maxDistance);
        }
    }
    }

    private void LateUpdate()
    {
         if (UIInteractionManager.Instance.IsScrollbarInteracting)
        {
            return; // Skip camera movement while scrollbar interaction is ongoing
        }
         if (target)
        {
            if (Input.GetMouseButton(0) || (Input.touchSupported && Input.touchCount == 1))
            {
                float moveX = Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                float moveY = Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                if (Input.touchSupported)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        moveX = -touch.deltaPosition.x * xSpeed * 0.005f;
                        moveY = -touch.deltaPosition.y * ySpeed * 0.005f;
                    }
                    else
                    {
                        moveX = 0;
                        moveY = 0;
                    }
                }

                x += moveX;
                y -= moveY;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
