using System.Data;

namespace Erp.Tools.Tygn
{
    public class C_实体类
    {
    }
    public class C_控件参数
    {
        /// <summary>
        /// 对应数据集中DataTable中的ColumeName
        /// </summary>
        public string 数据名称 { get; set; }
        /// <summary>
        /// Label控件显示的内容
        /// </summary>
        public string 显示名称 { get; set; }
        public E_控件类型 控件类型 { get; set; }
        public C_数据源 数据源 { get; set; }
        public bool 是否显示 { get; set; }
        public bool 是否必填 { get; set; }
        public C_控件参数(string 数据名称, string 显示名称, E_控件类型 控件类型,bool 是否显示, bool 是否必填, C_数据源 数据源)
        {
            this.数据名称 = 数据名称;
            this.显示名称 = 显示名称;
            this.控件类型 = 控件类型;
            this.数据源 = 数据源;
            this.是否显示 = 是否显示;
            this.是否必填 = 是否必填;
        }
        public C_控件参数(string 数据名称, string 显示名称, E_控件类型 控件类型,  bool 是否显示, bool 是否必填)
        {
            this.数据名称 = 数据名称;
            this.显示名称 = 显示名称;
            this.控件类型 = 控件类型;
            this.是否显示 = 是否显示;
            this.是否必填 = 是否必填;
        }
    }

    public class C_数据源
    {
        public string ValueMember { get; set; }
        public string DisplayMember { get; set; }
        public DataTable DataSource { get; set; }
        public C_数据源(DataTable DataSource,string ValueMember, string DisplayMember)
        {
            this.ValueMember = ValueMember;
            this.DisplayMember = DisplayMember;
            this.DataSource = DataSource;
        }
    }
}
