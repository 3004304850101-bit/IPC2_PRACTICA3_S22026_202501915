namespace Mascotas.Models;

public class Ave : Mascota
{
    public string Especie { get;}
    public string Color { get;}
    public string Envergadura { get;}
    public bool Volar { get; private set;}

    public Ave(string nombre, int edad, double peso, string sexo, string propietario, bool estado, string especie, string color, string envergadura, bool volar)
        : base(nombre, edad, peso, sexo, propietario, estado)
    {
        Especie = especie;
        Color = color;
        Envergadura = envergadura;
        Volar = volar;
    }

    public override double CalcularDosis(double dosis_kg)
    {
        return Peso * dosis_kg * 0.5;
    }

    public void CambiarVolar(bool volar)
    {
        Volar=volar;
    }
}
