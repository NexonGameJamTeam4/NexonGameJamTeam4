using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<SpawnData> _spawnDatas = new List<SpawnData>();

    //string _xmlFileName = "MobData";

    private void Start()
    {
        //InvokeRepeating("Spawn", 0, 1f);
    }

    private void Update()
    {
        
    }

    void Spawn()
    {
        GameObject bullet = GameManager.instance.poolManager.Get(0);
        bullet.transform.position = Vector3.zero;
    }

    /*private void Start()
    {
        LoadXML(_xmlFileName);
    }

    private void LoadXML(string _fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load(_fileName);
        if (txtAsset == null)
        {
            Debug.LogError("Failed to load XML file: " + _fileName);
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);

        // 전체 아이템 가져오기 예제.
        XmlNodeList all_nodes = xmlDoc.SelectNodes("root/Sheet1");
        foreach (XmlNode node in all_nodes)
        {
            SpawnData newData = new SpawnData();

            newData.spriteType = int.Parse(node.SelectSingleNode("spriteType").InnerText);
            newData.health = float.Parse(node.SelectSingleNode("health").InnerText);
            newData.damage = float.Parse(node.SelectSingleNode("damage").InnerText);
            newData.atkSpeed = float.Parse(node.SelectSingleNode("atkSpeed").InnerText);
            newData.speed = float.Parse(node.SelectSingleNode("speed").InnerText);

            _spawnDatas.Add(newData);
        }
    }*/

    [System.Serializable]
    public class SpawnData //몬스터 능력치 데이터
    {
        public int spriteType;
        public float health;
        public float damage;
        public float atkSpeed;
        public float speed;
    }
}
