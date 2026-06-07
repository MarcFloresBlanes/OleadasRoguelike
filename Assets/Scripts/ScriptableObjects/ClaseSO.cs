using UnityEngine;

[CreateAssetMenu(fileName = "NuevaClase", menuName = "Juego/Clase")]
public class ClaseSO : ScriptableObject
{
    [Header("Información")]
    public string nombreClase;
    public Sprite spritePersonaje;

    [Header("Stats base")]
    public int vidaBase = 100;
    public float velocidadBase = 4f;
    public int dañoBase = 10;
    public float cooldownAtaque = 0.4f;

    [Header("Tipo de ataque")]
    public TipoAtaque tipoAtaque;
}

public enum TipoAtaque
{
    Melee,
    Proyectil
}
