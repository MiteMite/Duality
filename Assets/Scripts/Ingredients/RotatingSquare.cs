using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSquare : MonoBehaviour
{
    public float speed;
    public float rotationStopDuration;
    private float m_Timer;
    private bool m_IsRotating = true;
    private float m_RotationAngle;

    void FixedUpdate()
    {
        m_Timer += Time.deltaTime;

        if (m_IsRotating)
        {
            transform.Rotate(new Vector3(0, 0, speed * 90 * Time.deltaTime));

            m_RotationAngle = speed * 90 * Time.deltaTime;

            if(m_RotationAngle >= 90)
            {
                m_IsRotating = false;
            }

            //Debug.Log((int)(transform.localEulerAngles.z));

            switch ((int)(transform.localEulerAngles.z))
            {

                case 90:
                case 180:
                case 270:
                case 0:
                    m_IsRotating = false;
                    m_Timer = 0f;
                    break;
            }

        }

        if (m_Timer >= rotationStopDuration)
        {
            m_IsRotating = true;
        }


    }
}
