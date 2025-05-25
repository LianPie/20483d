using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    //[SerializeField] private float cubeMinPosX;

    [Space]
    [SerializeField] private TouchSlider touchSlider;
    private Cube Maincube;

    private bool isPointerDown;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCube();

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerUpEvent += OnPointerUp;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
    }
    private void Update()
    {
        if (isPointerDown)
        {
            Maincube.transform.position = Vector3.Lerp(
                Maincube.transform.position,
                cubePos,
                moveSpeed * Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerUp()
    {
        isPointerDown = false;
        Maincube.CubeRb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
        Invoke("SpawnNewCube", 0.5f);
    }
    private void SpawnNewCube()
    {
        Maincube.IsMainCube = false;
        SpawnCube();
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = Maincube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void SpawnCube()
    {
        Maincube = CubeSpawner.Instance.SpawnRandom();
        Maincube.IsMainCube = true;
        cubePos = Maincube.transform.position;
    }
    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
    }
}
