using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObj : MonoBehaviorObj
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
        isGen = true;
    }
    // Set spell objects datas
    public void setData(SpellData dts, Vector3 mos)
    {
        player = GameManager.player;
        data = dts;
        spawnTime = Time.time;
        currentTime = spawnTime;
        spawnPos = player.GetComponent<Player>().staffPos.position;
        transform.position = spawnPos;
        spawnPos = transform.position;
        // if Spells field tyles set position eliipse inside
        if(data.type == 2)
        {
            pos = GetEllipseIntersectionPoint(mos);
            transform.position = pos;
        }
        this.mos = mos;
    }

    // damage set
    public virtual void setDMG()
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
                Debuffs(other.gameObject);
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

    public virtual void Debuffs(GameObject target)
    {

    }

    // calculate Time
    public virtual void Update()
    {
        deltaTime = Time.time - currentTime;
        currentTime = Time.time;
    }

    public virtual void OnDestroy()
    {

    }

    public Vector3 GetEllipseIntersectionPoint(Vector3 point)
    {
        // 타원 중심과 주어진 점 사이의 벡터를 계산합니다.
        Vector3 direction = point - spawnPos;

        // 주어진 점이 타원 안에 있는지 확인합니다.
        if (IsPointInsideEllipse(direction))
        {
            // 점이 타원 안에 있는 경우 해당 점을 반환합니다.
            return point;
        }
        else
        {
            // 점이 타원 밖에 있는 경우, 점과 타원의 경계에 지점하는 점을 찾습니다.

            // 타원의 반지름을 계산합니다.
            float radiusX = data.xRange / 2f;
            float radiusY = data.yRange / 2f;

            // 타원의 중심을 기준으로 점과 직선을 형성하는 벡터의 방향을 계산합니다.
            Vector3 normalizedDirection = direction.normalized;

            // 직선의 방정식에서 y 값이 0일 때, x 값을 계산합니다.
            float x = Mathf.Sqrt(radiusX * radiusX * radiusY * radiusY / (radiusY * radiusY + radiusX * radiusX * normalizedDirection.y * normalizedDirection.y));

            // x 값을 사용하여 y 값을 계산합니다.
            float y = -Mathf.Sqrt(radiusY * radiusY * (1 - x * x / (radiusX * radiusX)));

            // 타원 경계와 교차하는 지점을 계산합니다.
            Vector3 intersectionPoint = spawnPos + normalizedDirection * x + Vector3.up * y;

            return intersectionPoint;
        }
    }

    private bool IsPointInsideEllipse(Vector3 direction)
    {
        // 타원의 반지름을 계산합니다.
        float radiusX = data.xRange / 2f;
        float radiusY = data.yRange / 2f;

        // 타원의 방정식을 사용하여 주어진 점이 타원 안에 있는지 확인합니다.
        float result = (direction.x * direction.x) / (radiusX * radiusX) + (direction.z * direction.z) / (radiusY * radiusY);

        return result <= 1;
    }
}
