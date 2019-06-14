namespace eTools.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RentalDetail
    {
        public int RentalDetailID { get; set; }

        public int RentalID { get; set; }

        public int RentalEquipmentID { get; set; }

        public double Days { get; set; }

        [Column(TypeName = "money")]
        public decimal DailyRate { get; set; }

        [Required(ErrorMessage = "ConditionOut is required.")]
        [StringLength(100, ErrorMessage = "ConditionOut cannot be more than 100 characters.")]
        public string ConditionOut { get; set; }

        [Required(ErrorMessage = "ConditionIn is required.")]
        [StringLength(100, ErrorMessage = "ConditionIn cannot be more than 100 characters.")]
        public string ConditionIn { get; set; }

        [Column(TypeName = "money")]
        public decimal DamageRepairCost { get; set; }

        [StringLength(250, ErrorMessage = "Comments cannot be more than 250 characters.")]
        public string Comments { get; set; }

        public bool Paid { get; set; }

        public virtual RentalEquipment RentalEquipment { get; set; }

        public virtual Rental Rental { get; set; }
    }
}
