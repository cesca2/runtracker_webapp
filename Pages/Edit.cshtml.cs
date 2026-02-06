using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using RunTrackerApp.Models;

namespace RunTrackerApp.Pages
{
    public class EditModel : PageModel
    {
        public List<(int Id, string Date, float Distance)>? RouteIdRecord;

       [ BindProperty]         
        public string RunDate {get; set;}
        [BindProperty] 
        public float Length {get; set;}

        public bool ValidModelEntry = true;

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
            RunModel Run = new RunTrackerApp.Models.RunModel();
            Run.RunDate = RunDate;
            Run.Length = Length;

            // Console.WriteLine(Run.RunDate);
            // Console.WriteLine(Run.Length);

            if (!ModelState.IsValid){
                ValidModelEntry=false;
                // return Page();
            }
            // Console.WriteLine(delete);
            Console.WriteLine(id);
            var connection = new SqliteConnection("Data Source=RunDatabase.db");

            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"""
               UPDATE runs
               SET date='{Run.RunDate:dd/MM/yy}' WHERE id={id};
             """;
            command.ExecuteNonQuery();

            var command2 = connection.CreateCommand();
            command2.CommandText = $"""
               UPDATE runs
               SET length='{Run.Length}' WHERE id={id};
             """;
            command2.ExecuteNonQuery();
            connection.Close();

            return RedirectToPage("/Index");
            

        }
    }
}
