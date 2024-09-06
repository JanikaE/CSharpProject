using Newtonsoft.Json;
using Utils.Extend;
using Utils.Tool;

internal class Program
{
    private static void Main()
    {
        string json = "[\r\n  {\r\n    \"Id\": 1272810615152581112,\r\n    \"Code\": \"ECGISSystem\",\r\n    \"Name\": \"心电系统管理员\",\r\n    \"Description\": \"心电系统管理员\",\r\n    \"TenacyId\": 0,\r\n    \"SystemId\": 0,\r\n    \"ServiceCenterId\": 0,\r\n    \"UserId\": 0\r\n  }\r\n]";
        List<AuthorityGroupDetailDTO> list = JsonConvert.DeserializeObject<List<AuthorityGroupDetailDTO>>(json);
        Console.WriteLine(JsonConvert.SerializeObject(list));
        Console.ReadLine();
    }

    public class AuthorityGroupDetailDTO
    {
        /// <summary>
        /// 权限组唯一号
        /// </summary>
        // Token: 0x17001229 RID: 4649
        // (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00017BD4 File Offset: 0x00015DD4
        // (set) Token: 0x06002ADA RID: 10970 RVA: 0x00017BDC File Offset: 0x00015DDC
        public long Id { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        // Token: 0x1700122A RID: 4650
        // (get) Token: 0x06002ADB RID: 10971 RVA: 0x00017BE5 File Offset: 0x00015DE5
        // (set) Token: 0x06002ADC RID: 10972 RVA: 0x00017BED File Offset: 0x00015DED
        public string Name { get; set; }

        /// <summary>
        /// 权限组描述
        /// </summary>
        // Token: 0x1700122B RID: 4651
        // (get) Token: 0x06002ADD RID: 10973 RVA: 0x00017BF6 File Offset: 0x00015DF6
        // (set) Token: 0x06002ADE RID: 10974 RVA: 0x00017BFE File Offset: 0x00015DFE
        public string Description { get; set; }

        /// <summary>
        /// 系统Id
        /// </summary>
        // Token: 0x1700122C RID: 4652
        // (get) Token: 0x06002ADF RID: 10975 RVA: 0x00017C07 File Offset: 0x00015E07
        // (set) Token: 0x06002AE0 RID: 10976 RVA: 0x00017C0F File Offset: 0x00015E0F
        public long SystemId { get; set; }

        /// <summary>
        /// 服务中心id
        /// </summary>
        // Token: 0x1700122D RID: 4653
        // (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00017C18 File Offset: 0x00015E18
        // (set) Token: 0x06002AE2 RID: 10978 RVA: 0x00017C20 File Offset: 0x00015E20
        public long ServiceCenterId { get; set; }

        /// <summary>
        /// 质控中心id
        /// </summary>
        // Token: 0x1700122E RID: 4654
        // (get) Token: 0x06002AE3 RID: 10979 RVA: 0x00017C29 File Offset: 0x00015E29
        // (set) Token: 0x06002AE4 RID: 10980 RVA: 0x00017C31 File Offset: 0x00015E31
        public long QualityCenterId { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        // Token: 0x1700122F RID: 4655
        // (get) Token: 0x06002AE5 RID: 10981 RVA: 0x00017C3A File Offset: 0x00015E3A
        // (set) Token: 0x06002AE6 RID: 10982 RVA: 0x00017C42 File Offset: 0x00015E42
        public long InstitutionId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        // Token: 0x17001230 RID: 4656
        // (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00017C4B File Offset: 0x00015E4B
        // (set) Token: 0x06002AE8 RID: 10984 RVA: 0x00017C53 File Offset: 0x00015E53
        public int? Ordinal { get; set; }

        /// <summary>
        /// 菜单Id集合
        /// </summary>
        // Token: 0x17001231 RID: 4657
        // (get) Token: 0x06002AE9 RID: 10985 RVA: 0x00017C5C File Offset: 0x00015E5C
        // (set) Token: 0x06002AEA RID: 10986 RVA: 0x00017C64 File Offset: 0x00015E64
        public IEnumerable<long> Menus { get; set; }

        /// <summary>
        /// 功能权限集合
        /// </summary>
        // Token: 0x17001232 RID: 4658
        // (get) Token: 0x06002AEB RID: 10987 RVA: 0x00017C6D File Offset: 0x00015E6D
        // (set) Token: 0x06002AEC RID: 10988 RVA: 0x00017C75 File Offset: 0x00015E75
        public IEnumerable<string> FunctionAuthorities { get; set; }

        /// <summary>
        /// 用户组编码
        /// </summary>
        // Token: 0x17001233 RID: 4659
        // (get) Token: 0x06002AED RID: 10989 RVA: 0x00017C7E File Offset: 0x00015E7E
        // (set) Token: 0x06002AEE RID: 10990 RVA: 0x00017C86 File Offset: 0x00015E86
        public string Code { get; set; }
    }
}