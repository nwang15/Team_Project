namespace eTools.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RentalEquipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RentalEquipment()
        {
            RentalDetails = new HashSet<RentalDetail>();
        }

        public int RentalEquipmentID { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, ErrorMessage = "Description cannot be more than 50 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ModelNumber is required.")]
        [StringLength(15, ErrorMessage = "ModelNumber cannot be more than 15 characters.")]
        public string ModelNumber { get; set; }

        [Required(ErrorMessage = "SerialNumber is required.")]
        [StringLength(20, ErrorMessage = "SerialNumber cannot be more than 20 characters.")]
        public string SerialNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal DailyRate { get; set; }

        [Required(ErrorMessage = "Condition is required.")]
        [StringLength(100, ErrorMessage = "Condition cannot be more than 100 characters.")]
        public string Condition { get; set; }

        public bool Available { get; set; }

        public bool Retired { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentalDetail> RentalDetails { get; set; }
    }
}
