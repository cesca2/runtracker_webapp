using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace RunTrackerApp.Pages
{
    public class DeleteModel : PageModel
    {

        public List<(int Id, string Date, float Distance)>? RouteIdRecord;

        public List<(int Id, string Date, float Distance)> GetRecord(int Id)
       {
        var rows = new List<(int Id, string Date, float Distance)>();
        
        var connection = new SqliteConnection("Data Source=RunDatabase.db");

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"""
            SELECT id, date, length FROM runs WHERE id={Id};
        """;
        command.ExecuteNonQuery();
        SqliteDataReader datareader;
        datareader = command.ExecuteReader();

        while (datareader.Read()){
                
            //Console.WriteLine(datareader.GetString(1));
            //Console.WriteLine(datareader.GetFloat(2));
            rows.Add((datareader.GetInt32(0), datareader.GetString(1), datareader.GetFloat(2)));
            
            }      
        
        connection.Close();
        return rows;
        }
        public void OnGet(int id)
        {
            RouteIdRecord = GetRecord(id);
        }

        public IActionResult OnPost(int id)
        {
          
            // Console.WriteLine(delete);
            Console.WriteLine(id);
            var connection = new SqliteConnection("Data Source=RunDatabase.db");

            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"""
               DELETE FROM runs WHERE id={id};
            """;
            command.ExecuteNonQuery();

            return RedirectToPage("/Index");
            

        }
    }
}
