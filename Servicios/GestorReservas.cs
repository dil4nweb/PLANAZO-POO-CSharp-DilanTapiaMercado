
using Planazo.Modelos;

namespace Planazo.Servicios;

public class GestorReservas
{
    private readonly List<Evento> eventos = new();
    private readonly List<Usuario> usuarios = new();
    private readonly List<Reserva> reservas = new();

    private int siguienteIdUsuario = 1;
    private int siguienteIdReserva = 1;

    public GestorReservas()
    {
        eventos.Add(new Cine(
            1, "Estreno de ciencia ficcion",
            DateTime.Today.AddDays(7), 18000m, 80));

        eventos.Add(new Cine(
            2, "Noche de cine clasico",
            DateTime.Today.AddDays(10), 15000m, 60));

        eventos.Add(new Concierto(
            3, "Concierto de rock",
            DateTime.Today.AddDays(15), 80000m, 200));

        eventos.Add(new Concierto(
            4, "Festival de musica",
            DateTime.Today.AddDays(20), 100000m, 300));

        eventos.Add(new Taller(
            5, "Taller de fotografia",
            DateTime.Today.AddDays(5), 50000m, 25));

        eventos.Add(new Taller(
            6, "Taller de programacion",
            DateTime.Today.AddDays(12), 60000m, 30));

        eventos.Add(new EventoDeportivo(
            7, "Torneo de futbol",
            DateTime.Today.AddDays(18), 40000m, 150));
    }

    public List<Evento> ObtenerEventos()
    {
        return eventos;
    }

    public Evento? BuscarEvento(int id)
    {
        return eventos.FirstOrDefault(e => e.Id == id);
    }

    public List<Reserva> ObtenerReservas()
    {
        return reservas;
    }

    public List<Reserva> ObtenerReservasDeUsuario(string nombre)
    {
        return reservas
            .Where(r => r.Usuario.Nombre.Equals(
                nombre.Trim(),
                StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Reserva? BuscarReserva(int id)
    {
        return reservas.FirstOrDefault(r => r.Id == id);
    }

    public Reserva CrearReserva(
        int idEvento,
        string nombreUsuario,
        string correo,
        int cantidadEntradas)
    {
        if (cantidadEntradas <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        Evento? evento = BuscarEvento(idEvento);

        if (evento == null)
            throw new ArgumentException("El evento no existe.");

        // Validar cupos antes de crear la reserva.
        if (cantidadEntradas > evento.CuposDisponibles)
            throw new InvalidOperationException("No hay suficientes cupos.");

        // Reutilizar al usuario si ya existe con el mismo correo.
        Usuario? usuario = usuarios.FirstOrDefault(u =>
            u.Correo.Equals(correo.Trim(), StringComparison.OrdinalIgnoreCase));

        if (usuario == null)
        {
            usuario = new Usuario(
                siguienteIdUsuario++,
                nombreUsuario,
                correo);

            usuarios.Add(usuario);
        }

        // Crear la reserva y descontar los cupos.
        Reserva reserva = new Reserva(
            siguienteIdReserva++,
            usuario,
            evento,
            cantidadEntradas);

        evento.ReservarCupos(cantidadEntradas);
        reservas.Add(reserva);

        return reserva;
    }

    public void CancelarReserva(int idReserva)
    {
        Reserva? reserva = BuscarReserva(idReserva);

        if (reserva == null)
            throw new ArgumentException("No existe una reserva con ese ID.");

        reserva.Cancelar();
    }

    public int ContarReservasActivas()
    {
        return reservas.Count(r => r.Activa);
    }

    public int ContarEntradasReservadas()
    {
        return reservas
            .Where(r => r.Activa)
            .Sum(r => r.CantidadEntradas);
    }

    public decimal CalcularValorTotalReservas()
    {
        return reservas
            .Where(r => r.Activa)
            .Sum(r => r.ValorTotal);
    }
}
