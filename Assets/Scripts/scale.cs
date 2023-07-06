using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale : MonoBehaviour
{
    public float scaleFactor = 1.02f; // Der Faktor, um den das Objekt vergrößert oder verkleinert wird
    public float maxScaleFactor = 1.5f; // Der maximale Faktor, um den das Objekt skaliert wird (50% größer als die Originalgröße)

    private Vector3 originalScale; // Die ursprüngliche Skalierung des Objekts
    private bool isScalingUp = true; // Gibt an, ob das Objekt vergrößert oder verkleinert wird

    private GameObject character; // Referenz auf das Charakter-Game Object

    void Start()
    {
        originalScale = transform.localScale;
        character = GameObject.FindGameObjectWithTag("Character"); // Tag des Charakter-Game Objects anpassen
    }

    void Update()
    {
        if (Time.frameCount % 10 == 0) // Überprüfe, ob alle 10 Frames erreicht wurden
        {
            if (isScalingUp)
            {
                // Vergrößere das Objekt um scaleFactor
                Vector3 newScale = transform.localScale * scaleFactor;

                // Überprüfe, ob das Objekt den maxScaleFactor erreicht oder mit dem Charakter kollidiert
                if (newScale.x >= originalScale.x * maxScaleFactor || CheckCollision(newScale))
                {
                    newScale = originalScale * maxScaleFactor;
                    isScalingUp = false; // Ändere die Richtung der Skalierung
                }

                transform.localScale = newScale;
            }
            else
            {
                // Verkleinere das Objekt um scaleFactor
                Vector3 newScale = transform.localScale / scaleFactor;

                // Überprüfe, ob das Objekt wieder auf die Originalgröße zurückgesetzt wurde
                if (newScale.x <= originalScale.x)
                {
                    newScale = originalScale;
                    isScalingUp = true; // Ändere die Richtung der Skalierung
                }

                transform.localScale = newScale;
            }
        }
    }

    bool CheckCollision(Vector3 scale)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, scale / 2f, transform.rotation);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == character)
            {
                return true;
            }
        }

        return false;
    }
}
