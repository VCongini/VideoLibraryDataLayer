using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSD.VideoLibrary;
using System.Collections.ObjectModel;

namespace VideoLibraryADO
{
    public class ADOImp : IVideoLibraryDAL
    {
        public System.Collections.ObjectModel.Collection<VideoSearchResult> SearchVideoLibrary(VideoSearchCriteria criteria)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Create the VideoSearchResults for return.
            ObservableCollection<VideoSearchResult> result = new ObservableCollection<VideoSearchResult>();

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                {
                    // Create the command and set param values to properties
                    var cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "pr_VideoSearch";
                    cmd.Parameters.AddWithValue("@title", criteria.Title);
                    cmd.Parameters.AddWithValue("@director", criteria.Director);
                    cmd.Parameters.AddWithValue("@year", criteria.Year);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    // Create the inital VideoSearchResults.  Will get converted to the ObservableCollection results.
                    VideoSearchResult testResults;

                    // Clause to make sure there are results
                    if (reader.HasRows)
                    {
                        // Read while there are results
                        while (reader.Read())
                        {
                            // Convert to necessary return
                            testResults = new VideoSearchResult();
                            testResults.Title = Convert.ToString(reader["Title"]);
                            testResults.Director = Convert.ToString(reader["Name"]);
                            testResults.Year = Convert.ToInt32(reader["Year"]);
                            result.Add(testResults);
                        }
                    }
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
            // Return the good results
            return result;
        }

        public void CheckOutVideo(int videoId, Guid userId)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

           // Try/Catch Block.
           try
           {
               // Set the connection
               using (var conn = new SqlConnection(connString))
               using (var cmd = new SqlCommand("pr_CheckOutVideo", conn)
               {
                   CommandType = CommandType.StoredProcedure})
               {
                   // Create the command and set param values to properties
                   cmd.Parameters.AddWithValue("@videoId", videoId);
                   cmd.Parameters.AddWithValue("@userid", userId);

                   // Open the connection and execute.
                   cmd.Connection = conn;
                   conn.Open();
                   var reader = cmd.ExecuteReader();
                   // Access Point
                   reader.Close();
               }
            }
           // Try/Catch Block.
           catch(SqlException ex)
           {
               throw ex;
           }
        }

        public void CheckInVideo(int videoId, Guid userId)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_CheckInVideo", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@videoId", videoId);
                    cmd.Parameters.AddWithValue("@userid", userId);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    // Access Point
                    reader.Close();
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int AddReview(int videoId, Guid userId, string reviewText)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_AddReview", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@videoId", videoId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@reviewText", reviewText);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();
                    // Access Point
                    conn.Close();
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
            return videoId;
        }

        public void UpdateReview(int reviewId, string reviewText)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_UpdateReview", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@reviewId", reviewId);
                    cmd.Parameters.AddWithValue("@reviewText", reviewText);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();
                    // Access Point
                    conn.Close();
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AddUpdateRating(int videoId, Guid userId, int rating)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_AddUpdateRating", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@videoId", videoId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@rating", rating);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();
                    // Access Point
                    conn.Close();
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int AddUpdateVideo(VideoInfo video, Guid userId)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            string formatCode = null;

            // Try/Catch Block.
            try
            {
                // Set the connectio
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_AddUpdateVideo", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@videoId", video.VideoId);
                    cmd.Parameters.AddWithValue("@title", video.Title);
                    cmd.Parameters.AddWithValue("@year", video.Year);
                    cmd.Parameters.AddWithValue("@director", video.Director);
                    cmd.Parameters.AddWithValue("@totalCopies", video.TotalCopies);
                    cmd.Parameters.AddWithValue("@formatCode", formatCode);
                    cmd.Parameters.AddWithValue("@userid", userId);

                    // Parse the enum for videoformat.
                    video.Format = (VideoFormat)Enum.Parse(typeof(VideoFormat), formatCode);



                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();
                    // Access Point
                    conn.Close();
                }
                
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
            // Return the required videoId
            return video.VideoId;
        }

        public void DeleteVideo(int videoId, Guid userId)
        {
            // Set up the connection using App.config file
            var connSettings = ConfigurationManager.ConnectionStrings["VideoLibrary"];
            var connString = connSettings.ConnectionString;

            // Try/Catch Block.
            try
            {
                // Set the connection
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand("pr_DeleteVideo", conn)
                {
                    CommandType = CommandType.StoredProcedure})
                {
                    // Create the command and set param values to properties
                    cmd.Parameters.AddWithValue("@videoId", videoId);
                    cmd.Parameters.AddWithValue("@userid", userId);

                    // Open the connection and execute.
                    cmd.Connection = conn;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();
                    // Access Point
                    conn.Close();
                }
            }
            // Try/Catch Block.
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
