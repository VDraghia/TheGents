using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementCollection.Models
{
    public class FavorDoc
    {
        public int FavorDocId { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
