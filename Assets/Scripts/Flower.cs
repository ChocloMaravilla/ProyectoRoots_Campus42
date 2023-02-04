using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    float lerp;
    public bool inverse;
    public int x;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        lerp+=Time.deltaTime;
        if (!inverse)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one,lerp);
        }else if (inverse)
        {
            transform.localScale = Vector3.Lerp(Vector3.one,Vector3.zero, lerp);
        }
        if (lerp<-0.5f)
        {
            Destroy(gameObject);
        }
        lerp = Mathf.Clamp(lerp,-0.6f,1);
    }
    public void Dissapear()
    {
        inverse = true;
    }
}
