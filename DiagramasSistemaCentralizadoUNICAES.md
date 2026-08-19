# Diagramas del Sistema Centralizado UNICAES

Sistema para administrar alumnos y empleados de la Universidad Catolica de El Salvador, considerando las sedes de Santa Ana e Ilobasco.

## Diagrama Entidad-Relacion

```mermaid
erDiagram
    SEDE ||--o{ ALUMNO : registra
    SEDE ||--o{ EMPLEADO : registra
    FACULTAD ||--o{ CARRERA : contiene
    CARRERA ||--o{ ALUMNO : pertenece
    DEPARTAMENTO ||--o{ CARGO : contiene
    CARGO ||--o{ EMPLEADO : asigna

    SEDE {
        int id_sede
        string nombre
        string direccion
    }

    ALUMNO {
        string codigo
        string nombre_completo
        int edad
        string dui
        string telefono
        string direccion
        int id_sede
        int id_carrera
        string modalidad
    }

    EMPLEADO {
        string codigo
        string nombre_completo
        int edad
        string dui
        string telefono
        string direccion
        int id_sede
        int id_cargo
        string tipo_empleado
    }

    FACULTAD {
        int id_facultad
        string nombre
    }

    CARRERA {
        int id_carrera
        string nombre
        int id_facultad
    }

    DEPARTAMENTO {
        int id_departamento
        string nombre
    }

    CARGO {
        int id_cargo
        string nombre
        int id_departamento
    }
```

## Diagrama de Casos de Uso

```mermaid
flowchart LR
    Administrador["Administrador del sistema"]

    subgraph Sistema["Sistema Centralizado UNICAES"]
        RegistrarAlumno["Registrar alumno"]
        ConsultarAlumno["Consultar alumno"]
        ModificarAlumno["Modificar alumno"]
        EliminarAlumno["Eliminar alumno"]
        ListarAlumnosSede["Listar alumnos por sede"]
        ListarAlumnosCarrera["Listar alumnos por carrera"]

        RegistrarEmpleado["Registrar empleado"]
        ConsultarEmpleado["Consultar empleado"]
        ModificarEmpleado["Modificar empleado"]
        EliminarEmpleado["Eliminar empleado"]
        ListarEmpleadosSede["Listar empleados por sede"]
        ListarEmpleadosDepartamento["Listar empleados por departamento"]
    end

    Administrador --> RegistrarAlumno
    Administrador --> ConsultarAlumno
    Administrador --> ModificarAlumno
    Administrador --> EliminarAlumno
    Administrador --> ListarAlumnosSede
    Administrador --> ListarAlumnosCarrera

    Administrador --> RegistrarEmpleado
    Administrador --> ConsultarEmpleado
    Administrador --> ModificarEmpleado
    Administrador --> EliminarEmpleado
    Administrador --> ListarEmpleadosSede
    Administrador --> ListarEmpleadosDepartamento
```

## Resumen del sistema

- El administrador del sistema es el unico actor.
- El sistema permite administrar alumnos y empleados.
- Cada alumno pertenece a una sede y a una carrera.
- Cada carrera pertenece a una facultad.
- Cada empleado pertenece a una sede y tiene un cargo.
- Cada cargo pertenece a un departamento.
- Las sedes principales son Santa Ana e Ilobasco.
