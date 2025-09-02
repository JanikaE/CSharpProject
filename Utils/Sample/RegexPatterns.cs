namespace Utils.Sample
{
    /// <summary>
    /// 提供常用正则表达式模式
    /// </summary>
    public class RegexPatterns
    {
        /// <summary>
        /// 单个中文字符（CJK统一表意文字）
        /// </summary>
        public const string ChineseCharacter = @"[\u4e00-\u9fa5]";

        /// <summary>
        /// 匹配完全由中文字符组成的字符串
        /// 示例："中文测试" ✓ | "中文test" ✗
        /// </summary>
        public const string ChineseTextOnly = @"^[\u4e00-\u9fa5]+$";

        /// <summary>
        /// 匹配完全由英文字母组成的字符串
        /// 示例："abcXYZ" ✓ | "abc123" ✗
        /// </summary>
        public const string EnglishLettersOnly = @"^[A-Za-z]+$";

        /// <summary>
        /// <![CDATA[ 字符串末尾的逻辑运算符（&& 或 ||），允许前后有空白字符 ]]>
        /// <![CDATA[ 示例：匹配 "&&"、" || " 等 ]]>
        /// </summary>
        public const string TrailingLogicalOperators = @"\s*(&&|\|\|)\s*$";

        /// <summary>
        /// 匹配标准电子邮件地址格式
        /// 示例：user@example.com ✓ | invalid.email ✗
        /// </summary>
        public const string EmailAddress = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";

        /// <summary>
        /// 匹配中国大陆手机号码（11位，以1开头）
        /// 示例：13800138000 ✓ | 12345678901 ✗
        /// </summary>
        public const string ChineseMobileNumber = @"^1[3-9]\d{9}$";

        /// <summary>
        /// 匹配身份证号码（15位或18位，支持最后一位为X）
        /// 示例：11010519491231001X ✓ | 123456789012345 ✗
        /// </summary>
        public const string ChineseIdCard = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";

        /// <summary>
        /// 匹配URL地址（支持HTTP/HTTPS/FTP协议）
        /// 示例：https://www.example.com/path?query=string ✓ | example.com ✗
        /// </summary>
        public const string Url = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";

        /// <summary>
        /// 匹配日期格式（YYYY-MM-DD）
        /// 示例：2023-12-31 ✓ | 2023/12/31 ✗
        /// </summary>
        public const string DateYmd = @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

        /// <summary>
        /// 匹配时间格式（HH:MM:SS，24小时制）
        /// 示例：23:59:59 ✓ | 24:00:00 ✗
        /// </summary>
        public const string TimeHms = @"^([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$";

        /// <summary>
        /// 匹配中国邮政编码（6位数字）
        /// 示例：100000 ✓ | 1000000 ✗
        /// </summary>
        public const string ChinesePostalCode = @"^\d{6}$";

        /// <summary>
        /// 匹配正整数（不包含0）
        /// 示例：123 ✓ | -123 ✗ | 0 ✗
        /// </summary>
        public const string PositiveInteger = @"^[1-9]\d*$";

        /// <summary>
        /// 匹配非负整数（包含0）
        /// 示例：123 ✓ | 0 ✓ | -123 ✗
        /// </summary>
        public const string NonNegativeInteger = @"^\d+$";

        /// <summary>
        /// 匹配浮点数（支持正负号和小数点）
        /// 示例：3.14 ✓ | -0.5 ✓ | +2.0 ✓ | .5 ✗
        /// </summary>
        public const string FloatNumber = @"^[-+]?[0-9]*\.?[0-9]+$";

        /// <summary>
        /// 匹配中英文混合字符串（可包含空格）
        /// 示例："中文abc" ✓ | "中文123" ✓ | "中文!@#" ✗
        /// </summary>
        public const string ChineseEnglishMixed = @"^[\u4e00-\u9fa5a-zA-Z\s]+$";

        /// <summary>
        /// 匹配HTML标签（包括自闭合标签）
        /// <![CDATA[ 示例：<div>content</div> ✓ | <br/> ✓ | <div> ✗ ]]>
        /// </summary>
        public const string HtmlTag = @"<([a-z]+)(?![^>]*\/>)[^>]*>([^<]*(?:<(?!\/\1)[^<]*)*)<\/\1>";

        /// <summary>
        /// 匹配QQ号码（5-11位数字，不以0开头）
        /// 示例：12345 ✓ | 012345 ✗
        /// </summary>
        public const string QQNumber = @"^[1-9]\d{4,10}$";

        /// <summary>
        /// 匹配银行卡号（16-19位数字）
        /// 示例：6222020000000000 ✓ | 12345678901234567 ✓ | 12345 ✗
        /// </summary>
        public const string BankCardNumber = @"^[1-9]\d{15,18}$";

        /// <summary>
        /// 匹配用户名（4-20位，可包含字母、数字、下划线）
        /// 示例：user_123 ✓ | ab ✗
        /// </summary>
        public const string Username = @"^[a-zA-Z0-9_]{4,20}$";

        /// <summary>
        /// 匹配强密码（至少8位，包含大小写字母和数字）
        /// 示例：Password123 ✓ | password ✗
        /// </summary>
        public const string StrongPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        /// <summary>
        /// 匹配IPv4地址格式（0.0.0.0 - 255.255.255.255）
        /// 示例：192.168.1.1 ✓ | 256.0.0.0 ✗
        /// </summary>
        public const string IPv4Address = @"^(?:(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}(?:25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])$";

        /// <summary>
        /// 匹配IPv6地址格式
        /// 示例：2001:0db8:85a3:0000:0000:8a2e:0370:7334 ✓ | 192.168.1.1 ✗
        /// </summary>
        public const string IPv6Address = @"^(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))$";

        /// <summary>
        /// 匹配MAC地址（物理地址）
        /// 示例：00:1A:2B:3C:4D:5E ✓ | 00-1A-2B-3C-4D-5E ✓
        /// </summary>
        public const string MacAddress = @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$";

        /// <summary>
        /// 匹配车牌号码（中国大陆）
        /// 示例：京A12345 ✓ | 粤B1234学 ✓ | 123456 ✗
        /// </summary>
        public const string ChineseLicensePlate = @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-HJ-NP-Z][A-HJ-NP-Z0-9]{4,5}[A-HJ-NP-Z0-9挂学警港澳]$";

        /// <summary>
        /// 匹配微信号（6-20位，可包含字母、数字、减号和下划线）
        /// 示例：username_123 ✓ | ab ✗
        /// </summary>
        public const string WeChatId = @"^[a-zA-Z][-_a-zA-Z0-9]{5,19}$";
    }
}