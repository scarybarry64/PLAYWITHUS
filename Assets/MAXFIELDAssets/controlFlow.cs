using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class controlFlow : MonoBehaviour
{
    [SerializeField]
    [Range(5, 25)]
    public int gridSectionsPerRow;

    public GameObject levelPrefab;

    private float relativePos = 0.0f;

    private GameObject level1;
    private GameObject level2;

    // Start is called before the first frame update
    void Start()
    {
      // Component[] levelLayout;
      // levelLayout = levelPrefab.GetComponentsInChildren<Transform>();

      // Transform baseTransform = levelLayout[0].GetComponent<Transform>();
      // var newPos = baseTransform.localPosition;
      // newPos.z += 140;

      level1 = Instantiate(levelPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.GetComponent<Transform>());

      level1.GetComponent<populateMe>().populateLevel(gridSectionsPerRow, relativePos);

      relativePos += 140.0f;

      level2 = Instantiate(levelPrefab, new Vector3(0, 0, 140), Quaternion.identity, gameObject.GetComponent<Transform>());

      level2.GetComponent<populateMe>().populateLevel(gridSectionsPerRow + 5, relativePos);

      level2 = Instantiate(levelPrefab, new Vector3(0, 0, 280), Quaternion.identity, gameObject.GetComponent<Transform>());

      relativePos += 140.0f;

      level2.GetComponent<populateMe>().populateLevel(gridSectionsPerRow + 10, relativePos);

      level2 = Instantiate(levelPrefab, new Vector3(0, 0, 420), Quaternion.identity, gameObject.GetComponent<Transform>());

      relativePos += 140.0f;

      level2.GetComponent<populateMe>().populateLevel(gridSectionsPerRow + 15, relativePos);
      // List<GameObject> currentObjs = level1.GetComponent<populateMe>().returnSceneObjs();

      // Transform levelLayout2 = nextLevel.GetComponent<Transform>();
      // levelLayout2.localPosition = newPos;

      // nextLevel.GetComponent<populateMe>().populateLevel(gridSectionsPerRow + 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
