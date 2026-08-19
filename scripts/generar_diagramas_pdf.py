from pathlib import Path

from reportlab.lib import colors
from reportlab.lib.pagesizes import letter, landscape
from reportlab.pdfgen import canvas


BASE_DIR = Path(__file__).resolve().parents[1]
OUTPUT_DIR = BASE_DIR / "output" / "pdf"
OUTPUT_FILE = OUTPUT_DIR / "DiagramasSistemaCentralizadoUNICAES.pdf"


def draw_title(pdf, text):
    width, height = landscape(letter)
    pdf.setFillColor(colors.HexColor("#12355B"))
    pdf.rect(0, height - 48, width, 48, fill=1, stroke=0)
    pdf.setFillColor(colors.white)
    pdf.setFont("Helvetica-Bold", 16)
    pdf.drawCentredString(width / 2, height - 31, text)


def draw_entity(pdf, x, y, w, title, fields):
    header_h = 24
    line_h = 14
    h = header_h + (len(fields) * line_h) + 12

    pdf.setStrokeColor(colors.HexColor("#12355B"))
    pdf.setLineWidth(1.2)
    pdf.setFillColor(colors.HexColor("#F4F8FB"))
    pdf.roundRect(x, y - h, w, h, 5, fill=1, stroke=1)

    pdf.setFillColor(colors.HexColor("#12355B"))
    pdf.roundRect(x, y - header_h, w, header_h, 5, fill=1, stroke=0)
    pdf.setFillColor(colors.white)
    pdf.setFont("Helvetica-Bold", 9)
    pdf.drawCentredString(x + w / 2, y - 16, title)

    pdf.setFillColor(colors.black)
    pdf.setFont("Helvetica", 7.5)
    current_y = y - header_h - 13
    for field in fields:
        pdf.drawString(x + 8, current_y, field)
        current_y -= line_h

    return (x, y - h, w, h)


def draw_relation(pdf, x1, y1, x2, y2, label):
    pdf.setStrokeColor(colors.HexColor("#555555"))
    pdf.setFillColor(colors.HexColor("#555555"))
    pdf.setLineWidth(1)
    pdf.line(x1, y1, x2, y2)
    pdf.circle(x2, y2, 2.5, fill=1, stroke=0)

    label_x = (x1 + x2) / 2
    label_y = (y1 + y2) / 2 + 4
    pdf.setFont("Helvetica", 7)
    pdf.setFillColor(colors.HexColor("#333333"))
    pdf.drawCentredString(label_x, label_y, label)


def draw_er_diagram(pdf):
    width, height = landscape(letter)
    draw_title(pdf, "Diagrama Entidad-Relacion - Sistema Centralizado UNICAES")

    sede = draw_entity(
        pdf,
        44,
        515,
        130,
        "SEDE",
        ["id_sede", "nombre", "direccion"],
    )
    alumno = draw_entity(
        pdf,
        250,
        540,
        155,
        "ALUMNO",
        [
            "codigo",
            "nombre_completo",
            "edad",
            "dui",
            "telefono",
            "direccion",
            "id_sede",
            "id_carrera",
            "modalidad",
        ],
    )
    empleado = draw_entity(
        pdf,
        250,
        300,
        155,
        "EMPLEADO",
        [
            "codigo",
            "nombre_completo",
            "edad",
            "dui",
            "telefono",
            "direccion",
            "id_sede",
            "id_cargo",
            "tipo_empleado",
        ],
    )
    facultad = draw_entity(
        pdf,
        615,
        540,
        130,
        "FACULTAD",
        ["id_facultad", "nombre"],
    )
    carrera = draw_entity(
        pdf,
        455,
        500,
        135,
        "CARRERA",
        ["id_carrera", "nombre", "id_facultad"],
    )
    departamento = draw_entity(
        pdf,
        615,
        300,
        130,
        "DEPARTAMENTO",
        ["id_departamento", "nombre"],
    )
    cargo = draw_entity(
        pdf,
        455,
        260,
        135,
        "CARGO",
        ["id_cargo", "nombre", "id_departamento"],
    )

    draw_relation(pdf, sede[0] + sede[2], 475, alumno[0], 470, "1 a muchos")
    draw_relation(pdf, sede[0] + sede[2], 420, empleado[0], 235, "1 a muchos")
    draw_relation(pdf, facultad[0], 500, carrera[0] + carrera[2], 458, "1 a muchas")
    draw_relation(pdf, carrera[0], 445, alumno[0] + alumno[2], 455, "1 a muchos")
    draw_relation(pdf, departamento[0], 260, cargo[0] + cargo[2], 218, "1 a muchos")
    draw_relation(pdf, cargo[0], 205, empleado[0] + empleado[2], 220, "1 a muchos")

    pdf.setFont("Helvetica", 8)
    pdf.setFillColor(colors.HexColor("#333333"))
    pdf.drawString(44, 64, "Sedes principales: Santa Ana e Ilobasco.")
    pdf.drawString(44, 50, "El sistema centraliza informacion basica de alumnos y empleados.")


def draw_actor(pdf, x, y):
    pdf.setStrokeColor(colors.HexColor("#12355B"))
    pdf.setFillColor(colors.white)
    pdf.circle(x, y, 14, fill=0, stroke=1)
    pdf.line(x, y - 14, x, y - 55)
    pdf.line(x - 24, y - 30, x + 24, y - 30)
    pdf.line(x, y - 55, x - 22, y - 88)
    pdf.line(x, y - 55, x + 22, y - 88)
    pdf.setFont("Helvetica-Bold", 10)
    pdf.setFillColor(colors.black)
    pdf.drawCentredString(x, y - 108, "Administrador")
    pdf.drawCentredString(x, y - 121, "del sistema")


def draw_use_case(pdf, x, y, w, h, text):
    pdf.setStrokeColor(colors.HexColor("#12355B"))
    pdf.setFillColor(colors.HexColor("#F4F8FB"))
    pdf.ellipse(x, y, x + w, y + h, fill=1, stroke=1)
    pdf.setFillColor(colors.black)
    pdf.setFont("Helvetica", 8.5)
    pdf.drawCentredString(x + w / 2, y + h / 2 - 3, text)
    return (x, y, w, h)


def draw_use_relation(pdf, actor_x, actor_y, use_case):
    x, y, w, h = use_case
    pdf.setStrokeColor(colors.HexColor("#777777"))
    pdf.setLineWidth(0.8)
    pdf.line(actor_x + 30, actor_y, x, y + h / 2)


def draw_include_relation(pdf, source, target):
    sx, sy, sw, sh = source
    tx, ty, tw, th = target
    pdf.setStrokeColor(colors.HexColor("#777777"))
    pdf.setFillColor(colors.HexColor("#555555"))
    pdf.setLineWidth(0.7)
    pdf.setDash(3, 2)
    pdf.line(sx + sw, sy + sh / 2, tx, ty + th / 2)
    pdf.setDash()


def draw_use_case_diagram(pdf):
    width, height = landscape(letter)
    draw_title(pdf, "Diagrama de Casos de Uso - Sistema Centralizado UNICAES")

    pdf.setStrokeColor(colors.HexColor("#12355B"))
    pdf.setLineWidth(1.2)
    pdf.setFillColor(colors.HexColor("#FFFFFF"))
    pdf.roundRect(190, 72, 555, 448, 8, fill=0, stroke=1)
    pdf.setFillColor(colors.HexColor("#12355B"))
    pdf.setFont("Helvetica-Bold", 11)
    pdf.drawCentredString(468, 503, "Sistema Centralizado UNICAES")

    draw_actor(pdf, 95, 350)

    gestionar_alumnos = draw_use_case(pdf, 230, 365, 130, 40, "Gestionar alumnos")
    gestionar_empleados = draw_use_case(pdf, 230, 225, 130, 40, "Gestionar empleados")

    casos_alumnos = [
        draw_use_case(pdf, 430, 450, 130, 34, "Registrar alumno"),
        draw_use_case(pdf, 600, 450, 130, 34, "Consultar alumno"),
        draw_use_case(pdf, 430, 395, 130, 34, "Modificar alumno"),
        draw_use_case(pdf, 600, 395, 130, 34, "Eliminar alumno"),
        draw_use_case(pdf, 430, 340, 130, 34, "Listar alumnos por sede"),
        draw_use_case(pdf, 600, 340, 130, 34, "Listar alumnos por carrera"),
    ]

    casos_empleados = [
        draw_use_case(pdf, 430, 265, 130, 34, "Registrar empleado"),
        draw_use_case(pdf, 600, 265, 130, 34, "Consultar empleado"),
        draw_use_case(pdf, 430, 210, 130, 34, "Modificar empleado"),
        draw_use_case(pdf, 600, 210, 130, 34, "Eliminar empleado"),
        draw_use_case(pdf, 430, 155, 130, 34, "Listar empleados por sede"),
        draw_use_case(pdf, 600, 155, 130, 34, "Listar empleados por depto."),
    ]

    draw_use_relation(pdf, 125, 320, gestionar_alumnos)
    draw_use_relation(pdf, 125, 320, gestionar_empleados)

    for use_case in casos_alumnos:
        draw_include_relation(pdf, gestionar_alumnos, use_case)

    for use_case in casos_empleados:
        draw_include_relation(pdf, gestionar_empleados, use_case)

    pdf.setFont("Helvetica", 8)
    pdf.setFillColor(colors.HexColor("#333333"))
    pdf.drawString(44, 50, "Actor unico: Administrador del sistema.")


def main():
    OUTPUT_DIR.mkdir(parents=True, exist_ok=True)
    pdf = canvas.Canvas(str(OUTPUT_FILE), pagesize=landscape(letter))

    draw_er_diagram(pdf)
    pdf.showPage()
    draw_use_case_diagram(pdf)
    pdf.save()

    print(OUTPUT_FILE)


if __name__ == "__main__":
    main()
