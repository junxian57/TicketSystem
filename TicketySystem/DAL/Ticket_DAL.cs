using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TicketySystem.Models;
using System.Net.Sockets;

namespace TicketySystem.DAL
{
    public class Ticket_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionString"].ToString();

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> ticketList = new List<Ticket>();

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllTicketList";
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sqlData.Fill(dt);
                sqlConnection.Close();
                Guid myGuid = Guid.NewGuid();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Guid guidValue = (Guid)dataRow["ID"];
                    ticketList.Add(new Ticket
                    {
                        Id= guidValue,
                        Priority = (Ticket.TicketPriority)Convert.ToInt32(dataRow["Priority"]),
                        ticketType= (Ticket.TicketType)Convert.ToInt32(dataRow["ticketType"]),
                        Summary = dataRow["Summary"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        IsResolved = (bool)dataRow["isResolved"]
                    });
                }
            }

            return ticketList;
        }

        public List<Ticket> GetTickets(Guid ticketID)
        {
            List<Ticket> ticketList = new List<Ticket>();

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            {
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetTicketList";
                cmd.Parameters.AddWithValue("@id", ticketID);
                SqlDataAdapter sqlData = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sqlData.Fill(dt);
                sqlConnection.Close();
                Guid myGuid = Guid.NewGuid();
                foreach (DataRow dataRow in dt.Rows)
                {
                    Guid guidValue = (Guid)dataRow["ID"];
                    ticketList.Add(new Ticket
                    {
                        Id = guidValue,
                        Priority = (Ticket.TicketPriority)Convert.ToInt32(dataRow["Priority"]),
                        ticketType = (Ticket.TicketType)Convert.ToInt32(dataRow["ticketType"]),
                        Summary = dataRow["Summary"].ToString(),
                        Description = dataRow["Description"].ToString(),
                        IsResolved = (bool)dataRow["isResolved"]
                    });
                }
            }

            return ticketList;
        }

        public bool insertTicket(Ticket ticket)
        {
            Guid newGuid = Guid.NewGuid();
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("insertTicket", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", newGuid);
                sqlCommand.Parameters.AddWithValue("@Description", ticket.Description);
                sqlCommand.Parameters.AddWithValue("@Summary", ticket.Summary);
                sqlCommand.Parameters.AddWithValue("@Priority", ticket.Priority);
                sqlCommand.Parameters.AddWithValue("@ticketType", ticket.ticketType);
                sqlCommand.Parameters.AddWithValue("@IsResolved", ticket.IsResolved);

                connection.Open();
                id = sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            return false;
        }

        public bool updTicketDetail(Ticket ticket)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("updTicketDetail", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", ticket.Id);
                sqlCommand.Parameters.AddWithValue("@Description", ticket.Description);
                sqlCommand.Parameters.AddWithValue("@Summary", ticket.Summary);
                sqlCommand.Parameters.AddWithValue("@Priority", ticket.Priority);
                sqlCommand.Parameters.AddWithValue("@ticketType", ticket.ticketType);

                connection.Open();
                i = sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            return false;
        }

        public string delTicket(Guid ticketID)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("delTicket", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", ticketID);


                connection.Open();
                sqlCommand.ExecuteNonQuery();
                result = sqlCommand.ToString();
                connection.Close();
            }

            return result;
        }

        public bool updTicketResloved(Guid ticketID,Ticket ticket)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand sqlCommand = new SqlCommand("reslovedTicket", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", ticketID);
                sqlCommand.Parameters.AddWithValue("@userID", ticket.RDUserId);
                sqlCommand.Parameters.AddWithValue("@IsResolved", ticket.IsResolved);
                connection.Open();
                i = sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            return false;
        }
    }
}