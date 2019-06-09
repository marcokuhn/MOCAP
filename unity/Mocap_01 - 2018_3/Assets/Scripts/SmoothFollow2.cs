using UnityEngine;

namespace UnityStandardAssets.Utility
{
  public class SmoothFollow2 : MonoBehaviour
  {

    // The target we are following
    [SerializeField]
    private Transform target;

    // The target we are following
    [SerializeField]
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;

    // The distance in the x-z plane to the target
    [SerializeField]
    private float distance = 10.0f;
    // the height we want the camera to be above the target
    [SerializeField]
    private float height = 5.0f;

    [SerializeField]
    private float rotationDamping;
    [SerializeField]
    private float heightDamping;

    private float wantedRotationAngle;
    private float currentRotationAngle;
    private Quaternion currentRotation;
    private float wantedHeight;
    private float currentHeight;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void LateUpdate()
    {
      // Early out if we don't have a target
      if (!target)
        return;

      // Calculate the current rotation angles
      wantedRotationAngle = target.eulerAngles.y;
      wantedHeight = target.position.y + height;

      currentRotationAngle = transform.eulerAngles.y;
      currentHeight = transform.position.y;

      // Damp the rotation around the y-axis
      currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

      // Damp the height
      currentHeight = wantedHeight; // Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

      // Convert the angle into a rotation
      currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

      //Smooth follow
      targetPosition = target.position;
      targetPosition -= currentRotation * Vector3.forward * distance;
      targetPosition = new Vector3(targetPosition.x, currentHeight, targetPosition.z);
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


      // Always look at the target
      transform.LookAt(target);
    }
  }
}