using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Empleado
{
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int PuestoID { get; set; }
    public int DepartamentoID { get; set; }

    public Empleado(int id, string nombre, string apellido, DateTime fechaNacimiento, int puestoID, int departamentoID)
    {
        ID = id;
        Nombre = nombre;
        Apellido = apellido;
        FechaNacimiento = fechaNacimiento;
        PuestoID = puestoID;
        DepartamentoID = departamentoID;
    }

    public void ObtenerInformacion()
    {
        Console.WriteLine($"ID: {ID}, Nombre: {Nombre} {Apellido}, Fecha de Nacimiento: {FechaNacimiento.ToShortDateString()}, Puesto ID: {PuestoID}, Departamento ID: {DepartamentoID}");
    }
}

public class EmpleadosDB
{
    private string connectionString;

    public EmpleadosDB(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Empleado> ObtenerEmpleados()
    {
        List<Empleado> empleados = new List<Empleado>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM Empleados";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string nombre = reader["Nombre"].ToString()!;
                    string apellido = reader["Apellido"].ToString()!;
                    DateTime fechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);
                    int puestoID = Convert.ToInt32(reader["PuestoID"]);
                    int departamentoID = Convert.ToInt32(reader["DepartamentoID"]);

                    Empleado empleado = new Empleado(id, nombre, apellido, fechaNacimiento, puestoID, departamentoID);
                    empleados.Add(empleado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        return empleados;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;"; // Aqui es donde tuve problemas, ya que puse algo diferente en la uni. Y cuando lo iba a hacer aqui, puse su bsd y nombre del sv y no funciona

        EmpleadosDB empleadosDB = new EmpleadosDB(connectionString);

        List<Empleado> listaEmpleados = empleadosDB.ObtenerEmpleados();

        foreach (Empleado empleado in listaEmpleados)
        {
            empleado.ObtenerInformacion();
        }

        Console.ReadLine();
    }
}
