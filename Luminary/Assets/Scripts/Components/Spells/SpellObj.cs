using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObj : MonoBehaviour
{
    // Start is called before the first frame update

    public SpellData data;
    public float spawnTime, currentTime, deltaTime;
    public Vector3 spawnPos = new Vector3(0,0,0);

    public GameObject player;
    public GameObject target;

    public Vector3 mos;
    public Vector3 pos;

    public int dmg;

    public bool isGen = false;

    public virtual void Start()
    {
        player = GameManager.player;
        Debug.Log("Gen");
        isGen = true;
    }
    // Set spell objects datas
    public void setData(SpellData dts, Vector3 mos)
    {
        player = GameManager.player;
        data = dts;
        spawnTime = Time.time;
        currentTime = spawnTime;
        spawnPos = player.transform.position;
        transform.position = spawnPos;
        // if Spells field tyles set position eliipse inside
        if(data.type == 2)
        {
            pos = GetEllipseIntersectionPoint(mos);
            transform.position = pos;
        }
        this.mos = mos;
    }

    // damage set
    public void setDMG()
    {
        dmg = (data.damage * player.GetComponent<Player>().status.finalDMG);
    }
    // trigger with mob, wall
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isGen)
        {
            if (other.tag == "Mob")
            {
                setDMG();
                other.GetComponent<Mob>().HPDecrease(dmg);
                GameManager.Resource.Destroy(this.gameObject);
            }
            if (other.CompareTag("Wall"))
            {
                Debug.Log(other.name);
                Debug.Log(other.transform.parent.transform.parent.name);
                Debug.Log(other.transform.parent.transform.parent.transform.parent.transform.parent);
                GameManager.Resource.Destroy(this.gameObject);

            }
        }
    }

    // calculate Time
    public virtual void Update()
    {
        deltaTime = Time.time - currentTime;
        currentTime = Time.time;
    }

    public virtual void OnDestroy()
    {
        Debug.Log("Destory");
    }

    public Vector3 GetEllipseIntersectionPoint(Vector3 point)
    {
        // Ÿ�� �߽ɰ� �־��� �� ������ ���͸� ����մϴ�.
        Vector3 direction = point - spawnPos;

        // �־��� ���� Ÿ�� �ȿ� �ִ��� Ȯ���մϴ�.
        if (IsPointInsideEllipse(direction))
        {
            // ���� Ÿ�� �ȿ� �ִ� ��� �ش� ���� ��ȯ�մϴ�.
            return point;
        }
        else
        {
            // ���� Ÿ�� �ۿ� �ִ� ���, ���� Ÿ���� ��迡 �����ϴ� ���� ã���ϴ�.

            // Ÿ���� �������� ����մϴ�.
            float radiusX = data.xRange / 2f;
            float radiusY = data.yRange / 2f;

            // Ÿ���� �߽��� �������� ���� ������ �����ϴ� ������ ������ ����մϴ�.
            Vector3 normalizedDirection = direction.normalized;

            // ������ �����Ŀ��� y ���� 0�� ��, x ���� ����մϴ�.
            float x = Mathf.Sqrt(radiusX * radiusX * radiusY * radiusY / (radiusY * radiusY + radiusX * radiusX * normalizedDirection.y * normalizedDirection.y));

            // x ���� ����Ͽ� y ���� ����մϴ�.
            float y = -Mathf.Sqrt(radiusY * radiusY * (1 - x * x / (radiusX * radiusX)));

            // Ÿ�� ���� �����ϴ� ������ ����մϴ�.
            Vector3 intersectionPoint = spawnPos + normalizedDirection * x + Vector3.up * y;

            return intersectionPoint;
        }
    }

    private bool IsPointInsideEllipse(Vector3 direction)
    {
        // Ÿ���� �������� ����մϴ�.
        float radiusX = data.xRange / 2f;
        float radiusY = data.yRange / 2f;

        // Ÿ���� �������� ����Ͽ� �־��� ���� Ÿ�� �ȿ� �ִ��� Ȯ���մϴ�.
        float result = (direction.x * direction.x) / (radiusX * radiusX) + (direction.z * direction.z) / (radiusY * radiusY);

        return result <= 1;
    }
}
