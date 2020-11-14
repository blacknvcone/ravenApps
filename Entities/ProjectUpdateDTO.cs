using System;
namespace Entities
{
    public class ProjectUpdateDTO
    {
        public string name {get; set;}
        public string company {get; set;}
        public int publish {get; set;}
        public DateTime active_date_start {get; set;}
        public DateTime active_date_end {get; set;}
    }
}