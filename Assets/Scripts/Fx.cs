using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fx : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFx;

    ParticleSystem.MainModule cubeExplosionFxMainMadule;
    public static Fx Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        cubeExplosionFxMainMadule = cubeExplosionFx.main;
    }
    public void playCubeExplosionFx(Vector3 position, Color color)
    {
        cubeExplosionFxMainMadule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFx.transform.position = position;
        cubeExplosionFx.Play();
    }
}
