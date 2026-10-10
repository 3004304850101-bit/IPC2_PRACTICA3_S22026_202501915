namespace Mascotas.Models;

public class Tortuga : Mascota
{
    public string Especie { get;}
    public string Color { get;}
    public bool Acuatico { get;}

    public Tortuga(string nombre, int edad, double peso, string sexo, string propietario, bool estado, string especie, string color, bool acuatico)
        : base(nombre, edad, peso, sexo, propietario, estado)
    {
        Especie = especie;
        Color = color;
        Acuatico = acuatico;
    }

    public override double CalcularDosis(double dosis_kg)
    {
        return Peso * dosis_kg * 0.80;
    }
}