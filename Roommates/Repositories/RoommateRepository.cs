using Microsoft.Data.SqlClient;
using Roommates.Models;
using System.Collections.Concurrent;

namespace Roommates.Repositories
{
    public class RoommateRepository : BaseRepository
    {
        public RoommateRepository(string connectionString) : base(connectionString) { }

        public Roommate GetById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select rm.FirstName, rm.RentPortion, r.Name as RoomName from Roommate rm join Room r on rm.RoomId = r.Id where r.Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    Roommate roommate = null;
                    Room room = null;

                    if (reader.Read())
                    {
                        room = new Room
                        {
                            Name = reader.GetString(reader.GetOrdinal("RoomName"))
                        };
                        roommate = new Roommate
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            RentPortion = reader.GetInt32(reader.GetOrdinal("RentPortion")),
                            Room = room
                        };
                    }

                    reader.Close();

                    return roommate;
                }
            }
        }
    }
}
