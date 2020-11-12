using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("project")]
    public class Project
    {
        /*
        public Guid OwnerId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters")]
        public string Address { get; set; }
        public ICollection<Account> Accounts { get; set; }
        */

        public int id {get; set;}
        [Required(ErrorMessage = "Name Is Required !")]
        public string name {get; set;}
        [Required(ErrorMessage = "Company Is Required !")]
        public string company {get; set;}
        public DateTime date_start {get; set;}
        public DateTime date_ent {get; set;}
        public DateTIme created_at {get; set;}


    }
}