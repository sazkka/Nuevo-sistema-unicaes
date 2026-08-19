namespace SistemaAcademicoUNICAES;

public static class Validaciones
{
    public static int LeerEntero(string mensaje, int minimo, int maximo)
    {
        int numero;

        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (entrada == null)
            {
                Console.WriteLine();
                Environment.Exit(0);
            }

            if (int.TryParse(entrada, out numero) && numero >= minimo && numero <= maximo)
            {
                return numero;
            }

            Console.WriteLine($"Debe ingresar un numero entero entre {minimo} y {maximo}.");
        }
    }

    public static string LeerTextoNoVacio(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (entrada == null)
            {
                Console.WriteLine();
                Environment.Exit(0);
            }

            string texto = entrada.Trim();

            if (texto.Length > 0)
            {
                return texto;
            }

            Console.WriteLine("El texto no puede estar vacio.");
        }
    }

    public static string LeerDireccion(string mensaje)
    {
        while (true)
        {
            string texto = LeerTextoNoVacio(mensaje);

            if (!ContieneSoloSignos(texto))
            {
                return texto;
            }

            Console.WriteLine("La direccion debe contener letras o numeros.");
        }
    }

    public static string LeerCodigoAlfanumerico(string mensaje)
    {
        while (true)
        {
            string codigo = LeerTextoNoVacio(mensaje);

            if (ContieneSoloLetrasYNumeros(codigo))
            {
                return codigo;
            }

            Console.WriteLine("El codigo debe contener solo letras y numeros, sin signos especiales.");
        }
    }

    public static string LeerNombreCompleto(string mensaje)
    {
        while (true)
        {
            string nombre = LeerTextoNoVacio(mensaje);

            if (EsNombreCompletoValido(nombre))
            {
                return nombre;
            }

            Console.WriteLine("Debe ingresar nombre y apellido, usando solo letras y espacios.");
        }
    }

    public static string LeerDui(string mensaje)
    {
        while (true)
        {
            string dui = LeerTextoNoVacio(mensaje);

            if (ContieneSoloDigitos(dui) && dui.Length == 9)
            {
                return dui;
            }

            Console.WriteLine("El DUI debe contener exactamente 9 digitos, sin guiones.");
        }
    }

    public static string LeerTelefono(string mensaje)
    {
        while (true)
        {
            string telefono = LeerTextoNoVacio(mensaje);

            if (ContieneSoloDigitos(telefono) && telefono.Length == 8)
            {
                return telefono;
            }

            Console.WriteLine("El telefono debe contener exactamente 8 digitos.");
        }
    }

    private static bool ContieneSoloLetrasYNumeros(string texto)
    {
        for (int i = 0; i < texto.Length; i++)
        {
            if (!char.IsLetterOrDigit(texto[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ContieneSoloDigitos(string texto)
    {
        for (int i = 0; i < texto.Length; i++)
        {
            if (!char.IsDigit(texto[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool EsNombreCompletoValido(string texto)
    {
        int cantidadPalabras = 0;
        bool dentroDePalabra = false;

        for (int i = 0; i < texto.Length; i++)
        {
            char caracter = texto[i];

            if (char.IsLetter(caracter))
            {
                if (!dentroDePalabra)
                {
                    cantidadPalabras++;
                    dentroDePalabra = true;
                }
            }
            else if (caracter == ' ')
            {
                dentroDePalabra = false;
            }
            else
            {
                return false;
            }
        }

        return cantidadPalabras >= 2;
    }

    private static bool ContieneSoloSignos(string texto)
    {
        for (int i = 0; i < texto.Length; i++)
        {
            if (char.IsLetterOrDigit(texto[i]))
            {
                return false;
            }
        }

        return true;
    }
}
