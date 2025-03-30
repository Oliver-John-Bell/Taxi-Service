using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRoupHI
{
    public class TripDatabase
    {
        public string CurrentState; // to hold the current db state
        private static SQLiteConnection _DatabaseConnection; // to hold and establish the connection

        public TripDatabase()
        {
            try
            {
                // Make the connection
                _DatabaseConnection = new SQLiteConnection(DBConnections.DatabasePath, DBConnections.Flags);
                // Create a Table
                _DatabaseConnection.CreateTable<DBTrip>();
                //_DatabaseConnection.DropTable<DBTrip>();
                

            }
            catch (Exception ex)
            {
                CurrentState = ex.Message;
            }
        }

        

        public int DeleteTrip(int tripTId)
        {
            var rowsAffected =  _DatabaseConnection.Delete<DBTrip>(tripTId);
            return rowsAffected;
        }

        public int AddTrip(DBTrip addtrip)
        {
            //addtrip.TId = 0;
            var addtrips = _DatabaseConnection.Insert(addtrip);
            return addtrips;
        }

        public int UpdateTrip(DBTrip updatetrip)
        {
            var updatetrips = _DatabaseConnection.Update(updatetrip);
            return updatetrips;
        }


        public ObservableCollection<DBTrip> GetTrips()
        {
            ObservableCollection<DBTrip> returntrip;

            var allTrips = _DatabaseConnection.Table<DBTrip>().ToList();
            returntrip = new ObservableCollection<DBTrip>(allTrips);
            return returntrip;
        }

        public ObservableCollection<DBTrip> SearchTrips(string searchTerm)
        {
            ObservableCollection<DBTrip> returnTrips;

            var trips = (from trip in _DatabaseConnection.Table<DBTrip>()
                         where trip.Origin.ToLower().Contains(searchTerm.ToLower()) ||
                               trip.Destination.ToLower().Contains(searchTerm.ToLower()) 
                         select trip).ToList();

            returnTrips = new ObservableCollection<DBTrip>(trips);
            return returnTrips;
        }


        public DBTrip GetTripByID(int tid)
        {
            var trip = (from trp in _DatabaseConnection.Table<DBTrip>()
                        where trp.TId == tid
                        select trp).FirstOrDefault();

            return trip;
        }
    }
}
