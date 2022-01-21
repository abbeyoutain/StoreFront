using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //added for metadata and validation

namespace StoreFront.DATA.EF//.StoreFrontMetaData
{

    #region MagicItem Metadata
    public class MagicItemMetadata
    {
        //public short MagicItemID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string MagicItemName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(5000, ErrorMessage = "* Value must be 5000 characters or less.")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        public short CategoryID { get; set; }

        public short RarityID { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0, double.MaxValue, ErrorMessage = "* Please enter a valid number.")]
        public short Price { get; set; }

        public short StatusID { get; set; }

        public short MakerID { get; set; }

        [Display(Name = "Magic Item Image")]
        [DisplayFormat(NullDisplayText = "No image.")]
        public string MagicItemImage { get; set; }
    }

    [MetadataType(typeof(MagicItemMetadata))]
    public partial class MagicItem { }
    #endregion

    #region Category Metadata
    public class CategoryMetadata
    {
        //public short CategoryID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Category")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }
    #endregion

    #region Rarity Metadata
    public class RarityMetadata
    {
        //public short RarityID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Rarity")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string RarityLevel { get; set; }
    }

    [MetadataType(typeof(RarityMetadata))]
    public partial class Rarity { }
    #endregion

    #region Status Metadata
    public class StatusMetadata
    {
        //public short StatusID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Status")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string StatusName { get; set; }
    }

    [MetadataType(typeof(StatusMetadata))]
    public partial class Status { }
    #endregion

    #region Maker Metadata
    public class MakerMetadata
    {
        //public short MakerID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(1000, ErrorMessage = "* Value must be 1000 characters or less.")]
        public string Location { get; set; }
    }

    [MetadataType(typeof(MakerMetadata))]
    public partial class Maker
    {
        [Display(Name = "Maker Name")]
        public string MakerName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
    #endregion

    #region Department Metadata
    public class DepartmentMetadata
    {
        [Display(Name = "Department ID")]
        public short DepartmentID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Department")]
        [StringLength(200, ErrorMessage = "* Value must be 200 characters or less.")]
        public string DepartmentName { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }
    #endregion

    #region Employee Metadata
    public class EmployeeMetadata
    {
        //public short EmployeeID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less.")]
        public string LastName { get; set; }

        [Display(Name = "Department ID")]
        public short DepartmentID { get; set; }

        [Display(Name = "Direct Report ID")]
        [DisplayFormat(NullDisplayText = "No direct report ID")]
        public Nullable<short> DirectReportID { get; set; }
    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        [Display(Name = "Employee Name")]
        public string EmployeeName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
    #endregion

}
