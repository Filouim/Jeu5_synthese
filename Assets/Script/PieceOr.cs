using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A integrer a l'interieur de la classe GameManager.
/// </summary>
public class PieceOr : MonoBehaviour
{
    public int points = 0;
    public Text txtPointage;

    /// <summary>
    /// Ajoute un point au champ de texte du pointage.
    /// </summary>
    public void AjouterPoints(int montant)
    {
        points = points + montant;
        txtPointage.text = points.ToString();
    }

    /// <summary>
    /// Ajoute un point au champ de texte du pointage.
    /// </summary>
    public void RetirerPoints(int montant)
    {
        points = points - montant;
        txtPointage.text = points.ToString();
    }
}
