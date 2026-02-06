using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic;
using RunTrackerApp.Models;
using Microsoft.Data.Sqlite;

namespace RunTrackerApp.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]         
        public string RunDate {get; set;}
        [BindProperty] 
        public float Length {get; set;}

        public bool ValidModelEntry = true;

        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            RunModel Run = new RunTrackerApp.Models.RunModel();
            Run.RunDate = RunDate;
            Run.Length = Length;

            // Console.WriteLine(Run.RunDate);
            // Console.WriteLine(Run.Length);

            if (!ModelState.IsValid){
                ValidModelEntry=false;
                return Page();
            }

            var connection = new SqliteConnection("Data Source=RunDatabase.db");

            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = $"""
                INSERT INTO runs(date, length) VALUES 
                ('{Run.RunDate:dd/MM/yy}', {Run.Length})
                ;

            """;
            command.ExecuteNonQuery();

            connection.Close();

            return RedirectToPage("./Index");
        }    
    }
}
