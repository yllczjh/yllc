using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace HisDBLayer
{
    /// <summary>
    /// 药品入出库管理
    /// </summary>
    public class Pharmacy
    {
        /// <summary>
        /// 获取剂型字典
        /// </summary>
        /// <returns></returns>
        public List<HisCommon.BringObject> GetPharmacyDosage(bool isShowCompare)
        {
            List<HisCommon.BringObject> dosage = new List<HisCommon.BringObject>();
            string strSQL = string.Empty;
            if (isShowCompare)
            {
                strSQL = @" select * From drug_form_dict t order by t.serial_no ";
            }
            else
            {
                strSQL = @" select *
                          From drug_form_dict t
                         where t.serial_no not in
                               (select m.hoscode from si_compare m where m.type = '2')
                         order by t.serial_no ";
            }

            DbDataReader dr = BaseEntityer.Db.ExecuteReader(strSQL, 0);
            while (dr.Read())
            {
                HisCommon.BringObject obj = new HisCommon.BringObject();
                obj.Id = dr[0].ToString();
                obj.Name = dr[2].ToString();
                obj.Memo = dr[1].ToString();
                dosage.Add(obj);
            }
            if (!dr.IsClosed)
                dr.Close();
            return dosage;
        }

        #region 药品入库

        /// <summary>
        /// 入库主表
        /// </summary>
        /// <returns></returns>
        private int InsertMain(HisCommon.DataEntity.Pharmacy.In.ApplyIn applyIn)
        {
            #region SQL
            string strSQL = @" INSERT into DRUG_IMPORT_MASTER t --
                              (t.DOCUMENT_NO, --入库单号
                               t.STORAGE, --库存单位管理
                               t.IMPORT_DATE, --入库日期
                               t.SUPPLIER, --供货方
                               t.ACCOUNT_RECEIVABLE, --应付款
                               t.ACCOUNT_PAYED, --已付款
                               t.ADDITIONAL_FEE, --附加费
                               t.IMPORT_CLASS, --入库类别
                               t.SUB_STORAGE, --存放库房
                               t.ACCOUNT_INDICATOR, --记账标志
                               t.MEMOS, --备注
                               t.OPERATOR, --录入者
                               t.ACCOUNT_OPERATOR, --
                               t.DOC_DATE, --
                               t.COSTS --
                               )
                            VALUES
                              ('{0}', --入库单号
                               '{1}', --库存单位管理
                               TO_DATE('{2}', 'YYYY-MM-DD HH24:MI:SS'), --入库日期
                               '{3}', --供货方
                               '{4}', --应付款
                               '{5}', --已付款
                               '{6}', --附加费
                               '{7}', --入库类别
                               '{8}', --存放库房
                               '{9}', --记账标志
                               '{10}', --备注
                               '{11}', --录入者
                               '{12}', --
                               TO_DATE('{13}', 'YYYY-MM-DD HH24:MI:SS'), --
                               '{14}' --
                               ) ";
            #endregion
            strSQL = string.Format(strSQL, applyIn.ToString());

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        /// <summary>
        /// 入库明细
        /// </summary>
        /// <returns></returns>
        private int InsertDetail(HisCommon.DataEntity.Pharmacy.In.ApplyIn applyIn)
        {
            #region SQL
            string strSQL = @" INSERT into DRUG_IMPORT_DETAIL t --
                              (t.DOCUMENT_NO, --入库单号
                               t.ITEM_NO, --项目序号
                               t.DRUG_CODE, --药品代码
                               t.DRUG_SPEC, --规格
                               t.UNITS, --单位
                               t.BATCH_NO, --批号
                               t.EXPIRE_DATE, --有效期
                               t.FIRM_ID, --厂家标识
                               t.PURCHASE_PRICE, --进货价
                               t.DISCOUNT, --折扣
                               t.RETAIL_PRICE, --零售价
                               t.PACKAGE_SPEC, --包装价格
                               t.QUANTITY, --数量
                               t.PACKAGE_UNITS, --包装单位
                               t.SUB_PACKAGE_1, --内含包装1
                               t.SUB_PACKAGE_UNITS_1, --内含包装1单位
                               t.SUB_PACKAGE_SPEC_1, --内含包装1规格
                               t.SUB_PACKAGE_2, --内含包装2
                               t.SUB_PACKAGE_UNITS_2, --内含包装2单位
                               t.SUB_PACKAGE_SPEC_2, --内含包装2规格
                               t.INVOICE_NO, --发票号
                               t.INVOICE_DATE, --发票日期
                               t.RECDOCUMENT, --
                               t.RATIFY_NO, --
                               t.IMPORT_DOCUMENT_NO --
                               )
                            VALUES
                              ('{0}', --入库单号
                               '{1}', --项目序号
                               '{2}', --药品代码
                               '{3}', --规格
                               '{4}', --单位
                               '{5}', --批号
                               TO_DATE('{6}', 'YYYY-MM-DD HH24:MI:SS'), --有效期
                               '{7}', --厂家标识
                               '{8}', --进货价
                               '{9}', --折扣
                               '{10}', --零售价
                               '{11}', --包装价格
                               '{12}', --数量
                               '{13}', --包装单位
                               '{14}', --内含包装1
                               '{15}', --内含包装1单位
                               '{16}', --内含包装1规格
                               '{17}', --内含包装2
                               '{18}', --内含包装2单位
                               '{19}', --内含包装2规格
                               '{20}', --发票号
                               TO_DATE('{21}', 'YYYY-MM-DD HH24:MI:SS'), --发票日期
                               '{22}', --
                               '{23}', --
                               '{24}' --
                               ) ";
            #endregion
            strSQL = string.Format(strSQL, applyIn.ToString());

            return BaseEntityer.Db.ZDExecNonQuery(strSQL);
        }

        private int UpdateMain()
        {
            return 1;
        }

        private int UpdateDetail()
        {
            return 1;
        }

        

        #endregion
    }
}
