using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneMove : MonoBehaviour
{

    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
    public Vector3 playerChange;
    public bool needText;
    public string zoneName;
    public GameObject text;
    public Text zoneText;

    private CameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            cam.maxPosition = cameraChangeMax;
            cam.minPosition = cameraChangeMin;
            other.transform.position += playerChange;

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
