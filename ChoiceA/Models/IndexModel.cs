using ChoiceA.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Models
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ChoiceContext _db;

        // на странице увидим протокол чата и поле ввода
        public List<Message> Messages { set; get; }
        [BindProperty]
        [Required]
        public string Text { set; get; }

        public IndexModel(ChoiceContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Messages = _db.Messages.ToList();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Messages.Add(new Message
                {
                    Text = Text,
                    When = DateTime.Now,
                    Sign = User.Identity.Name
                });
                _db.SaveChanges();
            }
            Messages = _db.Messages.ToList();
        }
    }
}
