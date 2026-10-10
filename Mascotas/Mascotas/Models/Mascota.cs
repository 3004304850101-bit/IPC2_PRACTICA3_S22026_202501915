namespace Mascotas.Models;

public abstract class Mascota
{
    public string Nombre { get; private set; }
    public double Peso { get; private set; }
    public string Sexo { get;}
    public int Edad { get; private set; }
    public string Propietario { get;  private set; }
    public string Id { get;}
    public bool Estable { get; private set; }
    private static Random random = new Random();
    
    public static string IdMascota(){
    string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    string id = "";

    for (int i = 1; i <= 8; i++){
        int num = random.Next(0, caracteres.Length);
        id += caracteres.Substring(num, 1);
    }
    return id;
    }

    public Mascota(string nombre, int edad, double peso, string sexo, string propietario, bool estado)
    {
        Nombre = nombre;
        Edad = edad;
        Peso = peso;
        Sexo = sexo;
        Propietario = propietario;
        Id = IdMascota();
        Estable = estado;
    }

    public abstract double CalcularDosis(double dosis_kg);

    public bool ActualizarNombre(string nuevoNombre)
    {
        Nombre=nuevoNombre;
        return true;
    }

    public bool ActualizarPeso(double nuevoPeso)
    {
        Peso=nuevoPeso;
        return true;
    }

    public bool ActualizarEdad(int nuevaEdad)
    {
        Edad=nuevaEdad;
        return true;
    }

    public bool ActualizarPropietario(string nuevoPropietario)
    {
        Propietario=nuevoPropietario;
        return true;
    }

    public bool CambiarEstado(bool nuevoEstado)
    {
        Estable = nuevoEstado;
        return true;
    }

}