using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static TowerController instance;

    public int towerPlacementIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
