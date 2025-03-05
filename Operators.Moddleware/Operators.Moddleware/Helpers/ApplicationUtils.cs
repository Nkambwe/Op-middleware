namespace Operators.Moddleware.Helpers {

    public class ApplicationUtils {
        public static bool ISLIVE => false;

        public static List<string> GenerateNameCombinations(string first_Name, string middle_name, string last_name) {
            var combinations = new List<string>();

            // Prepare non-null, trimmed name parts
            var firstName = !string.IsNullOrWhiteSpace(first_Name) ? first_Name.Trim() : null;
            var middleName = !string.IsNullOrWhiteSpace(middle_name) ? middle_name.Trim() : null;
            var lastName = !string.IsNullOrWhiteSpace(last_name) ? last_name.Trim() : null;

            // All possible combinations
            if (firstName != null && middleName != null && lastName != null) {
                combinations.Add($"{firstName} {middleName} {lastName}");
                combinations.Add($"{firstName} {lastName}");
                combinations.Add($"{firstName} {middleName}");
            } else if (firstName != null && middleName != null) {
                combinations.Add($"{firstName} {middleName}");
            } else if (firstName != null && lastName != null) {
                combinations.Add($"{firstName} {lastName}");
            }  else if (middleName != null && lastName != null) {
                combinations.Add($"{middleName} {lastName}");
            }

            // Add individual names if they exist
            if (firstName != null) combinations.Add(firstName);
            if (middleName != null) combinations.Add(middleName);
            if (lastName != null) combinations.Add(lastName);

            return combinations;
        }
    }
}
