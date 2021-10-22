using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyParticleAnim : MonoBehaviour
{
    [HideInInspector] public Transform goToPosition;
    [HideInInspector]public Rigidbody2D rb;
    float timeStamp;
    bool fly;

    // Start is called before the first frame update
    void Start()
    {
        timeStamp = Time.time;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, goToPosition.position) <= 0.5f)
            Destroy(gameObject);
        
    }
    private void FixedUpdate()
    {
        if(fly)
            flyToObject();
    }
    IEnumerator Fly()
    {
        yield return new WaitForSeconds(1f);
        fly = true;
    }
    void flyToObject()
    {
        Vector2 objectDirection = (goToPosition.position - transform.position);
        
        rb.velocity = objectDirection * (Time.time / timeStamp) * 7f;
    }
}
