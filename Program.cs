using SistemaAcademicoUNICAES;

string[] codigosAlumnos = new string[500];
string[] nombresAlumnos = new string[500];
int[] edadesAlumnos = new int[500];
string[] duisAlumnos = new string[500];
string[] telefonosAlumnos = new string[500];
string[] direccionesAlumnos = new string[500];
string[] sedesAlumnos = new string[500];
string[] facultadesAlumnos = new string[500];
string[] modalidadesAlumnos = new string[500];
string[] carrerasAlumnos = new string[500];
bool[] alumnosConSalon = new bool[500];
string[] sedesSalonAlumno = new string[500];
string[] edificiosSalonAlumno = new string[500];
int[] numerosSalonAlumno = new int[500];

string[] codigosEmpleados = new string[300];
string[] nombresEmpleados = new string[300];
int[] edadesEmpleados = new int[300];
string[] duisEmpleados = new string[300];
string[] telefonosEmpleados = new string[300];
string[] direccionesEmpleados = new string[300];
string[] sedesEmpleados = new string[300];
string[] departamentosEmpleados = new string[300];
string[] cargosEmpleados = new string[300];
string[] tiposEmpleados = new string[300];

string[] sedesSalones = new string[96];
string[] edificiosSalones = new string[96];
int[] numerosSalones = new int[96];
string[] profesoresGuiaSalones = new string[96];
string[,] alumnosPorSalon = new string[96, 50];
int[] cantidadAlumnosSalon = new int[96];

int cantidadAlumnos = 0;
int cantidadEmpleados = 0;
int opcion;

Infraestructura.InicializarSalones(sedesSalones, edificiosSalones, numerosSalones);
InicializarTextos();

do
{
    MostrarTitulo();
    MostrarMenuPrincipal();
    opcion = Validaciones.LeerEntero("Seleccione una opcion: ", 1, 10);

    if (opcion == 1) cantidadAlumnos = RegistrarAlumno(cantidadAlumnos);
    else if (opcion == 2) ConsultarAlumnos(cantidadAlumnos);
    else if (opcion == 3) ModificarAlumno(cantidadAlumnos);
    else if (opcion == 4) cantidadAlumnos = EliminarAlumno(cantidadAlumnos);
    else if (opcion == 5) cantidadEmpleados = RegistrarEmpleado(cantidadEmpleados);
    else if (opcion == 6) ConsultarEmpleados(cantidadEmpleados);
    else if (opcion == 7) ModificarEmpleado(cantidadEmpleados);
    else if (opcion == 8) cantidadEmpleados = EliminarEmpleado(cantidadEmpleados);
    else if (opcion == 9) MenuSalones(cantidadAlumnos, cantidadEmpleados);

    if (opcion != 10)
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }
}
while (opcion != 10);

Console.WriteLine("Gracias por usar el sistema.");

void MostrarTitulo()
{
    Console.WriteLine();
    Console.WriteLine("==============================================");
    Console.WriteLine(" SISTEMA CENTRALIZADO UNICAES");
    Console.WriteLine("==============================================");
}

void MostrarMenuPrincipal()
{
    Console.WriteLine("1. Registrar alumno");
    Console.WriteLine("2. Consultar alumnos");
    Console.WriteLine("3. Modificar alumno");
    Console.WriteLine("4. Eliminar alumno");
    Console.WriteLine("5. Registrar empleado");
    Console.WriteLine("6. Consultar empleados");
    Console.WriteLine("7. Modificar empleado");
    Console.WriteLine("8. Eliminar empleado");
    Console.WriteLine("9. Gestion de salones");
    Console.WriteLine("10. Salir");
}

void InicializarTextos()
{
    for (int i = 0; i < profesoresGuiaSalones.Length; i++)
    {
        profesoresGuiaSalones[i] = "";
    }

    for (int i = 0; i < sedesSalonAlumno.Length; i++)
    {
        sedesSalonAlumno[i] = "";
        edificiosSalonAlumno[i] = "";
    }

    for (int i = 0; i < alumnosPorSalon.GetLength(0); i++)
    {
        for (int j = 0; j < alumnosPorSalon.GetLength(1); j++)
        {
            alumnosPorSalon[i, j] = "";
        }
    }
}

int RegistrarAlumno(int cantidad)
{
    if (cantidad >= codigosAlumnos.Length)
    {
        Console.WriteLine("No se pueden registrar mas alumnos.");
        return cantidad;
    }

    Console.WriteLine();
    Console.WriteLine("REGISTRO DE ALUMNO");
    string codigo;

    while (true)
    {
        codigo = Validaciones.LeerCodigoAlfanumerico("Codigo: ");
        if (BuscarAlumno(codigo, cantidad) == -1) break;
        Console.WriteLine("Ya existe un alumno con ese codigo.");
    }

    codigosAlumnos[cantidad] = codigo;
    nombresAlumnos[cantidad] = Validaciones.LeerNombreCompleto("Nombre completo: ");
    edadesAlumnos[cantidad] = Validaciones.LeerEntero("Edad: ", 17, 60);
    duisAlumnos[cantidad] = Validaciones.LeerDui("DUI: ");
    telefonosAlumnos[cantidad] = Validaciones.LeerTelefono("Telefono: ");
    direccionesAlumnos[cantidad] = Validaciones.LeerDireccion("Direccion: ");
    sedesAlumnos[cantidad] = Infraestructura.SeleccionarSede();
    Infraestructura.SeleccionarDatosAcademicos(out facultadesAlumnos[cantidad], out modalidadesAlumnos[cantidad], out carrerasAlumnos[cantidad]);

    Console.WriteLine("Alumno registrado correctamente.");
    return cantidad + 1;
}

void ConsultarAlumnos(int cantidad)
{
    if (cantidad == 0)
    {
        Console.WriteLine("No hay alumnos registrados.");
        return;
    }

    Console.WriteLine("1. Buscar por codigo");
    Console.WriteLine("2. Listar por sede");
    Console.WriteLine("3. Listar por carrera");
    int opcionConsulta = Validaciones.LeerEntero("Seleccione una opcion: ", 1, 3);

    if (opcionConsulta == 1)
    {
        int posicion = BuscarAlumno(Validaciones.LeerCodigoAlfanumerico("Codigo: "), cantidad);
        if (posicion == -1) Console.WriteLine("No se encontro un alumno con ese codigo.");
        else MostrarAlumno(posicion);
    }
    else if (opcionConsulta == 2)
    {
        string sede = Infraestructura.SeleccionarSede();
        bool encontrado = false;

        for (int i = 0; i < cantidad; i++)
        {
            if (sedesAlumnos[i] == sede)
            {
                MostrarAlumno(i);
                encontrado = true;
            }
        }

        if (!encontrado) Console.WriteLine("No hay alumnos registrados en esa sede.");
    }
    else
    {
        Infraestructura.SeleccionarDatosAcademicos(out string facultad, out string modalidad, out string carrera);
        bool encontrado = false;

        for (int i = 0; i < cantidad; i++)
        {
            if (facultadesAlumnos[i] == facultad && modalidadesAlumnos[i] == modalidad && carrerasAlumnos[i] == carrera)
            {
                MostrarAlumno(i);
                encontrado = true;
            }
        }

        if (!encontrado) Console.WriteLine("No hay alumnos registrados en esa carrera.");
    }
}

void ModificarAlumno(int cantidad)
{
    int posicion = BuscarAlumno(Validaciones.LeerCodigoAlfanumerico("Codigo del alumno: "), cantidad);
    if (posicion == -1)
    {
        Console.WriteLine("No se encontro un alumno con ese codigo.");
        return;
    }

    MostrarAlumno(posicion);
    nombresAlumnos[posicion] = Validaciones.LeerNombreCompleto("Nuevo nombre completo: ");
    edadesAlumnos[posicion] = Validaciones.LeerEntero("Nueva edad: ", 17, 60);
    duisAlumnos[posicion] = Validaciones.LeerDui("Nuevo DUI: ");
    telefonosAlumnos[posicion] = Validaciones.LeerTelefono("Nuevo telefono: ");
    direccionesAlumnos[posicion] = Validaciones.LeerDireccion("Nueva direccion: ");
    sedesAlumnos[posicion] = Infraestructura.SeleccionarSede();
    Infraestructura.SeleccionarDatosAcademicos(out facultadesAlumnos[posicion], out modalidadesAlumnos[posicion], out carrerasAlumnos[posicion]);
    Console.WriteLine("Alumno modificado correctamente.");
}

int EliminarAlumno(int cantidad)
{
    string codigo = Validaciones.LeerCodigoAlfanumerico("Codigo del alumno: ");
    int posicion = BuscarAlumno(codigo, cantidad);

    if (posicion == -1)
    {
        Console.WriteLine("No se encontro un alumno con ese codigo.");
        return cantidad;
    }

    QuitarAlumnoDeSalones(codigo);

    for (int i = posicion; i < cantidad - 1; i++)
    {
        codigosAlumnos[i] = codigosAlumnos[i + 1];
        nombresAlumnos[i] = nombresAlumnos[i + 1];
        edadesAlumnos[i] = edadesAlumnos[i + 1];
        duisAlumnos[i] = duisAlumnos[i + 1];
        telefonosAlumnos[i] = telefonosAlumnos[i + 1];
        direccionesAlumnos[i] = direccionesAlumnos[i + 1];
        sedesAlumnos[i] = sedesAlumnos[i + 1];
        facultadesAlumnos[i] = facultadesAlumnos[i + 1];
        modalidadesAlumnos[i] = modalidadesAlumnos[i + 1];
        carrerasAlumnos[i] = carrerasAlumnos[i + 1];
        alumnosConSalon[i] = alumnosConSalon[i + 1];
        sedesSalonAlumno[i] = sedesSalonAlumno[i + 1];
        edificiosSalonAlumno[i] = edificiosSalonAlumno[i + 1];
        numerosSalonAlumno[i] = numerosSalonAlumno[i + 1];
    }

    Console.WriteLine("Alumno eliminado correctamente.");
    return cantidad - 1;
}

int RegistrarEmpleado(int cantidad)
{
    if (cantidad >= codigosEmpleados.Length)
    {
        Console.WriteLine("No se pueden registrar mas empleados.");
        return cantidad;
    }

    Console.WriteLine();
    Console.WriteLine("REGISTRO DE EMPLEADO");
    string codigo;

    while (true)
    {
        codigo = Validaciones.LeerCodigoAlfanumerico("Codigo: ");
        if (BuscarEmpleado(codigo, cantidad) == -1) break;
        Console.WriteLine("Ya existe un empleado con ese codigo.");
    }

    codigosEmpleados[cantidad] = codigo;
    nombresEmpleados[cantidad] = Validaciones.LeerNombreCompleto("Nombre completo: ");
    edadesEmpleados[cantidad] = Validaciones.LeerEntero("Edad: ", 18, 75);
    duisEmpleados[cantidad] = Validaciones.LeerDui("DUI: ");
    telefonosEmpleados[cantidad] = Validaciones.LeerTelefono("Telefono: ");
    direccionesEmpleados[cantidad] = Validaciones.LeerDireccion("Direccion: ");
    sedesEmpleados[cantidad] = Infraestructura.SeleccionarSede();
    departamentosEmpleados[cantidad] = Infraestructura.SeleccionarDepartamento();
    cargosEmpleados[cantidad] = Infraestructura.SeleccionarCargo(departamentosEmpleados[cantidad]);
    tiposEmpleados[cantidad] = Infraestructura.SeleccionarTipoEmpleado();

    Console.WriteLine("Empleado registrado correctamente.");
    return cantidad + 1;
}

void ConsultarEmpleados(int cantidad)
{
    if (cantidad == 0)
    {
        Console.WriteLine("No hay empleados registrados.");
        return;
    }

    Console.WriteLine("1. Buscar por codigo");
    Console.WriteLine("2. Listar por sede");
    Console.WriteLine("3. Listar por departamento");
    int opcionConsulta = Validaciones.LeerEntero("Seleccione una opcion: ", 1, 3);

    if (opcionConsulta == 1)
    {
        int posicion = BuscarEmpleado(Validaciones.LeerCodigoAlfanumerico("Codigo: "), cantidad);
        if (posicion == -1) Console.WriteLine("No se encontro un empleado con ese codigo.");
        else MostrarEmpleado(posicion);
    }
    else if (opcionConsulta == 2)
    {
        string sede = Infraestructura.SeleccionarSede();
        MostrarEmpleadosFiltrados(cantidad, sede, "");
    }
    else
    {
        string departamento = Infraestructura.SeleccionarDepartamento();
        MostrarEmpleadosFiltrados(cantidad, "", departamento);
    }
}

void ModificarEmpleado(int cantidad)
{
    int posicion = BuscarEmpleado(Validaciones.LeerCodigoAlfanumerico("Codigo del empleado: "), cantidad);
    if (posicion == -1)
    {
        Console.WriteLine("No se encontro un empleado con ese codigo.");
        return;
    }

    MostrarEmpleado(posicion);
    nombresEmpleados[posicion] = Validaciones.LeerNombreCompleto("Nuevo nombre completo: ");
    edadesEmpleados[posicion] = Validaciones.LeerEntero("Nueva edad: ", 18, 75);
    duisEmpleados[posicion] = Validaciones.LeerDui("Nuevo DUI: ");
    telefonosEmpleados[posicion] = Validaciones.LeerTelefono("Nuevo telefono: ");
    direccionesEmpleados[posicion] = Validaciones.LeerDireccion("Nueva direccion: ");
    sedesEmpleados[posicion] = Infraestructura.SeleccionarSede();
    departamentosEmpleados[posicion] = Infraestructura.SeleccionarDepartamento();
    cargosEmpleados[posicion] = Infraestructura.SeleccionarCargo(departamentosEmpleados[posicion]);
    tiposEmpleados[posicion] = Infraestructura.SeleccionarTipoEmpleado();
    Console.WriteLine("Empleado modificado correctamente.");
}

int EliminarEmpleado(int cantidad)
{
    string codigo = Validaciones.LeerCodigoAlfanumerico("Codigo del empleado: ");
    int posicion = BuscarEmpleado(codigo, cantidad);

    if (posicion == -1)
    {
        Console.WriteLine("No se encontro un empleado con ese codigo.");
        return cantidad;
    }

    QuitarProfesorDeSalones(codigo);

    for (int i = posicion; i < cantidad - 1; i++)
    {
        codigosEmpleados[i] = codigosEmpleados[i + 1];
        nombresEmpleados[i] = nombresEmpleados[i + 1];
        edadesEmpleados[i] = edadesEmpleados[i + 1];
        duisEmpleados[i] = duisEmpleados[i + 1];
        telefonosEmpleados[i] = telefonosEmpleados[i + 1];
        direccionesEmpleados[i] = direccionesEmpleados[i + 1];
        sedesEmpleados[i] = sedesEmpleados[i + 1];
        departamentosEmpleados[i] = departamentosEmpleados[i + 1];
        cargosEmpleados[i] = cargosEmpleados[i + 1];
        tiposEmpleados[i] = tiposEmpleados[i + 1];
    }

    Console.WriteLine("Empleado eliminado correctamente.");
    return cantidad - 1;
}

void MenuSalones(int totalAlumnos, int totalEmpleados)
{
    int opcionSalon;
    do
    {
        Console.WriteLine();
        Console.WriteLine("GESTION DE SALONES");
        Console.WriteLine("1. Ver salones por sede y edificio");
        Console.WriteLine("2. Asignar alumno a salon");
        Console.WriteLine("3. Asignar profesor guia a salon");
        Console.WriteLine("4. Listar alumnos de un salon");
        Console.WriteLine("5. Consultar profesor guia de un salon");
        Console.WriteLine("6. Volver al menu principal");
        opcionSalon = Validaciones.LeerEntero("Seleccione una opcion: ", 1, 6);

        if (opcionSalon == 1) VerSalones();
        else if (opcionSalon == 2) AsignarAlumnoASalon(totalAlumnos);
        else if (opcionSalon == 3) AsignarProfesorGuia(totalEmpleados);
        else if (opcionSalon == 4) ListarAlumnosSalon(totalAlumnos);
        else if (opcionSalon == 5) ConsultarProfesorGuia(totalEmpleados);
    }
    while (opcionSalon != 6);
}

void VerSalones()
{
    string sede = Infraestructura.SeleccionarSede();
    string edificio = Infraestructura.SeleccionarEdificio();

    for (int i = 0; i < sedesSalones.Length; i++)
    {
        if (sedesSalones[i] == sede && edificiosSalones[i] == edificio)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Salon: {edificiosSalones[i]}-{numerosSalones[i]}");
            Console.WriteLine($"Capacidad: {cantidadAlumnosSalon[i]}/50");
            Console.WriteLine(profesoresGuiaSalones[i] == "" ? "Profesor guia: Sin asignar" : $"Profesor guia: {profesoresGuiaSalones[i]}");
        }
    }
}

void AsignarAlumnoASalon(int totalAlumnos)
{
    int[] disponibles = new int[totalAlumnos];
    int cantidadDisponibles = 0;

    for (int i = 0; i < totalAlumnos; i++)
    {
        if (!alumnosConSalon[i])
        {
            disponibles[cantidadDisponibles] = i;
            cantidadDisponibles++;
        }
    }

    if (cantidadDisponibles == 0)
    {
        Console.WriteLine("No hay alumnos disponibles para asignar.");
        return;
    }

    Console.WriteLine("Seleccione alumno:");
    for (int i = 0; i < cantidadDisponibles; i++)
    {
        Console.WriteLine($"{i + 1}. {codigosAlumnos[disponibles[i]]} - {nombresAlumnos[disponibles[i]]}");
    }

    int opcionAlumno = Validaciones.LeerEntero("Seleccione una opcion: ", 1, cantidadDisponibles);
    int posicionAlumno = disponibles[opcionAlumno - 1];
    int posicionSalon = SeleccionarSalon();

    if (cantidadAlumnosSalon[posicionSalon] >= 50)
    {
        Console.WriteLine("El salon ya alcanzo su capacidad maxima.");
        return;
    }

    alumnosPorSalon[posicionSalon, cantidadAlumnosSalon[posicionSalon]] = codigosAlumnos[posicionAlumno];
    cantidadAlumnosSalon[posicionSalon]++;
    alumnosConSalon[posicionAlumno] = true;
    sedesSalonAlumno[posicionAlumno] = sedesSalones[posicionSalon];
    edificiosSalonAlumno[posicionAlumno] = edificiosSalones[posicionSalon];
    numerosSalonAlumno[posicionAlumno] = numerosSalones[posicionSalon];
    Console.WriteLine("Alumno asignado correctamente.");
}

void AsignarProfesorGuia(int totalEmpleados)
{
    int posicionSalon = SeleccionarSalon();

    if (profesoresGuiaSalones[posicionSalon] != "")
    {
        Console.WriteLine("Este salon ya tiene profesor guia asignado.");
        return;
    }

    int[] disponibles = new int[totalEmpleados];
    int cantidadDisponibles = 0;

    for (int i = 0; i < totalEmpleados; i++)
    {
        if (tiposEmpleados[i] == "Profesor" && !ProfesorYaAsignado(codigosEmpleados[i]))
        {
            disponibles[cantidadDisponibles] = i;
            cantidadDisponibles++;
        }
    }

    if (cantidadDisponibles == 0)
    {
        Console.WriteLine("No hay profesores disponibles para asignar.");
        return;
    }

    Console.WriteLine("Seleccione profesor guia:");
    for (int i = 0; i < cantidadDisponibles; i++)
    {
        Console.WriteLine($"{i + 1}. {codigosEmpleados[disponibles[i]]} - {nombresEmpleados[disponibles[i]]}");
    }

    int opcionProfesor = Validaciones.LeerEntero("Seleccione una opcion: ", 1, cantidadDisponibles);
    profesoresGuiaSalones[posicionSalon] = codigosEmpleados[disponibles[opcionProfesor - 1]];
    Console.WriteLine("Profesor guia asignado correctamente.");
}

void ListarAlumnosSalon(int totalAlumnos)
{
    int posicionSalon = SeleccionarSalon();

    if (cantidadAlumnosSalon[posicionSalon] == 0)
    {
        Console.WriteLine("El salon no tiene alumnos asignados.");
        return;
    }

    for (int i = 0; i < cantidadAlumnosSalon[posicionSalon]; i++)
    {
        int posicionAlumno = BuscarAlumno(alumnosPorSalon[posicionSalon, i], totalAlumnos);
        if (posicionAlumno != -1) MostrarAlumno(posicionAlumno);
    }
}

void ConsultarProfesorGuia(int totalEmpleados)
{
    int posicionSalon = SeleccionarSalon();

    if (profesoresGuiaSalones[posicionSalon] == "")
    {
        Console.WriteLine("El salon no tiene profesor guia asignado.");
        return;
    }

    int posicionProfesor = BuscarEmpleado(profesoresGuiaSalones[posicionSalon], totalEmpleados);
    if (posicionProfesor == -1) Console.WriteLine("El profesor asignado ya no existe.");
    else MostrarEmpleado(posicionProfesor);
}

int SeleccionarSalon()
{
    string sede = Infraestructura.SeleccionarSede();
    string edificio = Infraestructura.SeleccionarEdificio();

    Console.WriteLine("Salon:");
    for (int i = 1; i <= 6; i++) Console.WriteLine($"{i}. {edificio}-{i}");
    int numero = Validaciones.LeerEntero("Seleccione un salon: ", 1, 6);

    for (int i = 0; i < sedesSalones.Length; i++)
    {
        if (sedesSalones[i] == sede && edificiosSalones[i] == edificio && numerosSalones[i] == numero)
        {
            return i;
        }
    }

    return 0;
}

void QuitarAlumnoDeSalones(string codigoAlumno)
{
    for (int i = 0; i < sedesSalones.Length; i++)
    {
        for (int j = 0; j < cantidadAlumnosSalon[i]; j++)
        {
            if (alumnosPorSalon[i, j].Equals(codigoAlumno, StringComparison.OrdinalIgnoreCase))
            {
                for (int k = j; k < cantidadAlumnosSalon[i] - 1; k++)
                {
                    alumnosPorSalon[i, k] = alumnosPorSalon[i, k + 1];
                }

                alumnosPorSalon[i, cantidadAlumnosSalon[i] - 1] = "";
                cantidadAlumnosSalon[i]--;
                return;
            }
        }
    }
}

void QuitarProfesorDeSalones(string codigoProfesor)
{
    for (int i = 0; i < profesoresGuiaSalones.Length; i++)
    {
        if (profesoresGuiaSalones[i].Equals(codigoProfesor, StringComparison.OrdinalIgnoreCase))
        {
            profesoresGuiaSalones[i] = "";
        }
    }
}

bool ProfesorYaAsignado(string codigoProfesor)
{
    for (int i = 0; i < profesoresGuiaSalones.Length; i++)
    {
        if (profesoresGuiaSalones[i].Equals(codigoProfesor, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }

    return false;
}

int BuscarAlumno(string codigo, int cantidad)
{
    for (int i = 0; i < cantidad; i++)
    {
        if (codigosAlumnos[i].Equals(codigo, StringComparison.OrdinalIgnoreCase)) return i;
    }
    return -1;
}

int BuscarEmpleado(string codigo, int cantidad)
{
    for (int i = 0; i < cantidad; i++)
    {
        if (codigosEmpleados[i].Equals(codigo, StringComparison.OrdinalIgnoreCase)) return i;
    }
    return -1;
}

void MostrarAlumno(int i)
{
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine($"Codigo: {codigosAlumnos[i]}");
    Console.WriteLine($"Nombre: {nombresAlumnos[i]}");
    Console.WriteLine($"Edad: {edadesAlumnos[i]}");
    Console.WriteLine($"DUI: {duisAlumnos[i]}");
    Console.WriteLine($"Telefono: {telefonosAlumnos[i]}");
    Console.WriteLine($"Direccion: {direccionesAlumnos[i]}");
    Console.WriteLine($"Sede: {sedesAlumnos[i]}");
    Console.WriteLine($"Facultad: {facultadesAlumnos[i]}");
    Console.WriteLine($"Modalidad: {modalidadesAlumnos[i]}");
    Console.WriteLine($"Carrera: {carrerasAlumnos[i]}");
    Console.WriteLine(alumnosConSalon[i]
        ? $"Salon: {sedesSalonAlumno[i]} - Edificio {edificiosSalonAlumno[i]} - Salon {edificiosSalonAlumno[i]}-{numerosSalonAlumno[i]}"
        : "Salon: Sin asignar");
}

void MostrarEmpleado(int i)
{
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine($"Codigo: {codigosEmpleados[i]}");
    Console.WriteLine($"Nombre: {nombresEmpleados[i]}");
    Console.WriteLine($"Edad: {edadesEmpleados[i]}");
    Console.WriteLine($"DUI: {duisEmpleados[i]}");
    Console.WriteLine($"Telefono: {telefonosEmpleados[i]}");
    Console.WriteLine($"Direccion: {direccionesEmpleados[i]}");
    Console.WriteLine($"Sede: {sedesEmpleados[i]}");
    Console.WriteLine($"Departamento: {departamentosEmpleados[i]}");
    Console.WriteLine($"Cargo: {cargosEmpleados[i]}");
    Console.WriteLine($"Tipo de empleado: {tiposEmpleados[i]}");
}

void MostrarEmpleadosFiltrados(int cantidad, string sede, string departamento)
{
    bool encontrado = false;

    for (int i = 0; i < cantidad; i++)
    {
        if ((sede != "" && sedesEmpleados[i] == sede) || (departamento != "" && departamentosEmpleados[i] == departamento))
        {
            MostrarEmpleado(i);
            encontrado = true;
        }
    }

    if (!encontrado) Console.WriteLine("No hay empleados registrados con ese filtro.");
}
