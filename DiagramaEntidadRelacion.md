# Diagrama Entidad-Relacion

Sistema Integrador de Gestion Academica Inteligente - UNICAES

```mermaid
erDiagram
    FACULTAD ||--o{ MODALIDAD : ofrece
    MODALIDAD ||--o{ CARRERA : contiene
    CARRERA ||--o{ ESTUDIANTE : inscribe
    ESTUDIANTE ||--o{ NOTA : registra
    ESTUDIANTE ||--|| ASISTENCIA : tiene

    FACULTAD {
        int id_facultad
        string nombre
    }

    MODALIDAD {
        int id_modalidad
        string nombre
        int id_facultad
    }

    CARRERA {
        int id_carrera
        string nombre
        int id_facultad
        int id_modalidad
        int cupo_maximo
    }

    ESTUDIANTE {
        string codigo
        string nombre
        int edad
        string facultad
        string modalidad
        string carrera
        double promedio
        int cantidad_notas
    }

    NOTA {
        int id_nota
        string codigo_estudiante
        double valor
    }

    ASISTENCIA {
        int id_asistencia
        string codigo_estudiante
        double porcentaje
    }
```

## Relaciones

- Una facultad puede ofrecer varias modalidades.
- Una modalidad puede contener varias carreras.
- Una carrera puede inscribir varios estudiantes.
- Cada carrera tiene un cupo maximo de 50 estudiantes.
- Un estudiante puede registrar varias notas.
- Un estudiante tiene un porcentaje de asistencia.
- El promedio academico se calcula usando las notas registradas.
- Un estudiante aprueba por notas si su promedio es mayor o igual a 6.
- Un estudiante aprueba por asistencia si su asistencia es mayor o igual a 75%.
