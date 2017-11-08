using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour {

    public Rigidbody2D rb;  
    public GameObject hook;
    public float releaseTime = .15f;
    public float maxTension;
    private bool isPressed = false;
    private bool cameraTracking = false;
    private Vector3 offset;
    private void Start()
    {
        offset = Camera.main.transform.position - rb.transform.position;
    }

    private void LateUpdate()
    {
        if (cameraTracking && Camera.main.transform.position.y > -2)
        {
            Camera.main.transform.position = rb.transform.position + offset;
            rb.freezeRotation = false;
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            MoveBall();
        }
    }

    private void MoveBall()
    {
        float distance = Vector2.Distance(hook.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (distance < maxTension)
        {
            rb.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0); ;
        }
        else
        {
            rb.Sleep();
        }

    }

    private void OnMouseDown()
    {
        Cursor.visible = false;
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
        cameraTracking = true;
        rb.WakeUp();
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
    }
}
