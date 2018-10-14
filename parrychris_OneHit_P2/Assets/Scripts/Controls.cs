using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "PlayerControls")]
public class Controls : ScriptableObject
{

    [Header("Controls")]
    public KeyCode left;
	public KeyCode right, jump, dash, block, jab, groundPound;

}


