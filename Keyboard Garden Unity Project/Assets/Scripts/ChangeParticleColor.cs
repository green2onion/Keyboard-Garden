using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleColor : MonoBehaviour
{
    void Start()
    {
        ParticleSystem.MainModule psMain = GetComponent<ParticleSystem>().main;
        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.green);
    }
}
