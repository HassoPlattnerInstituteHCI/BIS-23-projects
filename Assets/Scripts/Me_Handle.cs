//default
//using UnityEngine;
//using DualPantoFramework;

//public class Me_Handle : MonoBehaviour
//{
//    bool free = true;
//    PantoHandle upperHandle;
//    void Start()
//    {
//        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
//    }

//    void FixedUpdate()
//    {
//        transform.position = (upperHandle.HandlePosition(transform.position));
//        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (free)
//            {
//                upperHandle.Freeze();
//            }
//            else
//            {
//                upperHandle.Free();
//            }
//            free = !free;
//        }
//    }
//}

////sphere
//using UnityEngine;
//using DualPantoFramework;

//public class Me_Handle : MonoBehaviour
//{
//    bool free = true;
//    PantoHandle upperHandle;
//    public GameObject linePrefab; // Öffentliches Feld für das Line-Prefab

//    void Start()
//    {
//        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
//    }

//    void FixedUpdate()
//    {
//        transform.position = upperHandle.HandlePosition(transform.position);
//        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);

//        if (linePrefab != null)
//        {
//            CreateLine();
//        }
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (free)
//            {
//                upperHandle.Freeze();
//            }
//            else
//            {
//                upperHandle.Free();
//            }
//            free = !free;
//        }
//    }

//    void CreateLine()
//    {
//        GameObject line = Instantiate(linePrefab, transform.position, Quaternion.identity);
//        // Hier kannst du weitere Anpassungen am Line-Objekt vornehmen, z. B. Skalierung, Rotation oder Material.
//    }
//}


//new
using UnityEngine;
using DualPantoFramework;
using System.Collections.Generic;

public class Me_Handle : MonoBehaviour
{
    bool free = true;
    PantoHandle upperHandle;
    public GameObject linePrefab; // Öffentliches Feld für das Line-Prefab
    public Transform spawnedObjectsParent; // Elternobjekt für die gespawnten Objekte
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Liste zum Speichern der gespawnten Objekte

    void Start()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
    }

    void FixedUpdate()
    {
        transform.position = upperHandle.HandlePosition(transform.position);
        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);

        if (linePrefab != null)
        {
            CreateLine();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (free)
            {
                upperHandle.Freeze();
            }
            else
            {
                upperHandle.Free();
            }
            free = !free;
        }
    }

    void CreateLine()
    {
        GameObject line = Instantiate(linePrefab, transform.position, Quaternion.identity);
        line.transform.SetParent(spawnedObjectsParent); // Setze das Elternobjekt für die gespawnten Objekte
        spawnedObjects.Add(line);
    }
}



//line
//using UnityEngine;
//using DualPantoFramework;

//public class Me_Handle : MonoBehaviour
//{
//    bool free = true;
//    PantoHandle upperHandle;
//    public GameObject linePrefab; // Das Line-Prefab für die erzeugte Linie
//    private LineRenderer lineRenderer; // Der LineRenderer der erzeugten Linie
//    private Transform lineContainer; // Das leere GameObject, das als Container für die Linien dient

//    void Start()
//    {
//        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();

//        lineContainer = new GameObject("LineContainer").transform;
//        lineRenderer = Instantiate(linePrefab, lineContainer).GetComponent<LineRenderer>();
//    }

//    void FixedUpdate()
//    {
//        transform.position = (upperHandle.HandlePosition(transform.position));
//        transform.eulerAngles = new Vector3(0, upperHandle.GetRotation(), 0);

//        UpdateLineRenderer();
//    }

//    void UpdateLineRenderer()
//    {
//        Vector3[] linePositions = new Vector3[2];
//        linePositions[0] = transform.position;
//        linePositions[1] = lineContainer.position;

//        lineRenderer.positionCount = 2;
//        lineRenderer.SetPositions(linePositions);
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (free)
//            {
//                upperHandle.Freeze();
//            }
//            else
//            {
//                upperHandle.Free();
//            }
//            free = !free;
//        }
//    }
//}