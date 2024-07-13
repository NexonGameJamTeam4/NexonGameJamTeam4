using UnityEngine;

public class ParallexBackground_01 : MonoBehaviour
{
    public enum BackgroundType
    {
        back,
        middle,
        front,
    }

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollAmount;
    [SerializeField]
    private Vector3 moveDir;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private BackgroundType backgroundType;

    private float moveSpeed;

    private void FixedUpdate()
    {
        float playerVelocityX = player.GetComponent<Rigidbody2D>().velocity.x;
        switch (backgroundType)
        {
            case BackgroundType.back:
                moveSpeed = playerVelocityX / 4;
                break;
            case BackgroundType.middle:
                moveSpeed = playerVelocityX / 2;
                break;
            case BackgroundType.front:
                moveSpeed = playerVelocityX;
                break;
        }
        Vector3 moveVec = moveSpeed * Time.deltaTime * moveDir;
        transform.position += moveVec;

        if (transform.position.x - player.gameObject.transform.position.x > 25)
        {
            transform.position = new Vector3(target.position.x - scrollAmount, target.position.y, target.position.z);
        }
        else if (transform.position.x - player.gameObject.transform.position.x < -25)
        {
            transform.position = new Vector3(target.position.x + scrollAmount, target.position.y, target.position.z);
        }
    }
}
