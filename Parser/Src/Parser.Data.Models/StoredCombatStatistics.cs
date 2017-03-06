using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Parser.Common.Constants.ErrorMessages;
using Parser.Common.Constants.Validation;
using Parser.Common.Contracts;
using Parser.Data.Models.Contracts;

namespace Parser.Data.Models
{
    public class StoredCombatStatistics : IDbModel, IFinalizedCombatStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [MinLength(DbModelValidationConstants.NameMinLength, ErrorMessage = DbModelValidationErrorMessages.NameMinLengthErrorMessage)]
        [MaxLength(DbModelValidationConstants.NameMaxLength, ErrorMessage = DbModelValidationErrorMessages.NameMaxLengthErrorMessage)]
        public string CharacterName { get; set; }

        public double CombatDurationInSeconds { get; set; }

        public double DamageDone { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double DamageTaken { get; set; }

        public double DamageTakenPerSecond { get; set; }
    }
}
