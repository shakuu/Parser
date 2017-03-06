using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Parser.Common.Constants.ErrorMessages;
using Parser.Common.Constants.Validation;
using Parser.Data.Models.Contracts;

namespace Parser.Data.Models
{
    public class ParserUser : IDbModel
    {
        private ICollection<StoredCombatStatistics> storedCombatStatistics;

        public ParserUser()
        {
            this.storedCombatStatistics = new HashSet<StoredCombatStatistics>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [MinLength(DbModelValidationConstants.NameMinLength, ErrorMessage = DbModelValidationErrorMessages.NameMinLengthErrorMessage)]
        [MaxLength(DbModelValidationConstants.NameMaxLength, ErrorMessage = DbModelValidationErrorMessages.NameMaxLengthErrorMessage)]
        public string Username { get; set; }

        public virtual ICollection<StoredCombatStatistics> StoredCombatStatistics { get { return this.storedCombatStatistics; } set { this.storedCombatStatistics = value; } }
    }
}
