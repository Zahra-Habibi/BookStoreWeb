using BulkyBookRaza.Data;
using BulkyBookRaza.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace BulkyBookRaza.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDBContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.categories.ToList();
        }
    }
}
