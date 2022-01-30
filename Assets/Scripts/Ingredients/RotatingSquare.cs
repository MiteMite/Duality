using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotatingSquare : MonoBehaviour
{
    public float speed;
    public float rotationStopDuration;
    private float m_Timer;
    private bool m_IsRotating = true;
    private float m_RotationAngle = 0;
    public GameObject hamster;

    void FixedUpdate()
    {

        if (m_IsRotating)
        {
            transform.Rotate(new Vector3(0, 0, speed * 90 * Time.deltaTime));
            hamster.transform.Rotate(new Vector3(0, 0, -speed * 90 * Time.deltaTime));
            m_RotationAngle += Mathf.Abs(speed * 90 * Time.deltaTime);

            if(m_RotationAngle >= 90)
            {
                m_IsRotating = false;
                m_Timer = 0f;
                m_RotationAngle = 0;
            }
            //rotate collider with respect to physics engine
        }
        else
        {
            m_Timer += Time.deltaTime;
            if (m_Timer >= rotationStopDuration)
            {
                m_IsRotating = true;
            }
        }


    }
}
