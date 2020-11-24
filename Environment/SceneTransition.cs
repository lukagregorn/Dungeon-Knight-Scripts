using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;
    public Vector2 teleportTo;
    public VectorValue playerPositionStorage;
    public GameObject sceneFadeOut;
    public GameObject sceneFadeIn;
    public float fadeWait;

    private void Awake() {
        if (sceneFadeIn) {
            GameObject fadeIn = Instantiate(sceneFadeIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeIn, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger) {
            playerPositionStorage.value = teleportTo;
            StartCoroutine(FadeCoroutine());
        }
    }


    public IEnumerator FadeCoroutine() {
        if (sceneFadeOut) {
            Instantiate(sceneFadeOut, Vector3.zero, Quaternion.identity);
        }

        yield return new WaitForSeconds(fadeWait);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!async.isDone) {
            yield return null;
        }
    }

}
