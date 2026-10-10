namespace Mascotas.Models;

public class Gato : Mascota
{
    public string Raza { get;}
    public string Color { get;}
    public bool Estirilizado { get; private set;}

    public Gato(string nombre, int edad, double peso, string sexo, string propietario, bool estado, string raza, string color, bool estirilizado)
        : base(nombre, edad, peso, sexo, propietario, estado)
    {
        Raza = raza;
        Color = color;
        Estirilizado = estirilizado;
    }

    public override double CalcularDosis(double dosis_kg)
    {
        return Peso*dosis_kg*0.90;
    }

    public void CambiarEsterilizado(bool esterilizado)
    {
        Estirilizado=esterilizado;
    }
}