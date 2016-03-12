namespace Tournamentz.DAL.Entity
{
    using Core;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TodoEntries")]
    public class TodoEntry : TrackedEntityBase<ApplicationUser>
    {
        public string Name { get; set; }

        public int Difficulty { get; set; }
    }
}