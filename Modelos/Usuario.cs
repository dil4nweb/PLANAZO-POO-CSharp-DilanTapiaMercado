
namespace Planazo.Modelos;

public class Usuario
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Correo { get; private set; }

    public Usuario(int id, string nombre, string correo)
    {
        if (id <= 0)
            throw new ArgumentException("El ID del usuario debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del usuario es obligatorio.");

        if (string.IsNullOrWhiteSpace(correo) ||
            !correo.Contains('@') ||
            !correo.Contains('.'))
        {
            throw new ArgumentException("Debes ingresar un correo valido.");
        }

        Id = id;
        Nombre = nombre.Trim();
        Correo = correo.Trim();
    }

    public override string ToString()
    {
        return $"ID: {Id} | Nombre: {Nombre} | Correo: {Correo}";
    }
}
