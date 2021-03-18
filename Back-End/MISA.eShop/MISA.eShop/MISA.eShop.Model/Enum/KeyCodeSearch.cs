
namespace MISA.eShop.Model.Enum
{
    /// <summary>
    /// Lớp enum định nghĩa các key code dùng để tìm kiếm dữ liệu
    /// </summary>
    /// created by vtthien 08.03.2021
    public class KeyCodeSearch
    {
        /// <summary>
        /// Key chứa dữ liệu
        /// </summary>
        public const string CONTRAIN = "*";

        /// <summary>
        /// So sánh bằng
        /// </summary>
        public const string EQUAL = "=";

        /// <summary>
        /// Không chứa
        /// </summary>
        public const string NOT_CONTRAIN = "!";

        /// <summary>
        /// Bắt đầu bởi
        /// </summary>
        public const string START_WITH = "+";

        /// <summary>
        /// Kết thúc bởi
        /// </summary>
        public const string END_WITH = "-";

        /// <summary>
        /// Nhỏ hơn
        /// </summary>
        public const string LESS = "<";

        /// <summary>
        /// Nhỏ hơn hoạc bằng
        /// </summary>
        public const string LESS_EQUAL = "<=";

        /// <summary>
        /// Lớn hơn
        /// </summary>
        public const string MORE = ">";

        /// <summary>
        /// Lớn hơn hoặc bằng
        /// </summary>
        public const string MORE_EQUAL = ">=";
    }
}
