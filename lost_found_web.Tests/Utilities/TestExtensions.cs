using System.ComponentModel.DataAnnotations;

namespace lost_found_web.Tests.Utilities;

public static class TestExtensions
{
    public static IList<ValidationResult> ValidateModel(this object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);
        return validationResults;
    }

    public static bool IsValid(this object model)
    {
        return model.ValidateModel().Count == 0;
    }

    public static IEnumerable<string> GetValidationErrors(this object model)
    {
        return model.ValidateModel().Select(vr => vr.ErrorMessage).Where(em => !string.IsNullOrEmpty(em))!;
    }

    public static IEnumerable<string> GetValidationErrorMembers(this object model)
    {
        return model.ValidateModel().SelectMany(vr => vr.MemberNames);
    }
}
