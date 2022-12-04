using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json.Linq;

namespace Investor.Core
{
    /// <summary>
    /// Extensions for <see cref="JsonPatchDocument"/>
    /// </summary>
    public static class JsonPatchExtensions
    {

        /// <summary>
        /// Constructs the underlying JSON patch document from changes made to the processed object.
        /// </summary>
        /// <param name="patchDoc">The <see cref="JsonPatchDocument"/>.</param>
        /// <param name="originalObj">The original object.</param>
        /// <param name="modifiedObj">The modified object.</param>
        public static void Construct<T>(this JsonPatchDocument patchDoc, T originalObj, T modifiedObj) where T : class
        {
            var original = JObject.FromObject(originalObj);
            var modified = JObject.FromObject(modifiedObj);

            patchDoc.Construct(original, modified, "/");
        }


        /// <summary>
        /// Constructs the underlying JSON patch document with differences found in the Json Objects.
        /// </summary>
        /// <param name="patchDoc">The <see cref="JsonPatchDocument"/>.</param>
        /// <param name="original">The original Json object.</param>
        /// <param name="modified">The modified Json object.</param>
        /// <param name="path">The path relative to changes made.</param>
        public static void Construct(this JsonPatchDocument patchDoc, JObject original, JObject modified, string path)
        {
            ArgumentNullException.ThrowIfNull(patchDoc);
            ArgumentNullException.ThrowIfNull(original);
            ArgumentNullException.ThrowIfNull(modified);

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path shouldn't be null or empty.", nameof(path));
            }

            var originalNames = original.Properties().Select(x => x.Name).ToArray();
            var modifiedNames = modified.Properties().Select(x => x.Name).ToArray();

            // Properties removed.
            foreach (var name in originalNames.Except(modifiedNames))
            {
                patchDoc.Remove(path + name);
            }

            // Properties added.
            foreach (var name in modifiedNames.Except(originalNames))
            {
                var prop = modified.Property(name);
                if (prop is not null)
                {
                    patchDoc.Add(path + prop.Name, prop.Value);
                }
            }

            // Properties present in both.
            foreach (var name in originalNames.Intersect(modifiedNames))
            {
                var originalProp = original.Property(name);
                var modifiedProp = modified.Property(name);

                if (originalProp is null || modifiedProp is null)
                {
                    continue;
                }

                if (originalProp.Value.Type != modifiedProp.Value.Type)
                {
                    patchDoc.Replace(path + modifiedProp.Name, modifiedProp.Value);
                    continue;
                }
                if (!string.Equals(originalProp.Value.ToString(Newtonsoft.Json.Formatting.None),
                                   modifiedProp.Value.ToString(Newtonsoft.Json.Formatting.None)))
                {
                    if (originalProp.Value.Type == JTokenType.Object)
                    {
                        // Recurse into objects..
                        patchDoc.Construct((JObject)originalProp.Value, (JObject)modifiedProp.Value, path + modifiedProp.Name + "/");
                        continue;
                    }
                    
                    // Replace values directly.
                    patchDoc.Replace(path + modifiedProp.Name, modifiedProp.Value);
                }
            }

        }

    }
}
