using UnityEngine;

namespace TbsFramework.Gui
{
    /// <summary>
    /// Simple movable camera implementation.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform MainCameraTransform;
        public float ScrollSpeed = 15;
        public float ScrollEdge = 0.01f;

        void Update()
        {
            //if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
            //{
            //    transform.Translate(transform.right * Time.deltaTime * ScrollSpeed, Space.World);
            //}
            //else if (Input.GetKey("a") || Input.mousePosition.x <= Screen.width * ScrollEdge)
            //{
            //    transform.Translate(transform.right * Time.deltaTime * -ScrollSpeed, Space.World);
            //}
            //if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge))
            //{
            //    transform.Translate(transform.up * Time.deltaTime * ScrollSpeed, Space.World);
            //}
            //else if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height * ScrollEdge)
            //{
            //    transform.Translate(transform.up * Time.deltaTime * -ScrollSpeed, Space.World);
            //}

            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width * (1 - ScrollEdge))
            {
                transform.localPosition = MainCameraTransform.localPosition + new Vector3(Time.deltaTime * ScrollSpeed, 0, 0);
            }
            else if (Input.GetKey("a") || Input.mousePosition.x <= Screen.width * ScrollEdge)
            {
                transform.localPosition = MainCameraTransform.localPosition + new Vector3(-Time.deltaTime * ScrollSpeed, 0, 0);
            }
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * (1 - ScrollEdge))
            {
                transform.localPosition = MainCameraTransform.localPosition + new Vector3(0, 0, Time.deltaTime * ScrollSpeed);
            }
            else if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height * ScrollEdge)
            {
                transform.localPosition = MainCameraTransform.localPosition + new Vector3(0, 0, -Time.deltaTime * ScrollSpeed);
            }
            else if (Input.mouseScrollDelta.y != 0)
            {
                transform.localPosition = MainCameraTransform.localPosition + new Vector3(0, Time.deltaTime * ScrollSpeed * Input.mouseScrollDelta.y, 0);
            }
        }
    }
}

