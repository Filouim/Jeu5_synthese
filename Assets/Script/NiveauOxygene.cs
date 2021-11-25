using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe qui gere le comportement de la barre d'oxygene.
/// Auteur du code : Philippe Hubert
/// Auteur des commentaires : Philippe Hubert
/// </summary>
public class NiveauOxygene : MonoBehaviour
{
    public Slider slider; // Valeur de la barre d'oxygene

    /// <summary>
    /// Ajuste le niveau d'oxygene maximum selon la valeur en parametre.
    /// </summary>
    /// <param name="oxygene">Valeur d'oxygene.</param>
    public void SetMaxOxygene(float oxygene)
    {
        slider.maxValue = oxygene;
        slider.value = oxygene;
    }

    /// <summary>
    /// Ajuste la barre d'oxygene selon la valeur en parametre.
    /// </summary>
    /// <param name="oxygene">Valeur d'oxygene.</param>
    public void SetOxygene(float oxygene)
    {
        slider.value = oxygene;
    }
}
