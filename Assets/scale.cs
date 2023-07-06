using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float scaleFactor = 1.02f; // Der Faktor, um den das Objekt vergrößert oder verkleinert wird
    public float maxScaleFactor = 1.5f; // Der maximale Faktor, um den das Objekt skaliert wird (50% größer als die Originalgröße)

    private Vector3 originalScale; // Die ursprüngliche Skalierung des Objekts
    private bool isScalingUp = true; // Gibt an, ob das Objekt vergrößert oder verkleinert wird

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Time.frameCount % 10 == 0) // Überprüfe, ob alle 10 Frames erreicht wurden
        {
            if (isScalingUp)
            {
                // Vergrößere das Objekt um scaleFactor
                Vector3 newScale = transform.localScale * scaleFactor;
                transform.localScale = newScale;

                // Überprüfe, ob das Objekt den maxScaleFactor erreicht hat
                if (transform.localScale.x >= originalScale.x * maxScaleFactor)
                {
                    isScalingUp = false; // Ändere die Richtung der Skalierung
                }
            }
            else
            {
                // Verkleinere das Objekt um scaleFactor
                Vector3 newScale = transform.localScale / scaleFactor;
                transform.localScale = newScale;

                // Überprüfe, ob das Objekt wieder auf die Originalgröße zurückgesetzt wurde
                if (transform.localScale.x <= originalScale.x)
                {
                    isScalingUp = true; // Ändere die Richtung der Skalierung
                }
            }
        }
    }
}
