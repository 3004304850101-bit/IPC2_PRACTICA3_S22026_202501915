using Mascotas.Models;

namespace TDA
{
    public class NodoMascota
    {
        public Mascota Mascota { get; set; }
        public NodoMascota? Siguiente { get; set; }

        public NodoMascota(Mascota mascota)
        {
            Mascota = mascota;
        }
    }
    public class ListaMascota
    {
        private NodoMascota? primero;
        private NodoMascota? ultimo;

        public void AgregarMascota(Mascota mascota)
        {
            NodoMascota nuevoNodo = new NodoMascota(mascota);
            if (primero == null || ultimo == null)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                ultimo.Siguiente = nuevoNodo;
                ultimo = nuevoNodo;
            }
        }

        public Mascota? BuscarMascota(string id)
        {
            NodoMascota? actual = primero;
            while (actual != null)
            {
                if (actual.Mascota.Id == id)
                {
                    return actual.Mascota;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool EliminarMascota(string id)
        {
            NodoMascota? actual = primero;
            NodoMascota? anterior = null;

            while (actual != null)
            {
                if (actual.Mascota.Id == id)
                {
                    if (anterior == null)
                    {
                        primero = actual.Siguiente;
                    }
                    else
                    {
                        anterior.Siguiente = actual.Siguiente;
                    }

                    if (actual == ultimo)
                    {
                        ultimo = anterior;
                    }

                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public int ContarMascotas()
        {
            int contador = 0;
            NodoMascota? actual = primero;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        public Mascota? Obtener(int i)
        {
            NodoMascota? actual = primero;
            int contador = 0;
            while (actual != null)
            {
                if (contador == i)
                {
                    return actual.Mascota;
                }
                contador++;
                actual = actual.Siguiente;
            }
            return null;
        }
    }
}