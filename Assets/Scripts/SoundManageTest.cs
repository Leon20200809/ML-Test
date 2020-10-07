using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManageTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM_Type.Game1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
