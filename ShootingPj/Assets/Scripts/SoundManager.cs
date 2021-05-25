using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject player;
    public AudioSource bgm;

    public static SoundManager sm;
    // Start is called before the first frame update

    private void Awake()
    {
        if(sm == null)
        {
            sm = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGameoverSound()
    {
        bgm.Stop();
        GetComponent<AudioSource>().Play();
           
    }
}