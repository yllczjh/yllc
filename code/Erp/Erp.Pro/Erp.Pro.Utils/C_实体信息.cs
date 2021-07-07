using System.Collections.Generic;
using System.Data;

namespace Erp.Pro.Utils
{
    public static class C_实体信息
    {
        public class C_共享变量
        {
            public static string ServicesAddress { get; set; }
            public static string 系统ID { get; set; }
            public static string 当前系统ID { get; set; }
            public static string 用户ID { get; set; }
        }
        public class C_共享数据集
        {
            private static DataTable _P_样式列表 = null;
            private static DataTable _P_系统信息 = null;
            private static DataTable _P_菜单信息 = null;
            private static DataTable _P_用户信息 = null;
            private static DataTable _P_角色信息 = null;

            public static DataTable P_样式列表 { get; set; }
            public static DataTable P_系统信息
            {
                get
                {
                    if (null != _P_系统信息)
                    {
                        return _P_系统信息;
                    }
                    else
                    {
                        _P_系统信息 = C_通用方法.M_加载共享数据集("xt_xt");
                        return _P_系统信息;
                    }
                }
                set
                {
                    _P_系统信息 = value;
                }
            }
            public static DataTable P_菜单信息
            {
                get
                {
                    if (null != _P_菜单信息)
                    {
                        return _P_菜单信息;
                    }
                    else
                    {
                        _P_菜单信息 = C_通用方法.M_加载共享数据集("xt_mk");
                        return _P_菜单信息;
                    }
                }
                set
                {
                    _P_菜单信息 = value;
                }
            }
            public static DataTable P_用户信息
            {
                get
                {
                    if (null != _P_用户信息)
                    {
                        return _P_用户信息;
                    }
                    else
                    {
                        _P_用户信息 = C_通用方法.M_加载共享数据集("xt_yh");
                        return _P_用户信息;
                    }
                }
                set
                {
                    _P_用户信息 = value;
                }
            }
            public static DataTable P_角色信息
            {
                get
                {
                    if (null != _P_角色信息)
                    {
                        return _P_角色信息;
                    }
                    else
                    {
                        _P_角色信息 = C_通用方法.M_加载共享数据集("xt_js");
                        return _P_角色信息;
                    }
                }
                set
                {
                    _P_角色信息 = value;
                }
            }
            public static void M_刷新数据集(string str_表名)
            {
                switch (str_表名)
                {
                    case "P_菜单信息":
                        _P_菜单信息 = C_通用方法.M_加载共享数据集("xt_mk");
                        break;
                }
            }
        }


        #region 自定义控件相关
        public class C_控件参数
        {
            /// <summary>
            /// 对应数据集中DataTable中的ColumeName
            /// </summary>
            private string _数据名称;
            public string 数据名称
            {
                get { return _数据名称; }
                set
                {
                    _数据名称 = value;
                    if (string.IsNullOrEmpty(显示名称))
                    {
                        显示名称 = value;
                    }
                }
            }


            /// <summary>
            /// Label控件显示的内容
            /// </summary>
            public string 显示名称 { get; set; }
            public E_控件类型 控件类型 { get; set; }
            public C_数据源 数据源 { get; set; }
            public bool 是否显示 { get; set; }
            public bool 是否必填 { get; set; }
            public bool 是否填充 { get; set; }
            public bool 值唯一 { get; set; }
            public bool 只读 { get; set; }
            public object 默认值 { get; set; }
            //用于排序列
            public bool 自增 { get; set; }

            public C_控件参数()
            {
                是否显示 = true;
                是否必填 = false;
                是否填充 = false;
                值唯一 = false;
                只读 = false;
                自增 = false;
            }
        }

        public class C_数据源
        {
            public string ValueMember { get; set; }
            public string DisplayMember { get; set; }
            public DataTable DataSource { get; set; }
            public C_数据源(DataTable DataSource, string ValueMember, string DisplayMember)
            {
                this.ValueMember = ValueMember;
                this.DisplayMember = DisplayMember;
                this.DataSource = DataSource;
            }

            public List<string> list { get; set; }
            public C_数据源(List<string> list)
            {
                this.list = list;
            }
        }
        #endregion
    }
}
