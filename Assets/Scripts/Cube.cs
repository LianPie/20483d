using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    static int StaticId = 0;
    [SerializeField] private TMP_Text[] numbersText;
    [HideInInspector] public int CubeID;
    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRb;
    [HideInInspector] public bool IsMainCube;
    private MeshRenderer CubeMeshRenderer;
    private void Awake()
    {
        CubeID = StaticId++;
        CubeRb = GetComponent<Rigidbody>();
        CubeMeshRenderer = GetComponent<MeshRenderer>();
    }
    public void setColor(Color color)
    {
        CubeColor = color;
        CubeMeshRenderer.material.color = color;
    }
    public void setNumber(int num)
    {
        CubeNumber = num;
        for(int i = 0; i < 6; i++)
        {
            numbersText[i].text = num.ToString();
        }
    }
}
