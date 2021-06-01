﻿using System.Collections.Generic;
using System.Data;

namespace Erp.Pro.Utils
{
    public class C_实体类
    {
    }
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

        public C_控件参数()
        {
            是否显示 = true;
            是否必填 = false;
            是否填充 = false;
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
}
