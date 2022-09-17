using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvestorAPI
{
    /// <summary>
    /// Extensions for <see cref="JsonPatchDocument{T}"/>
    /// </summary>
    public static class JsonPatchExtensions
    {

        /// <summary>
        /// Applies JSON patch operations on object and logs errors in <see cref="ModelStateDictionary"/>.
        /// </summary>
        /// <param name="patchDoc">The <see cref="JsonPatchDocument{T}"/>.</param>
        /// <param name="objectToApplyTo">The entity on which <see cref="JsonPatchDocument{T}"/> is applied.</param>
        /// <param name="modelState">The <see cref="ModelStateDictionary"/> to add errors.</param>
        public static void TryApplyTo<T>(this JsonPatchDocument<T> patchDoc, T objectToApplyTo, ModelStateDictionary modelState, IEnumerable<string>? restricted = null) where T : class
        {
            ArgumentNullException.ThrowIfNull(patchDoc);
            ArgumentNullException.ThrowIfNull(objectToApplyTo);
            ArgumentNullException.ThrowIfNull(modelState);

            if (patchDoc.Operations.Any(o => o.op is null || o.path is null))
            {
                modelState.TryAddModelError("JSON Patch", "All operations must have a valid 'op' and 'path' values.");
            }
            else if (patchDoc.Operations.Any(o => o.OperationType is OperationType.Move or OperationType.Copy && o.from is null))
            {
                modelState.TryAddModelError("JSON Patch", "Move and copy operations must have a valid 'from' value.");
            }
            else if (restricted is not null)
            {
                var restrictedSeg = CheckRestrictedSegment(patchDoc.Operations, restricted);
                if (restrictedSeg is not null)
                {
                    modelState.TryAddModelError("JSON Patch", $"The target location specified by path segment '{restrictedSeg}' is immutable.");
                } 
            }

            if (modelState.IsValid)
            {
                patchDoc.ApplyTo(objectToApplyTo, err => modelState.TryAddModelError("JSON Patch", err.ErrorMessage));
            }
        }


        #region Helper Methods

        private static string? CheckRestrictedSegment<T>(List<Operation<T>> operations, IEnumerable<string> restrictions) where T : class
        {
            return operations.FirstOrDefault(o => o.IsRestricted(restrictions))?.GetFirstSegment();
        }

        private static bool IsRestricted<T>(this Operation<T> operation, IEnumerable<string> restrictions) where T : class
        {
            return operation.OperationType != OperationType.Test && IsSegmentRestricted(operation.GetFirstSegment(), restrictions);
        }

        private static bool IsSegmentRestricted(string segment, IEnumerable<string> restrictions)
        {
            return restrictions.Any(x => string.Equals(x, segment, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetFirstSegment<T>(this Operation<T> operation) where T : class
        {
            string[] strings = operation.path.Split('/');

            return operation.path.Split('/')[1];
        }

        #endregion

    }
}
