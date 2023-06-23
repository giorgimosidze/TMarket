namespace TMarket.WEB.Helpers.Constants
{
    public static class ModelConstants
    {
        public const string PropertyNotFound = "{PropertyName}ის ველი არ არის შევსებული!";
        public const string MustBeMoreThanZero = "{PropertyName}ის მნიშვნელობა 0-ზე მეტი უნდა იყოს!";
        public const string IsAvailableLogicError = "პროდუქტი ვერ იქნება ხელმისწავდომი, თუკი ხელმისაწვდომი რაოდენობა 0-ის ტოლია.";
        public const string StringLengthError = "{PropertyName}ის ველის სიგრძე ({TotalLength}) არ შეესაბამება სტანდარტს ({MinLength}-{MaxLength})";
        public const string NameRegEx = @"^[ა-ჰA-z]+$";
        public const string InvalidName = "{PropertyName}ი მხოლოდ ასოებს უნდა შეიცავდეს!";
        public const string InvalidQuery = "ერთ-ერთი ველი არასწორადაა შევსებული";
        public const string PropertyNotFoundFromController = "მითითებული {0} ვერ მოიძებნა";
    }
}
