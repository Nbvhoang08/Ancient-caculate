using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 45f; // Tốc độ xoay tính bằng độ mỗi giây
    void Update()
    {
        // Xoay đối tượng quanh trục z theo thời gian
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
