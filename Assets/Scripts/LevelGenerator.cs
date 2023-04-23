
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] levelPart;
    [SerializeField] private Vector3 nextPartPosition;

    [SerializeField] private float distanceToSpawn; // platformlar arası yaklaşmayı anlamak için distance değişkenini tanımladık
    [SerializeField] private float distanceToDelete;
    [SerializeField] private Transform player;



    void Update()
    {
        DeletePlatform();
        GeneratePlatform();
    }

    private void GeneratePlatform()
    {
        while (Vector2.Distance(player.transform.position, nextPartPosition) < distanceToSpawn)
        {
            Transform part = levelPart[Random.Range(0, levelPart.Length)];


            Vector2 newPosition = new Vector2(nextPartPosition.x - part.Find("StartPoint").position.x, 0); // startpointi tam yerine koymak için çıkardık, child olduğu için find ile yaptık

            Transform newPart = Instantiate(part, newPosition, transform.rotation, transform); // sondaki transformun anlamı direkt olarak oluşacak clonları child yapacak

            nextPartPosition = newPart.Find("EndPoint").position; // find ile transformun child objesine ulaştık

        }
    }

    private void DeletePlatform()
    {
        if (transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(player.transform.position, partToDelete.transform.position) > distanceToDelete)
                Destroy(partToDelete.gameObject);

        }
    }
}
