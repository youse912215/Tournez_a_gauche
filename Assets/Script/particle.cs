using PLAYER;
using UnityEngine;
using static Call.ConstantValue;

public class particle : MonoBehaviour
{
    public static bool isAlive;
    private float aliving;
    [SerializeField] private ParticleSystem heart;

    public GameObject Particle;
    public GameObject Particle2;
    [SerializeField] private ParticleSystem star;

    // Start is called before the first frame update
    private void Start()
    {
        aliving = 0.0f;
        isAlive = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            Instantiate(Particle, Vector3.zero, Quaternion.identity);
            Instantiate(Particle2, Vector3.zero, Quaternion.identity);
        }

        //以下はテスト用
        if (!isAlive) return;

        star.transform.position = cubeRotate.currentPoints;
        heart.transform.position = cubeRotate.currentPoints;

        aliving += LATE_SPEED;

        if (aliving >= 4.0f)
        {
            aliving = 0.0f;
            isAlive = false;
            star.transform.position = new Vector3(-300, 15, -55);
            heart.transform.position = new Vector3(-300, 15, -55);
        }
    }
}