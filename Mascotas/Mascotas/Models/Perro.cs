namespace Mascotas.Models;

public class Perro : Mascota
{
    public string Raza { get;}
    public string Tamaño { get;}
    public string Color { get;}

    public Perro(string nombre, int edad, double peso, string sexo, string propietario, bool estado, string raza, string tamaño, string color)
        : base(nombre, edad, peso, sexo, propietario, estado)
    {
        Raza = raza;
        Tamaño = tamaño;
        Color = color;
    }

    public override double CalcularDosis(double dosis_kg)
    {
        return Peso * dosis_kg;
    }
}