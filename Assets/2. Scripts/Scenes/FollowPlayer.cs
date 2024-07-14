using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.localPosition - player.localPosition;
    }

    void Update()
    {
        if (player == null)
            return;
        
        transform.localPosition = new Vector3(player.localPosition.x, player.localPosition.y, transform.localPosition.z) + offset;
    }
}
