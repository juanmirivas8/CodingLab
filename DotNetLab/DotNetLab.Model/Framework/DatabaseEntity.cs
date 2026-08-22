namespace DotNetLab.Model.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class DatabaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Required]
    public Guid CreatedBy { get; set; }
    
    [Required]
    public Guid LastModifiedBy { get; set; }
    
    [Required]
    public DateTime CreatedDateTime { get; set; }
    
    [Required]
    public DateTime LastModifiedDateTime { get; set; }
}