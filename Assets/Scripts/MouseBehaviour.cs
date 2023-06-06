using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour
{
    public float sensitivity = 10.0f;

    private GameObject player;
    private float xRot = 0.0f;
    private float yRot = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            float x = Input.GetAxis("Mouse X") * this.sensitivity;
            float y = Input.GetAxis("Mouse Y") * -this.sensitivity;

            this.player.transform.Rotate(0,x,0);

            this.yRot += x;

            // Kondisional digunakan agar player tidak dapat merotasikan kamera 360 derajat secara vertikal
            this.xRot = (this.xRot+y > 90) ? 90 : (this.xRot+y < -90) ? -90 : this.xRot + y;

            this.transform.eulerAngles = new Vector3(this.xRot, this.yRot, 0.0f);
        }
    }
}
