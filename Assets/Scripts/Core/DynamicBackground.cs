using UnityEngine;

namespace Toon.Core
{
    public class DynamicBackground : MonoBehaviour
    {
        [SerializeField] GameObject[] backgrounds;
        private Camera mainCamera;
        private Vector2 screenBounds;
        private Vector3 lastScreenPosition;

        void Awake()
        {
            mainCamera = gameObject.GetComponent<Camera>();
            screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width,
                                                                     Screen.height,
                                                                     mainCamera.transform.position.z));
            foreach (GameObject obj in backgrounds)
            {
                LoadChildObjects(obj);
            }

            lastScreenPosition = transform.position;
        }

        void LateUpdate()
        {
            foreach (GameObject obj in backgrounds)
            {
                RepositionChildObjects(obj);

                // Parallax effect
                // if the object has higher z value, moves with a higher speed and gets closer to the camera speed 
                float parallaxSpeed = 1 - Mathf.Clamp01(Mathf.Abs(transform.position.z /
                                                                  obj.transform.position.z));
                float difference = transform.position.x - lastScreenPosition.x;
                obj.transform.Translate(Vector3.right * difference * parallaxSpeed);
            }

            lastScreenPosition = transform.position;
        }

        private void LoadChildObjects(GameObject obj) // call this from Awake() method
        {
            float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;

            // calculate how many child objects we need
            // they should cover the double size of the screen width
            int objectsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth);

            GameObject clone = Instantiate(obj) as GameObject;

            for (int i = 0; i <= objectsNeeded; i++) // duplicate the clone object at the start
            {
                GameObject duplicatedClone = Instantiate(clone) as GameObject;
                duplicatedClone.transform.SetParent(obj.transform); // make them the child of original object
                duplicatedClone.transform.position = new Vector3(objectWidth * i,
                                                                 obj.transform.position.y,
                                                                 obj.transform.position.z);
                duplicatedClone.name = obj.name + i; // change the clone name to keep things tidy
            }

            Destroy(clone); // we can remove the first clone object, because we already duplicated it
            Destroy(obj.GetComponent<SpriteRenderer>()); // origial oject doesn't need sprite renderer
                                                         // because clone duplications already have it
        }

        private void RepositionChildObjects(GameObject obj) // call this from LateUpdate() method
        {
            Transform[] childObjects = obj.GetComponentsInChildren<Transform>();
            if (childObjects.Length > 1)
            {
                GameObject firstChild = childObjects[1].gameObject;
                GameObject lastChild = childObjects[childObjects.Length - 1].gameObject;
                float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x;

                // replace the child objects, if camera moves to +x direction
                if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
                {
                    firstChild.transform.SetAsLastSibling();
                    firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2,
                                                                lastChild.transform.position.y,
                                                                lastChild.transform.position.z);
                }

                // replace the child objects, if camera moves to -x direction
                else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
                {
                    lastChild.transform.SetAsFirstSibling();
                    lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2,
                                                               firstChild.transform.position.y,
                                                               firstChild.transform.position.z);
                }
            }
        }
    }
}