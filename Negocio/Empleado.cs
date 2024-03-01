using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; }
        public List<object> Empleados { get; set; }
        public Negocio.Departamento Departamento { get; set; }
        public Negocio.Puesto Puesto { get; set; }

        public static Dictionary<string, object> Add(Empleado empleado)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Excepcion", empleado }, { "Resultado", false } };
            try
            {
                using (AccesoDatos.DButronEstructuraEntities context = new AccesoDatos.DButronEstructuraEntities())
                {
                    int filasAfectadas = context.EmpleadoAdd(empleado.Nombre, empleado.Puesto.PuestoID, empleado.Departamento.DepartamentoID);
                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario; 
        }
        public static Dictionary<string, object> GetAll(Empleado empleado)
        {
            //Negocio.Empleado empleado = new Empleado();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Empleado", empleado}, {"Excepcion", excepcion}, {"Resultado", false} };
            try
            {
                using (AccesoDatos.DButronEstructuraEntities context = new AccesoDatos.DButronEstructuraEntities())
                {
                    var listaEmpleados = context.EmpleadoGetAll(empleado.Nombre).ToList();
                    if (listaEmpleados != null)
                    {
                        empleado.Empleados = new List<object>();
                        foreach (var registro in listaEmpleados)
                        {
                            Negocio.Empleado employee = new Empleado();
                            employee.EmpleadoID = registro.EmpleadoID;
                            employee.Nombre = registro.Nombre;
                            employee.Puesto = new Puesto();
                            employee.Puesto.Descripcion = registro.DescripcionPuesto;
                            employee.Departamento = new Departamento();
                            employee.Departamento.Descripcion = registro.DescripcionDepartamento;
                            empleado.Empleados.Add(employee);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Empleado"] = empleado;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> GetByName(string Nombre)
        {
            Empleado empleado = new Empleado();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Empleado", empleado},{"Excepcion", excepcion},{"Resultado", false} };
            try
            {
                using (AccesoDatos.DButronEstructuraEntities context = new AccesoDatos.DButronEstructuraEntities())
                {
                    var objetoEmpleado = context.EmpleadoGetByName(Nombre).FirstOrDefault();
                    if (objetoEmpleado != null)
                    {
                        empleado.Empleados = new List<object>();
                        Empleado employee = new Empleado();
                        employee.EmpleadoID = objetoEmpleado.EmpleadoID;
                        employee.Nombre = objetoEmpleado.Nombre;
                        employee.Puesto = new Puesto();
                        employee.Puesto.Descripcion = objetoEmpleado.DescripcionPuesto;
                        employee.Departamento = new Departamento();
                        employee.Departamento.Descripcion = objetoEmpleado.DescripcionDepartamento;
                        empleado.Empleados.Add(employee);
                        diccionario["Empleado"] = empleado;
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
        public static Dictionary<string, object> Delete(int EmpleadoID)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Excepcion", excepcion},{"Resultado", false} };
            try
            {
                using (SqlConnection context = new SqlConnection(AccesoDatos.SQLConnection.GetConnection()))
                {
                    var query = "EmpleadoDelete";
                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@EmpleadoID", System.Data.SqlDbType.Int);
                    parametros[0].Value = EmpleadoID;

                    SqlCommand command = new SqlCommand(query, context);
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }
            return diccionario;
        }
    }
}
