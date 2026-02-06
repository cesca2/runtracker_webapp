using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace RunTrackerApp.Pages;

public class IndexModel : PageModel
{
    
    
    public List<(int Id, string Date, float Distance)> GetAllRecords()
    {
        var rows = new List<(int Id, string Date, float Distance)>();
        
        var connection = new SqliteConnection("Data Source=RunDatabase.db");

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            SELECT id, date, length FROM runs ORDER BY date DESC;
        """;
        command.ExecuteNonQuery();
        SqliteDataReader datareader;
        datareader = command.ExecuteReader();

        while (datareader.Read()){
                
            // Console.WriteLine(datareader.GetString(1));
            // Console.WriteLine(datareader.GetFloat(2));
            rows.Add((datareader.GetInt32(0), datareader.GetString(1), datareader.GetFloat(2)));
            
            }      
        
        connection.Close();
        return rows;
    }

    public void OnGet()
    {
        var connection = new SqliteConnection("Data Source=RunDatabase.db");

        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS runs (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                date TEXT NOT NULL, 
                length REAL NOT NULL
            );

        """;
        command.ExecuteNonQuery();

        connection.Close(); 


    }
    public IActionResult OnPost()
    {
        return Page();
    }
}
