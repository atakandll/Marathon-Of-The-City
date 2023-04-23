using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform[] movePoints;
    private int i;
    protected override void Start()
    {
        base.Start();
        transform.position = movePoints[0].position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoints[i].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[i].position) < .25f)
        {
            i++; // diğer pozisyonlara da gitmesini sağlıyoruz.

            if (i >= movePoints.Length)
            {
                i = 0;
            }
        }
        if (transform.position.x > movePoints[i].position.x) // we are from the left of the moving point menas we move into the right.
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));

        }

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other); // miras aldığımız sınıfın metoduna ulaştık

    }

}
