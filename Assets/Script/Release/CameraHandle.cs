using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{

    Vector3 inputFinal;
    Vector3 inputTemp;

    // Update is called once per frame
    void Update()
    {
        inputFinal = ReadInput();
        inputTemp = Vector3.Lerp(inputTemp, inputFinal, lerp);
        transform.Translate(inputTemp * Time.deltaTime);
    }
    public float speed = 20;
    public float boost = 1.5f;
    public float lerp = 0.01f;
    float m_horInput;
    float m_verInput;

    float m_yaw;
    public float m_yawSens = 3;
    float m_pitch;
    public float m_pitchSens = 3;

    Quaternion rotationForCamera;

    public void ReadMouse()
    {
        m_pitch += m_pitchSens * Input.GetAxis("Mouse X");
        m_yaw += m_yawSens * Input.GetAxis("Mouse Y");
        m_yaw = Mathf.Clamp(m_yaw, -89, 89);
        rotationForCamera = Quaternion.Euler(m_yaw, m_pitch, 0);
        transform.GetChild(0).transform.rotation = rotationForCamera;
    }
    public Vector3 ReadInput()
    {
        ReadMouse();
        m_horInput = Input.GetAxisRaw("Horizontal");
        m_verInput = Input.GetAxisRaw("Vertical");
        Vector3 input = rotationForCamera * new Vector3(m_horInput, 0, m_verInput).normalized;
        if (Input.GetKey(KeyCode.LeftShift)) return speed * boost * input;
        return speed * input;
    }
}
