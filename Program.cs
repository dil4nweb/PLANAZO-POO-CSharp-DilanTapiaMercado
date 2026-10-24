
using Planazo.Modelos;
using Planazo.Servicios;

GestorReservas gestor = new GestorReservas();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("\n==================================");
    Console.WriteLine("       PLANAZO - EVENTOS");
    Console.WriteLine("==================================");
    Console.WriteLine("1. Listar eventos");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Listar reservas");
    Console.WriteLine("5. Cancelar reserva");
    Console.WriteLine("6. Mostrar resumen");
    Console.WriteLine("7. Consultar reservas de un usuario");
    Console.WriteLine("0. Salir");
    Console.Write("Selecciona una opcion: ");

    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");

                foreach (Evento evento in gestor.ObtenerEventos())
                {
                    Console.WriteLine(evento);
                }
                break;

            case "2":
                Console.Write("Ingresa el ID del evento: ");

                if (!int.TryParse(Console.ReadLine(), out int idEvento))
                {
                    Console.WriteLine("El ID debe ser un numero entero.");
                    break;
                }

                Evento? eventoEncontrado = gestor.BuscarEvento(idEvento);

                if (eventoEncontrado == null)
                {
                    Console.WriteLine("No se encontro el evento.");
                }
                else
                {
                    Console.WriteLine("\n--- DETALLE DEL EVENTO ---");
                    Console.WriteLine(eventoEncontrado);
                    Console.WriteLine(
                        $"Precio por entrada: {eventoEncontrado.CalcularPrecioFinal():C}");
                }
                break;

            case "3":
                Console.Write("ID del evento: ");

                if (!int.TryParse(Console.ReadLine(), out int idParaReservar))
                {
                    Console.WriteLine("ID invalido.");
                    break;
                }

                Console.Write("Nombre completo: ");
                string nombre = Console.ReadLine() ?? "";

                Console.Write("Correo electronico: ");
                string correo = Console.ReadLine() ?? "";

                Console.Write("Cantidad de entradas: ");

                if (!int.TryParse(Console.ReadLine(), out int cantidad))
                {
                    Console.WriteLine("La cantidad debe ser un numero entero.");
                    break;
                }

                Reserva nuevaReserva = gestor.CrearReserva(
                    idParaReservar,
                    nombre,
                    correo,
                    cantidad);

                Console.WriteLine("\n¡Reserva realizada correctamente!");
                Console.WriteLine(nuevaReserva);
                break;

            case "4":
                Console.WriteLine("\n--- TODAS LAS RESERVAS ---");

                List<Reserva> reservas = gestor.ObtenerReservas();

                if (reservas.Count == 0)
                {
                    Console.WriteLine("Todavia no hay reservas.");
                }
                else
                {
                    foreach (Reserva reserva in reservas)
                    {
                        Console.WriteLine(reserva);
                    }
                }
                break;

            case "5":
                Console.Write("ID de la reserva que deseas cancelar: ");

                if (!int.TryParse(Console.ReadLine(), out int idReserva))
                {
                    Console.WriteLine("El ID debe ser un numero entero.");
                    break;
                }

                gestor.CancelarReserva(idReserva);
                Console.WriteLine("Reserva cancelada. Los cupos fueron liberados.");
                break;

            case "6":
                Console.WriteLine("\n--- RESUMEN GENERAL ---");
                Console.WriteLine(
                    $"Reservas activas: {gestor.ContarReservasActivas()}");

                Console.WriteLine(
                    $"Entradas reservadas: {gestor.ContarEntradasReservadas()}");

                Console.WriteLine(
                    $"Valor total: {gestor.CalcularValorTotalReservas():C}");
                break;

            case "7":
                Console.Write("Nombre del usuario: ");
                string nombreConsulta = Console.ReadLine() ?? "";

                List<Reserva> reservasUsuario =
                    gestor.ObtenerReservasDeUsuario(nombreConsulta);

                if (reservasUsuario.Count == 0)
                {
                    Console.WriteLine("No se encontraron reservas para ese usuario.");
                }
                else
                {
                    Console.WriteLine("\n--- RESERVAS DEL USUARIO ---");

                    foreach (Reserva reserva in reservasUsuario)
                    {
                        Console.WriteLine(reserva);
                    }
                }
                break;

            case "0":
                continuar = false;
                Console.WriteLine("Gracias por utilizar PLANAZO.");
                break;

            default:
                Console.WriteLine("Opcion no valida. Intenta nuevamente.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Operacion no permitida: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ocurrio un error inesperado: {ex.Message}");
    }
}
