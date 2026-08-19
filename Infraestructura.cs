namespace SistemaAcademicoUNICAES;

public static class Infraestructura
{
    private static readonly string[] Sedes = { "Santa Ana", "Ilobasco" };
    private static readonly string[] Edificios = { "A", "B", "C", "D", "E", "F", "G", "H" };
    private static readonly string[] Facultades =
    {
        "Facultad de Ciencias Empresariales",
        "Facultad de Ingenieria y Arquitectura",
        "Facultad de Ciencias y Humanidades",
        "Facultad de Ciencias de la Salud"
    };
    private static readonly string[] Modalidades = { "Presencial", "Semipresencial", "No presencial" };
    private static readonly string[] Departamentos =
    {
        "Administracion",
        "Recursos Humanos",
        "Registro Academico",
        "Biblioteca",
        "Tecnologia",
        "Mantenimiento",
        "Seguridad",
        "Servicios Generales"
    };
    private static readonly string[] TiposEmpleado =
    {
        "Profesor",
        "Administrativo",
        "Mantenimiento",
        "Seguridad",
        "Servicios Generales"
    };

    public static void InicializarSalones(string[] sedesSalones, string[] edificiosSalones, int[] numerosSalones)
    {
        int posicion = 0;

        for (int i = 0; i < Sedes.Length; i++)
        {
            for (int j = 0; j < Edificios.Length; j++)
            {
                for (int k = 1; k <= 6; k++)
                {
                    sedesSalones[posicion] = Sedes[i];
                    edificiosSalones[posicion] = Edificios[j];
                    numerosSalones[posicion] = k;
                    posicion++;
                }
            }
        }
    }

    public static string SeleccionarSede()
    {
        Console.WriteLine("Sede:");
        MostrarOpciones(Sedes);
        int opcion = Validaciones.LeerEntero("Seleccione una sede: ", 1, Sedes.Length);
        return Sedes[opcion - 1];
    }

    public static string SeleccionarEdificio()
    {
        Console.WriteLine("Edificio:");
        MostrarOpciones(Edificios);
        int opcion = Validaciones.LeerEntero("Seleccione un edificio: ", 1, Edificios.Length);
        return Edificios[opcion - 1];
    }

    public static string SeleccionarFacultad()
    {
        Console.WriteLine("Facultad:");
        MostrarOpciones(Facultades);
        int opcion = Validaciones.LeerEntero("Seleccione una facultad: ", 1, Facultades.Length);
        return Facultades[opcion - 1];
    }

    public static string SeleccionarModalidad(string facultad)
    {
        string[] modalidades = ObtenerModalidadesPorFacultad(facultad);

        Console.WriteLine("Modalidad:");
        MostrarOpciones(modalidades);

        int opcion = Validaciones.LeerEntero("Seleccione una modalidad: ", 1, modalidades.Length);
        return modalidades[opcion - 1];
    }

    public static string SeleccionarCarrera(string facultad, string modalidad)
    {
        string[] carreras = ObtenerCarreras(facultad, modalidad);

        Console.WriteLine("Carrera:");
        MostrarOpciones(carreras);

        int opcion = Validaciones.LeerEntero("Seleccione una carrera: ", 1, carreras.Length);
        return carreras[opcion - 1];
    }

    public static void SeleccionarDatosAcademicos(out string facultad, out string modalidad, out string carrera)
    {
        facultad = SeleccionarFacultad();
        modalidad = SeleccionarModalidad(facultad);
        carrera = SeleccionarCarrera(facultad, modalidad);
    }

    public static string SeleccionarDepartamento()
    {
        Console.WriteLine("Departamento:");
        MostrarOpciones(Departamentos);
        int opcion = Validaciones.LeerEntero("Seleccione un departamento: ", 1, Departamentos.Length);
        return Departamentos[opcion - 1];
    }

    public static string SeleccionarCargo(string departamento)
    {
        string[] cargos = ObtenerCargosPorDepartamento(departamento);

        Console.WriteLine("Cargo:");
        MostrarOpciones(cargos);

        int opcion = Validaciones.LeerEntero("Seleccione un cargo: ", 1, cargos.Length);
        return cargos[opcion - 1];
    }

    public static string SeleccionarTipoEmpleado()
    {
        Console.WriteLine("Tipo de empleado:");
        MostrarOpciones(TiposEmpleado);
        int opcion = Validaciones.LeerEntero("Seleccione un tipo: ", 1, TiposEmpleado.Length);
        return TiposEmpleado[opcion - 1];
    }

    public static void MostrarOpciones(string[] opciones)
    {
        for (int i = 0; i < opciones.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {opciones[i]}");
        }
    }

    private static string[] ObtenerModalidadesPorFacultad(string facultad)
    {
        if (facultad == Facultades[2])
        {
            return new string[] { Modalidades[0], Modalidades[1] };
        }

        if (facultad == Facultades[3])
        {
            return new string[] { Modalidades[0] };
        }

        return new string[] { Modalidades[0], Modalidades[1], Modalidades[2] };
    }

    private static string[] ObtenerCarreras(string facultad, string modalidad)
    {
        if (facultad == Facultades[0] && modalidad == Modalidades[0])
        {
            return new string[]
            {
                "Licenciatura en Administracion de Empresas",
                "Licenciatura en Contaduria Publica",
                "Licenciatura en Mercadeo y Negocios Internacionales",
                "Licenciatura en Gestion y Desarrollo Turistico",
                "Licenciatura en Gestion de Negocios Digitales",
                "Licenciatura en Relaciones Internacionales y Comercio Exterior",
                "Licenciatura en Gastronomia y Hosteleria"
            };
        }

        if (facultad == Facultades[0] && modalidad == Modalidades[1])
        {
            return new string[]
            {
                "Licenciatura en Administracion de Empresas",
                "Licenciatura en Logistica y Operaciones"
            };
        }

        if (facultad == Facultades[0])
        {
            return new string[]
            {
                "Tecnico en Gestion de Ventas",
                "Licenciatura en Gestion de Negocios Digitales"
            };
        }

        if (facultad == Facultades[1] && modalidad == Modalidades[0])
        {
            return new string[]
            {
                "Ingenieria Quimica",
                "Ingenieria Mecanica",
                "Ingenieria en Desarrollo de Software",
                "Ingenieria en Telecomunicaciones y Redes",
                "Arquitectura",
                "Ingenieria Civil",
                "Ingenieria en Sistemas Informaticos",
                "Ingenieria Agronomica",
                "Ingenieria Industrial",
                "Ingenieria Electrica"
            };
        }

        if (facultad == Facultades[1] && modalidad == Modalidades[1])
        {
            return new string[]
            {
                "Ingenieria en Tecnologia y Procesamiento de Alimentos",
                "Tecnico en Textiles"
            };
        }

        if (facultad == Facultades[1])
        {
            return new string[] { "Ingenieria en Desarrollo de Software" };
        }

        if (facultad == Facultades[2] && modalidad == Modalidades[0])
        {
            return new string[]
            {
                "Licenciatura en Diseno Grafico Publicitario",
                "Licenciatura en Ciencias Juridicas",
                "Licenciatura en Periodismo y Comunicacion Audiovisual",
                "Licenciatura en Idioma Ingles",
                "Licenciatura en Ciencias de la Educacion con Especialidad en Idioma Ingles",
                "Licenciatura en Ciencias Religiosas"
            };
        }

        if (facultad == Facultades[2])
        {
            return new string[] { "Licenciatura en Idioma Ingles" };
        }

        return new string[]
        {
            "Doctorado en Medicina",
            "Licenciatura en Enfermeria",
            "Tecnico en Enfermeria",
            "Licenciatura en Nutricion y Dietetica",
            "Licenciatura en Quimica y Farmacia"
        };
    }

    private static string[] ObtenerCargosPorDepartamento(string departamento)
    {
        if (departamento == "Tecnologia")
        {
            return new string[] { "Tecnico", "Soporte", "Coordinador" };
        }

        if (departamento == "Mantenimiento" || departamento == "Seguridad" || departamento == "Servicios Generales")
        {
            return new string[] { "Encargado", "Auxiliar", "Supervisor" };
        }

        if (departamento == "Registro Academico")
        {
            return new string[] { "Docente", "Coordinador", "Auxiliar administrativo" };
        }

        return new string[] { "Administrador", "Auxiliar administrativo", "Coordinador" };
    }
}
