using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneMove : MonoBehaviour
{
 
    // Zone's min and max points
    public Vector2 bottomLeft;
    public Vector2 topRight;

    // Zone also needs a direction which indicates in which direction
    // we are going relative to the world (x=[-1, 1], y =[-1, 1])
    public Vector3 direction;

    // indicators for zone text displaying
    public bool needText;
    public string zoneName;
    public GameObject text;
    public Text zoneText;

    // camera
    private CameraMovement cam;

    // determine how we calmp the main camera
    private float cameraSizeXOffset;
    private float cameraSizeYOffset;

    // static variables of fixed values
    private static float playerMove = 2;
    private static float cameraAspect = 16f/9f;
    private static float cameraSize = 7f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        cameraSizeXOffset = cameraAspect * cameraSize;
        cameraSizeYOffset = cameraSize;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {

            Vector2 newMin = new Vector2(bottomLeft.x + cameraSizeXOffset, bottomLeft.y + cameraSizeYOffset);
            Vector2 newMax = new Vector2(topRight.x - cameraSizeXOffset, topRight.y - cameraSizeYOffset);

            cam.SetClampValues(newMin, newMax);
            other.transform.position += direction * playerMove;

            if (needText) {
                StartCoroutine(zoneNameCoroutine());
            }
        }
    }

    // coroutine for showing zone name
    private IEnumerator zoneNameCoroutine() {
        // change text to new zone text
        zoneText.text = zoneName;

        // set active and inactive
        text.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }

}
