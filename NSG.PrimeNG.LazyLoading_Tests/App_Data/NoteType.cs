using System;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//
namespace NSG.PrimeNG.LazyLoading_Tests
{
    [Table("NoteType")]
    public partial class NoteType
    {
        //
        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "NoteTypeId is required.")]
        public int NoteTypeId { get; set; }
        [Required(ErrorMessage = "NoteTypeSortOrder is required.")]
        public int NoteTypeSortOrder { get; set; }
        [Required(ErrorMessage = "NoteTypeShortDesc is required."), MaxLength(8, ErrorMessage = "'NoteTypeShortDesc' must be 8 or less characters.")]
        public string NoteTypeShortDesc { get; set; }
        [Required(ErrorMessage = "NoteTypeDesc is required."), MaxLength(50, ErrorMessage = "'NoteTypeDesc' must be 50 or less characters.")]
        public string NoteTypeDesc { get; set; }
        //
        //
        /// <summary>
        /// Create a 'to string'.
        /// </summary>
        public override string ToString()
        {
            //
            StringBuilder _return = new StringBuilder("record:[");
            _return.AppendFormat("NoteTypeId: {0}, ", NoteTypeId.ToString());
            _return.AppendFormat("NoteTypeSortOrder: {0}, ", NoteTypeSortOrder);
            _return.AppendFormat("NoteTypeShortDesc: {0}, ", NoteTypeShortDesc);
            _return.AppendFormat("NoteTypeDesc: {0}]", NoteTypeDesc);
            return _return.ToString();
        }
    }
}
//
