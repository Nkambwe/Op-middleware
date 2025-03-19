
namespace Operators.Moddleware.Helpers {

    public class DecryptionHandler {

        private readonly IServiceLogger _logger;

        public DecryptionHandler(IServiceLogger logger){ 
            _logger = logger;
            _logger.Channel = $"[SECURE {DateTime.Now:yyyyMMMdddHHmmss}]";
        }

        public List<T> DecryptProperties<T>(List<T> entities, string[] propertiesToDecrypt) where T : class  {
            List<T> result = [];
            result.AddRange(from T e in entities select DecryptProperties(e, propertiesToDecrypt));
            return result;
        } 

        public T DecryptProperties<T>(T entity, string[] propertiesToDecrypt) where T : class {

            if (entity == null || propertiesToDecrypt == null || propertiesToDecrypt.Length == 0)
                return entity;

            var entityType = entity.GetType();
        
            // Create a dictionary of property names (case-insensitive)
            var properties = entityType.GetProperties()
                .Where(p => p.PropertyType == typeof(string) && p.CanRead && p.CanWrite)
                .ToDictionary(p => p.Name.ToLower(), p => p);

            foreach (var propertyName in propertiesToDecrypt) {
                var normalizedPropertyName = propertyName.ToLower();
            
                // Check if the property exists in our dictionary
                if (properties.TryGetValue(normalizedPropertyName, out var propertyInfo)) {
                    // Get the encrypted value
                    var encryptedValue = propertyInfo.GetValue(entity) as string;
                
                    if (!string.IsNullOrEmpty(encryptedValue)) {
                        try {
                            // Decrypt the value
                            var decryptedValue = HashGenerator.DecryptString(encryptedValue);
                        
                            // Set the decrypted value back to the property
                            propertyInfo.SetValue(entity, decryptedValue);
                        } catch (Exception ex) {
                            string msg = $"Failed to decrypt property {propertyInfo.Name}: {ex.Message}";
                            _logger.LogToFile(msg, "ERROR");
                            _logger.LogToFile(ex.StackTrace, "STACKTRACE");
                            throw new Exception(msg);
                        }
                    }
                }
            }

            return entity;
        }
    }
}
