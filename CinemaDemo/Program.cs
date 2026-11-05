using System.Globalization;
using System.Text;
using CinemaDemo.Data;
using CinemaDemo.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Absolute Cinema");

Console.OutputEncoding = Encoding.UTF8;

using CinemaContext db = new CinemaContext();

var sessions = db.Sessions
    .Include(s => s.Movie)
    .Include(s => s.Hall)
    .OrderBy(s => s.StartsAt)
    .ToList();

foreach (var session in sessions)
{
    string price = session.Price.ToString(
        "F2", CultureInfo.InvariantCulture);

    string hallName = session.Hall?.Name
        ?? "დარბაზი მითითებული არ არის";

    Console.WriteLine(
        $"{session.Time} · {session.Movie.Title} · " +
        $"{price} ₾ · {hallName}");
}

//-DataAnnotations-მა Session მოდელში დაამატა [Key], [Column], [ForeignKey] და [InverseProperty]. OnModelCreating-ში დარჩა შეზღუდვების სახელები და წაშლის ქცევის კონფიგურაცია.

//არსებული სახელით დარბაზი წარმატებით დაემატა. Name სვეტზე სახელის უნიკალურობა არ არის უზრუნველყოფილი; ახალ ჩანაწერს განსხვავებული Id მიენიჭა.