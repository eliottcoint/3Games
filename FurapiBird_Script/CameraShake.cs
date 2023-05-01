using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public GameObject cameraObj; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShakeCam(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = cameraObj.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraObj.transform.position = new Vector3(x, y, cameraObj.transform.position.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        cameraObj.transform.position = orignalPosition;
    }
}
