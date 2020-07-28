using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HisCommon.DataEntity;
using HisCommon;
using System.Data.Common;

namespace HisDBLayer
{
    public class MaterialManagement
    {
        public List<HisCommon.DataEntity.OUTP_BILL_ITEMS> QueryOutpBillItem(BaseEntityer db, string deptCode, bool isBack, bool isInp)
        {
            List<OUTP_BILL_ITEMS> outpBillItemList = new List<OUTP_BILL_ITEMS>();
            string sql = string.Empty;
            string outNoStr = string.Empty;
            string numStr = " and amount>0 ";
            if (isBack)
            {
                if (isInp)
                {
                    outNoStr = @"(select b.out_no from inp_bill_detail b where 
 b.patient_id = i.patient_id  
 and b.visit_id = i.visit_id 
 and b.formularyno = i.formularyno 
 and b.performed_by = i.performed_by
 and b.amount>0)";
                }
                else
                {
                    outNoStr = @"(select i.out_no from outp_bill_items i where 
 i.visit_no = o.visit_no and i.item_code = o.item_code 
 and i.item_spec = o.item_spec and i.amount = -o.amount 
 and i.units = o.units and i.serial_no = o.serial_no 
 and i.d_item_no = o.d_item_no 
 and i.performed_by = o.performed_by and i.amount>0)";
                }
                numStr = " and amount<0 ";
            }
            if (isInp)
            {
                sql = @"select
 i.item_class,
 i.item_code,
 i.item_name,
 i.item_spec,
 i.price,
 i.amount,
 i.units,
 i.costs,
 to_char(i.billing_date_time,'yyyy-MM-dd hh24:mi:ss') visit_date,
 {1} out_no,
 '' invoice_new,
 i.rcpt_no,
 i.visit_id visit_no,
 i.item_no,
 patient_id,
 (select e.info_id from EXP_INFO_VS_PRICE e  where e.item_class = i.item_class and e.item_code = i.item_code and e.item_spec = i.item_spec and e.units = i.units and rownum = 1) info_id,
 formularyno  serial_no,
 0 d_item_no,
 (select p.name from pat_master_index p where p.patient_id = i.patient_id and rownum = 1)  patient_name,
 i.performed_by
 from inp_bill_detail i where i.item_class = 'I' 
 and i.out_no is null {2}
 and (i.performed_by = '{0}' or 'ALL' = '{0}')
 order by patient_id ,visit_id ,item_no,billing_date_time";
            }
            else
            {
                sql = @"select 
 o.item_class,
 o.item_code,
 o.item_name,
 o.item_spec,
 o.price,
 o.amount,
 o.units,
 o.costs,
 to_char(o.visit_date,'yyyy-MM-dd hh24:mi:ss') visit_date,
 {1} out_no,
 o.invoice_new,
 o.rcpt_no,
 o.visit_no,
 o.item_no,
 (select r.patient_id from outp_rcpt_master r where r.rcpt_no = o.rcpt_no and rownum = 1) patient_id,
 (select e.info_id from EXP_INFO_VS_PRICE e  where e.item_class = o.item_class and e.item_code = o.item_code and e.item_spec = o.item_spec and e.units = o.units and rownum = 1) info_id,
 serial_no,d_item_no,
 (select r.name from outp_rcpt_master r where r.rcpt_no = o.rcpt_no and rownum = 1)  patient_name,
 o.performed_by
 from outp_bill_items o where o.item_class = 'I'
 and o.out_no is null {2}
 and (o.performed_by = '{0}' or 'ALL' = '{0}')
 order by patient_name,visit_date,visit_no,rcpt_no,item_no";
            }
            sql = string.Format(sql, deptCode, outNoStr, numStr);
            int result = db.ExecQuery(sql);
            if (result == -1)
            {
                return outpBillItemList;
            }
            if (db.Reader != null)
            {
                try
                {
                    while (db.Reader.Read())
                    {
                        OUTP_BILL_ITEMS outpBillItem = new OUTP_BILL_ITEMS();
                        outpBillItem.ITEM_CLASS = db.Reader[0].ToString();
                        outpBillItem.ITEM_CODE = db.Reader[1].ToString();
                        outpBillItem.ITEM_NAME = db.Reader[2].ToString();
                        outpBillItem.ITEM_SPEC = db.Reader[3].ToString();
                        outpBillItem.PRICE = decimal.Parse(db.Reader[4].ToString());
                        outpBillItem.AMOUNT = decimal.Parse(db.Reader[5].ToString());
                        outpBillItem.UNITS = db.Reader[6].ToString();
                        outpBillItem.COSTS = decimal.Parse(db.Reader[7].ToString());
                        outpBillItem.VISIT_DATE = DateTime.Parse(db.Reader[8].ToString());
                        outpBillItem.OUT_NO = db.Reader[9].ToString();
                        outpBillItem.INVOICE_NEW = db.Reader[10].ToString();
                        outpBillItem.RCPT_NO = db.Reader[11].ToString();
                        outpBillItem.VISIT_NO = int.Parse(db.Reader[12].ToString());
                        outpBillItem.ITEM_NO = int.Parse(db.Reader[13].ToString());
                        outpBillItem.PATIENT_ID = db.Reader[14].ToString();
                        outpBillItem.INF_NO = db.Reader[15].ToString();
                        outpBillItem.SERIAL_NO = db.Reader[16].ToString();
                        outpBillItem.D_ITEM_NO = int.Parse(db.Reader[17].ToString());
                        outpBillItem.PATIENT_NAME = db.Reader[18].ToString();
                        outpBillItem.PERFORMED_BY = db.Reader[19].ToString();
                        outpBillItemList.Add(outpBillItem);
                    }
                }
                catch (Exception ex)
                {
                    db.Err = ex.Message;
                    return outpBillItemList;
                }
                finally
                {
                    if (null != db.Reader)
                    {
                        db.Reader.Close();
                    }
                }
            }
            return outpBillItemList;
        }

        public List<DEPT_MAT_STOCKDETAIL> GetDeptMatStock(BaseEntityer db, string deptCode, string infoId)
        {
            List<DEPT_MAT_STOCKDETAIL> deptMatStockList = new List<DEPT_MAT_STOCKDETAIL>();
            string sql = string.Empty;
            if (!infoId.Equals("ALL"))
            {
                sql = @"select 
                            stock_code, 
                            stock_no, 
                            storage_code, 
                            kind_code, 
                            item_code, 
                            item_name, 
                            specs, 
                            batch_no, 
                            place_code, 
                            store_num, 
                            store_cost, 
                            min_unit, 
                            in_no, 
                            in_num, 
                            in_price, 
                            input_date, 
                            sale_price, 
                            sale_cost, 
                            pack_qty, 
                            pack_unit, 
                            pack_price, 
                            factory_code, 
                            company_code, 
                            output_date, 
                            valid_state, 
                            top_num, 
                            low_num, 
                            lack_flag, 
                            memo, 
                            oper_code, 
                            oper_date, 
                            highvalue_flag, 
                            highvalue_barcode,
                            finance_flag
                            from dept_mat_stockdetail where storage_code = '{0}' and item_code in ({1}) 
                            -- and store_num > 0 
                            order by item_code,store_num desc";
                sql = string.Format(sql, deptCode, infoId);
                DataSet ds = db.GetDataSet(sql);
                deptMatStockList = DataSetToEntity.DataSetToT<DEPT_MAT_STOCKDETAIL>(ds).ToList();
            }
            else
            {
                sql = @"SELECT item_code,
                            item_name,
                            specs,
                            batch_no,
                            SUM(store_num) store_num,
                            SUM(store_cost) store_cost,
                            min_unit,
                            in_price,
                            sale_price,
                            pack_qty,
                            pack_unit,
                            sale_cost,
                            stock_code,
                            stock_no,
                            company_code,
                            factory_code,
                            Place_Code,
                            finance_flag, --财务记账标记
                            LACK_FLAG
                        FROM dept_mat_stockdetail
                        WHERE storage_code = '{0}'
                        AND (item_code = '{1}' OR 'ALL' = '{1}')
                      --  AND store_num > 0
                        GROUP BY item_code,
                                item_name,
                                specs,
                                batch_no,
                                min_unit,
                                pack_unit,
                                pack_qty,
                                sale_price,
                                in_price,
                                sale_cost,
                                stock_code,
                                stock_no,
                                company_code,
                                factory_code,
                                Place_Code,
                                finance_flag,
                                LACK_FLAG
                        ORDER BY item_code,
                                store_num DESC
                    ";
                sql = string.Format(sql, deptCode, infoId);
                //int result = db.ExecQuery(sql);
                //if (result == -1)
                //{
                //    return deptMatStockList;
                //}
                db.Reader = db.ExecuteReader(sql);
                if (db.Reader != null)
                {
                    try
                    {
                        while (db.Reader.Read())
                        {
                            DEPT_MAT_STOCKDETAIL outpBillItem = new DEPT_MAT_STOCKDETAIL();
                            outpBillItem.ITEM_CODE = db.Reader[0].ToString();
                            outpBillItem.ITEM_NAME = db.Reader[1].ToString();
                            outpBillItem.SPECS = db.Reader[2].ToString();
                            outpBillItem.BATCH_NO = db.Reader[3].ToString();
                            outpBillItem.STORE_NUM = decimal.Parse(db.Reader[4].ToString());
                            outpBillItem.STORE_COST = decimal.Parse(db.Reader[5].ToString());
                            outpBillItem.MIN_UNIT = db.Reader[6].ToString();
                            outpBillItem.IN_PRICE = decimal.Parse(db.Reader[7].ToString());
                            outpBillItem.SALE_PRICE = decimal.Parse(db.Reader[8].ToString());
                            outpBillItem.PACK_QTY = decimal.Parse(db.Reader[9].ToString());
                            outpBillItem.PACK_UNIT = db.Reader[10].ToString();
                            outpBillItem.SALE_COST = decimal.Parse(db.Reader[11].ToString());// 增加零售金额
                            outpBillItem.STOCK_CODE = db.Reader[12].ToString();// 库存流水号
                            outpBillItem.STOCK_NO = db.Reader[13].ToString();// 库存序号
                            outpBillItem.COMPANY_CODE = db.Reader[14].ToString();// 供货公司
                            outpBillItem.FACTORY_CODE = db.Reader[15].ToString();// 生产厂家
                            outpBillItem.PLACE_CODE = db.Reader[16].ToString();// 生产厂家
                            outpBillItem.FINANCE_FLAG = db.Reader[17].ToString();// 记账标记
                            outpBillItem.LACK_FLAG = db.Reader[18].ToString();// 虚入标记
                            deptMatStockList.Add(outpBillItem);
                        }
                    }
                    catch (Exception ex)
                    {
                        db.Err = ex.Message;
                        return deptMatStockList;
                    }
                    finally
                    {
                        //if (null != db.Reader)
                        if (!db.Reader.IsClosed)
                        {
                            db.Reader.Close();
                        }
                    }
                }
            }
            return deptMatStockList;
        }

        public List<DEPT_MAT_OUTPUT> GetDeptMatOutputInfo(string outNo)
        {
            string sql = @"select 
out_no, 
out_list_code, 
out_serial_no, 
storage_code, 
target_dept, 
stock_code, 
out_class3, 
out_class3mean, 
trans_type, 
out_state, 
item_code, 
item_name, 
kind_code, 
specs, 
batch_no, 
out_num, 
min_unit, 
pack_unit, 
pack_qty, 
out_price, 
out_cost, 
sale_price, 
sale_cost, 
valid_date, 
priv_store_num, 
place_code, 
out_date, 
get_type, 
get_personid, 
recipe_no, 
sequence_no, 
apply_oper, 
apply_date, 
exam_oper, 
exam_date, 
approve_oper, 
approve_date, 
target_in_no, 
finance_flag, 
apply_no, 
return_num, 
return_out_no, 
return_apply_num, 
memo, 
oper_code, 
oper_date, 
highvalue_flag, 
highvalue_barcode, 
receive_person, 
get_visit_no
  from dept_mat_output where out_list_code = '{0}' and trans_type = '1'";
            sql = string.Format(sql, outNo);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_OUTPUT>(ds).ToList();
        }

        public int InsertDeptMatOutput(BaseEntityer db, HisCommon.DataEntity.DEPT_MAT_OUTPUT outputObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into dept_mat_output
                                 (
                                out_no, 
                                out_list_code, 
                                out_serial_no, 
                                storage_code, 
                                target_dept, 
                                stock_code, 
                                out_class3, 
                                out_class3mean, 
                                trans_type, 
                                out_state, 
                                item_code, 
                                item_name, 
                                kind_code, 
                                specs, 
                                batch_no, 
                                out_num, 
                                min_unit, 
                                pack_unit, 
                                pack_qty, 
                                out_price, 
                                out_cost, 
                                sale_price, 
                                sale_cost, 
                                valid_date, 
                                priv_store_num, 
                                place_code, 
                                out_date,
                                get_type, 
                                get_personid, 
                                recipe_no, 
                                sequence_no, 
                                apply_oper, 
                                apply_date,
                                exam_oper, 
                                exam_date,
                                approve_oper, 
                                approve_date,
                                target_in_no, 
                                finance_flag, 
                                apply_no, 
                                return_num, 
                                return_out_no, 
                                return_apply_num, 
                                memo, 
                                oper_code, 
                                oper_date,
                                highvalue_flag, 
                                highvalue_barcode, 
                                receive_person, 
                                get_visit_no,
                                valid_state
                                 )
                                 values
                                 (
                                 '{0}',
                                 '{1}',
                                 '{2}',
                                 '{3}',
                                 '{4}',
                                 '{5}',
                                 '{6}',
                                 '{7}',
                                 '{8}',
                                 '{9}',
                                 '{10}',
                                 '{11}',
                                 '{12}',
                                 '{13}',
                                 '{14}',
                                 '{15}',
                                 '{16}',
                                 '{17}',
                                 '{18}',
                                 '{19}',
                                 '{20}',
                                 '{21}',
                                 '{22}',
                                 to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{24}',
                                 '{25}',
                                 to_date('{26}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{27}',
                                 '{28}',
                                 '{29}',
                                 '{30}',
                                 '{31}',
                                 to_date('{32}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{33}',
                                 to_date('{34}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{35}',
                                 to_date('{36}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{37}',
                                 '{38}',
                                 '{39}',
                                 '{40}',
                                 '{41}',
                                 '{42}',
                                 '{43}',
                                 '{44}',
                                 to_date('{45}', 'yyyy-mm-dd hh24:mi:ss'),
                                 '{46}',
                                 '{47}',
                                 '{48}',
                                 '{49}',
                                 '{50}'
                                 )";
                sql = string.Format(sql, GetDeptMatOutputParam(outputObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetDeptMatOutputParam(HisCommon.DataEntity.DEPT_MAT_OUTPUT outputObj)
        {
            string[] para = new string[]{
                                        outputObj.OUT_NO,
                                        outputObj.OUT_LIST_CODE,
                                        outputObj.OUT_SERIAL_NO.ToString(),
                                        outputObj.STORAGE_CODE,
                                        outputObj.TARGET_DEPT,
                                        outputObj.STOCK_CODE,
                                        outputObj.OUT_CLASS3,
                                        outputObj.OUT_CLASS3MEAN,
                                        outputObj.TRANS_TYPE,
                                        outputObj.OUT_STATE,
                                        outputObj.ITEM_CODE,
                                        outputObj.ITEM_NAME,
                                        outputObj.KIND_CODE,
                                        outputObj.SPECS,
                                        outputObj.BATCH_NO,
                                        outputObj.OUT_NUM.ToString(),
                                        outputObj.MIN_UNIT,
                                        outputObj.PACK_UNIT,
                                        outputObj.PACK_QTY.ToString(),
                                        outputObj.OUT_PRICE.ToString(),
                                        outputObj.OUT_COST.ToString(),
                                        outputObj.SALE_PRICE.ToString(),
                                        outputObj.SALE_COST.ToString(),
                                        outputObj.VALID_DATE.ToString(),
                                        outputObj.PRIV_STORE_NUM.ToString(),
                                        outputObj.PLACE_CODE,
                                        outputObj.OUT_DATE.ToString(),
                                        outputObj.GET_TYPE,
                                        outputObj.GET_PERSONID,
                                        outputObj.RECIPE_NO,
                                        outputObj.SEQUENCE_NO.ToString(),
                                        outputObj.APPLY_OPER,
                                        outputObj.APPLY_DATE.ToString(),
                                        outputObj.EXAM_OPER,
                                        outputObj.EXAM_DATE.ToString(),
                                        outputObj.APPROVE_OPER,
                                        outputObj.APPROVE_DATE.ToString(),
                                        outputObj.TARGET_IN_NO,
                                        outputObj.FINANCE_FLAG,
                                        outputObj.APPLY_NO,
                                        outputObj.RETURN_NUM.ToString(),
                                        outputObj.RETURN_OUT_NO,
                                        outputObj.RETURN_APPLY_NUM.ToString(),
                                        outputObj.MEMO,
                                        outputObj.OPER_CODE,
                                        outputObj.OPER_DATE.ToString(),
                                        outputObj.HIGHVALUE_FLAG,
                                        outputObj.HIGHVALUE_BARCODE,
                                        outputObj.RECEIVE_PERSON,
                                        outputObj.GET_VISIT_NO,
                                        outputObj.Valid_State
            };
            return para;
        }

        public int UpdateDeptMatStock(BaseEntityer db, string stockId, string stockNum, string stockCost, string stockSaleCost)
        {
            int exec = 0;
            try
            {
                if (!stockCost.Equals("No"))
                {
                    string sql = @"UPDATE dept_mat_stockdetail
                                       SET store_num  = store_num - to_number('{1}'),
                                           store_cost = store_cost - to_number('{2}'),
                                           sale_cost  = sale_cost - to_number('{3}')
                                     WHERE stock_code = '{0}'
                                ";
                    sql = string.Format(sql, stockId, stockNum, stockCost, stockSaleCost);
                    exec = db.ExecuteNonQuery(sql);
                }
                else
                {
                    string sql = @"UPDATE dept_mat_stockdetail
                                       SET store_num = store_num - to_number('{1}'),
                                           store_cost = store_cost - (in_price * to_number('{1}')),
                                           sale_cost  = sale_cost - to_number('{2}')
                                     WHERE stock_code = '{0}' ";
                    sql = string.Format(sql, stockId, stockNum, stockSaleCost);
                    exec = db.ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int UpdateOutpItem(BaseEntityer db, string outId, string visitDate, string visitNo, string rcptNo, string itemNo, bool isInp)
        {
            int exec = 0;
            try
            {
                string sql = string.Empty;
                if (isInp)
                {
                    sql = @"update inp_bill_detail set out_no = '{0}'
where patient_id = '{1}' and  visit_id = '{2}' and  item_no = '{3}'";
                    sql = string.Format(sql, outId, rcptNo, visitNo, itemNo);
                }
                else
                {
                    sql = @"update outp_bill_items set out_no = '{0}'
where visit_date  =  to_date('{1}', 'yyyy-mm-dd hh24:mi:ss') and  visit_no = '{2}' and rcpt_no = '{3}' and  item_no = '{4}'";
                    sql = string.Format(sql, outId, visitDate, visitNo, rcptNo, itemNo);
                }
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int InsertDeptMatInput(BaseEntityer db, HisCommon.DataEntity.DEPT_MAT_INPUT inputObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into dept_mat_input
( 
in_no, 
in_list_code, 
in_serial_no, 
storage_code, 
stock_code, 
in_class3, 
in_class3mean, 
trans_type, 
in_state, 
item_code, 
item_name, 
kind_code, 
specs, 
batch_no, 
in_num, 
min_unit, 
pack_in_num, 
pack_unit, 
pack_price, 
pack_qty, 
in_price, 
in_cost, 
sale_price, 
sale_cost, 
priv_store_num, 
place_code, 
in_date,
apply_oper, 
apply_date,
exam_oper, 
exam_date,
approve_oper, 
approve_date,
invoice_no, 
invoice_date,
source_dept, 
source_deptname, 
company_code, 
company_name, 
factory_code, 
out_no, 
return_num, 
return_in_no, 
packin_flag, 
memo, 
oper_code, 
oper_date,
highvalue_flag, 
highvalue_barcode, 
virtual_oper, 
virtual_date,
finance_flag, 
apply_no,
IN_BILL,
IN_ID,
RETAIL_PRICE
)
values
 (
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 '{20}',
 '{21}',
 '{22}',
 '{23}',
 '{24}',
 '{25}',
 to_date('{26}', 'yyyy-mm-dd hh24:mi:ss'),
 '{27}',
 to_date('{28}', 'yyyy-mm-dd hh24:mi:ss'),
 '{29}',
 to_date('{30}', 'yyyy-mm-dd hh24:mi:ss'),
 '{31}',
 to_date('{32}', 'yyyy-mm-dd hh24:mi:ss'),
 '{33}',
 to_date('{34}', 'yyyy-mm-dd hh24:mi:ss'),
 '{35}',
 '{36}',
 '{37}',
 '{38}',
 '{39}',
 '{40}',
 '{41}',
 '{42}',
 '{43}',
 '{44}',
 '{45}',
 to_date('{46}', 'yyyy-mm-dd hh24:mi:ss'),
 '{47}',
 '{48}',
 '{49}',
 to_date('{50}', 'yyyy-mm-dd hh24:mi:ss'),
 '{51}',
 '{52}',
 '{53}',
 '{54}',
'{55}'
 )";
                sql = string.Format(sql, GetDeptMatInputParam(inputObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetDeptMatInputParam(HisCommon.DataEntity.DEPT_MAT_INPUT inputObj)
        {
            string[] para = new string[]{
                                        inputObj.IN_NO,
                                        inputObj.IN_LIST_CODE,
                                        inputObj.IN_SERIAL_NO.ToString(),
                                        inputObj.STORAGE_CODE,
                                        inputObj.STOCK_CODE,
                                        inputObj.IN_CLASS3,
                                        inputObj.IN_CLASS3MEAN,
                                        inputObj.TRANS_TYPE,
                                        inputObj.IN_STATE,
                                        inputObj.ITEM_CODE,
                                        inputObj.ITEM_NAME,
                                        inputObj.KIND_CODE,
                                        inputObj.SPECS,
                                        inputObj.BATCH_NO,
                                        inputObj.IN_NUM.ToString(),
                                        inputObj.MIN_UNIT,
                                        inputObj.PACK_IN_NUM.ToString(),
                                        inputObj.PACK_UNIT,
                                        inputObj.PACK_PRICE.ToString(),
                                        inputObj.PACK_QTY.ToString(),
                                        inputObj.IN_PRICE.ToString(),
                                        inputObj.IN_COST.ToString(),
                                        inputObj.SALE_PRICE.ToString(),
                                        inputObj.SALE_COST.ToString(),
                                        inputObj.PRIV_STORE_NUM.ToString(),
                                        inputObj.PLACE_CODE,
                                        inputObj.IN_DATE.ToString(),
                                        inputObj.APPLY_OPER,
                                        inputObj.APPLY_DATE.ToString(),
                                        inputObj.EXAM_OPER,
                                        inputObj.EXAM_DATE.ToString(),
                                        inputObj.APPROVE_OPER,
                                        inputObj.APPROVE_DATE.ToString(),
                                        inputObj.INVOICE_NO,
                                        inputObj.INVOICE_DATE.ToString(),
                                        inputObj.SOURCE_DEPT,
                                        inputObj.SOURCE_DEPTNAME,
                                        inputObj.COMPANY_CODE,
                                        inputObj.COMPANY_NAME,
                                        inputObj.FACTORY_CODE,
                                        inputObj.OUT_NO,
                                        inputObj.RETURN_NUM.ToString(),
                                        inputObj.RETURN_IN_NO,
                                        inputObj.PACKIN_FLAG,
                                        inputObj.MEMO,
                                        inputObj.OPER_CODE,
                                        inputObj.OPER_DATE.ToString(),
                                        inputObj.HIGHVALUE_FLAG,
                                        inputObj.HIGHVALUE_BARCODE,
                                        inputObj.VIRTUAL_OPER,
                                        inputObj.VIRTUAL_DATE.ToString(),
                                        inputObj.FINANCE_FLAG,
                                        inputObj.APPLY_NO,
                                        inputObj.IN_BILL,
                                        inputObj.IN_ID.ToString(),
                                        inputObj.Retail_Price.ToString()
                                        
            };
            return para;
        }

        public int InsertDeptMatStock(BaseEntityer db, HisCommon.DataEntity.DEPT_MAT_STOCKDETAIL deptMatStockObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into dept_mat_stockdetail
( 
stock_code, 
stock_no, 
storage_code, 
kind_code, 
item_code, 
item_name, 
specs, 
batch_no, 
place_code, 
store_num, 
store_cost, 
min_unit, 
in_no, 
in_num, 
in_price, 
input_date,
sale_price, 
sale_cost, 
pack_qty, 
pack_unit, 
pack_price, 
factory_code, 
company_code, 
output_date,
valid_state, 
top_num, 
low_num, 
lack_flag, 
memo, 
oper_code, 
oper_date,
highvalue_flag, 
highvalue_barcode,
finance_flag
)
values
 (
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 to_date('{15}', 'yyyy-mm-dd hh24:mi:ss'),
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 '{20}',
 '{21}',
 '{22}',
 to_date('{23}', 'yyyy-mm-dd hh24:mi:ss'),
 '{24}',
 '{25}',
 '{26}',
 '{27}',
 '{28}',
 '{29}',
 to_date('{30}', 'yyyy-mm-dd hh24:mi:ss'),
 '{31}',
 '{32}',
'{33}'
 )";
                sql = string.Format(sql, GetDeptMatStockParam(deptMatStockObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        private string[] GetDeptMatStockParam(HisCommon.DataEntity.DEPT_MAT_STOCKDETAIL deptMatStockObj)
        {
            string[] para = new string[]{
                                        deptMatStockObj.STOCK_CODE,
                                        deptMatStockObj.STOCK_NO,
                                        deptMatStockObj.STORAGE_CODE,
                                        deptMatStockObj.KIND_CODE,
                                        deptMatStockObj.ITEM_CODE,
                                        deptMatStockObj.ITEM_NAME,
                                        deptMatStockObj.SPECS,
                                        deptMatStockObj.BATCH_NO,
                                        deptMatStockObj.PLACE_CODE,
                                        deptMatStockObj.STORE_NUM.ToString(),
                                        deptMatStockObj.STORE_COST.ToString(),
                                        deptMatStockObj.MIN_UNIT,
                                        deptMatStockObj.IN_NO,
                                        deptMatStockObj.IN_NUM.ToString(),
                                        deptMatStockObj.IN_PRICE.ToString(),
                                        deptMatStockObj.INPUT_DATE.ToString(),
                                        deptMatStockObj.SALE_PRICE.ToString(),
                                        deptMatStockObj.SALE_COST.ToString(),
                                        deptMatStockObj.PACK_QTY.ToString(),
                                        deptMatStockObj.PACK_UNIT,
                                        deptMatStockObj.PACK_PRICE.ToString(),
                                        deptMatStockObj.FACTORY_CODE,
                                        deptMatStockObj.COMPANY_CODE,
                                        deptMatStockObj.OUTPUT_DATE.ToString(),
                                        deptMatStockObj.VALID_STATE,
                                        deptMatStockObj.TOP_NUM.ToString(),
                                        deptMatStockObj.LOW_NUM.ToString(),
                                        deptMatStockObj.LACK_FLAG,
                                        deptMatStockObj.MEMO,
                                        deptMatStockObj.OPER_CODE,
                                        deptMatStockObj.OPER_DATE.ToString(),
                                        deptMatStockObj.HIGHVALUE_FLAG,
                                        deptMatStockObj.HIGHVALUE_BARCODE,
                                        deptMatStockObj.FINANCE_FLAG
            };
            return para;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<DEPT_MAT_OUTPUT> GetDeptMatOutputList(string deptCode, string beginTime, string endTime)
        {
            string sql = @"SELECT item_code,
                               item_name,
                               specs,
                               batch_no,
                               min_unit,
                               pack_unit,
                               pack_qty,
                               sale_price,
                               out_price,
                               to_char(out_date, 'yyyy-MM-dd') out_date,
                               SUM(out_num) out_num,
                               SUM(out_cost) out_cost,
                               stock_code
                          FROM dept_mat_output
                         WHERE storage_code = '{0}'
                           AND out_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           AND out_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
                           AND valid_state = '1'
                         GROUP BY item_code,
                                  item_name,
                                  specs,
                                  batch_no,
                                  min_unit,
                                  pack_unit,
                                  pack_qty,
                                  sale_price,
                                  out_price,
                                  to_char(out_date, 'yyyy-MM-dd'),
                                  stock_code
                         ORDER BY to_date(out_date, 'yyyy-MM-dd'),
                                  item_code
                        ";
            sql = string.Format(sql, deptCode, beginTime, endTime);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_OUTPUT>(ds).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<DEPT_MAT_INPUT> GetDeptMatInputList(string deptCode, string beginTime, string endTime)
        {
            string sql = @"SELECT item_code,
                               item_name,
                               specs,
                               batch_no,
                               min_unit,
                               pack_unit,
                               pack_qty,
                               sale_price,
                               Retail_Price,
                               to_char(in_date, 'yyyy-MM-dd') in_date,
                               SUM(in_num) in_num,
                               SUM(in_cost) in_cost,
                               stock_code,in_list_code
                          FROM dept_mat_input
                         WHERE storage_code = '{0}'
                           AND in_date >= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')
                           AND in_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')
  
                         GROUP BY item_code,
                                  item_name,
                                  specs,
                                  batch_no,
                                  min_unit,
                                  pack_unit,
                                  pack_qty,
                                  sale_price,
                                  Retail_Price,
                                  to_char(in_date, 'yyyy-MM-dd'),
                                  stock_code,in_list_code
                         ORDER BY to_date(in_date, 'yyyy-MM-dd'),item_code
                         ";
            sql = string.Format(sql, deptCode, beginTime, endTime);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_INPUT>(ds).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public List<VHIS_EXP_OUT_BOOK> GetStockOutputList(string deptCode)
        {
            string sql = @"SELECT store_id,
                               bill_id,
                               receive,
                               out_date,
                               out_sum,
                               book_id,
                               info_id,
                               exp_code,
                               exp_alias,
                               factory,
                               fcountry,
                               exp_country,
                               units,
                               pack,
                               pack_quan,
                               specs,
                               specs_abbr,
                               batch_id,
                               effect,
                               place, --位置信息
                               trade_pric, -- 购入价
                               retail_pric, --调拨价
                               book_quan,
                               (SELECT e.name
                                  FROM exp_stock_name e
                                 WHERE e.store_id = v.store_id
                                   AND rownum = 1) stock_name,
                               (SELECT d.dept_name
                                  FROM dept_dict d
                                 WHERE d.dept_code = v.receive
                                   AND rownum = 1) dept_name,
                               (SELECT i.exp_name
                                  FROM exp_info i
                                 WHERE i.info_id = v.info_id
                                   AND rownum = 1) item_name,
                               v.in_id,
                               v.in_bill,
                               v.retail_pric_true, --真正的零售价
                               v.apply_bill, -- 申请单号
                               v.apply_id,-- 申请单内序号
                               v.is_billpric-- 是否记账
                          FROM vhis_exp_out_book v
                         WHERE receive = '{0}'
                           AND store_id || bill_id || to_char(book_id) NOT IN
                               (SELECT source_dept || in_list_code || to_char(in_serial_no)
                                  FROM dept_mat_input
                                 WHERE storage_code = '{0}')
                             order by v.bill_id desc 
                        ";
            sql = string.Format(sql, deptCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<VHIS_EXP_OUT_BOOK>(ds).ToList();
        }

        public string GetMatInfNo(string itemClass, string itemCode, string spec, string units)
        {
            string sql = @"select to_char(WMSYS.WM_CONCAT(''''||info_id||'''')) from exp_info_vs_price 
where item_class = '{0}' and item_code = '{1}' and item_spec = '{2}' and units = '{3}' 
group by item_class,item_code,item_spec,units";
            sql = string.Format(sql, itemClass, itemCode, spec, units);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public List<HisCommon.DataEntity.his_exp_info> GetExpinfoList(string infoId)
        {
            string sql = @"SELECT info_id,
                               exp_code,
                               exp_name,
                               attr,
                               specs,
                               specs_abbr,
                               units,
                               country,
                               (SELECT t.name FROM exp_factory t WHERE e.factory = t.code) AS factory,
                               trade_pric,
                               retail_pric,
                               (SELECT price
                                  FROM current_price_list b,
                                       exp_info_vs_price  c
                                 WHERE e.info_id = c.info_id
                                   AND c.item_class = b.item_class
                                   AND c.item_code = b.item_code
                                   AND c.item_spec = b.item_spec) AS sale_pric, -- 零售价
                               operator,
                               store_id,
                               is_billpric AS finance_flag
                          FROM exp_info e
                         WHERE info_id = '{0}'
                           AND rownum = 1
                        ";
            sql = string.Format(sql, infoId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.his_exp_info>(ds).ToList();
        }

        public List<HisCommon.DataEntity.his_exp_info> GetExpInfoDetail(string infoId)
        {
            string sql = @"SELECT info_id,
                               exp_code,
                               exp_name,
                               attr,
                               specs,
                               specs_abbr,
                               units,
                               country,
                               (SELECT t.name FROM exp_factory t WHERE e.factory = t.code) AS factory,
                               trade_pric, -- 购入价
                               retail_pric, --调拨价
                               (SELECT price
                                  FROM current_price_list b,
                                       exp_info_vs_price  c
                                 WHERE e.info_id = c.info_id
                                   AND c.item_class = b.item_class
                                   AND c.item_code = b.item_code
                                   AND c.item_spec = b.item_spec) AS sale_pric, -- 零售价
                               (SELECT CASE
                                         WHEN COUNT(*) > 0 THEN
                                          'Y'
                                         ELSE
                                          'N'
                                       END
                                  FROM exp_info_vs_price ep
                                 WHERE ep.info_id = e.info_id) operator,
                               store_id,
                               is_billpric AS FINANCE_FLAG--是否过帐
                          FROM exp_info e
                         WHERE info_id LIKE '{0}%'
                            OR exp_code LIKE '{0}%'
                            OR exp_name LIKE '{0}%'
                        ";
            sql = string.Format(sql, infoId);
            DataSet ds = BaseEntityer.Db.GetDataSet(sql);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.his_exp_info>(ds).ToList();
        }

        public List<HisCommon.DataEntity.MatRegInfo> GetMatRegList(string itemCode)
        {
            string sql = @"select 
reg_code, 
item_code, 
factory_code, 
(select company_name from mat_bs_company where company_code = base.factory_code and rownum =1) factory_name,
specs, 
pack_unit, 
pack_qty, 
pack_price, 
register_code, 
special_type, 
register_date, 
over_date, 
default_flag, 
valid_flag, 
mader, 
memo, 
oper_code, 
oper_date
from mat_bs_basereginfo base where base.item_code = '{0}'";
            sql = string.Format(sql, itemCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.MatRegInfo>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int InsertMatReg(BaseEntityer db, HisCommon.DataEntity.MatRegInfo MatRegObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into mat_bs_basereginfo
( 
REG_CODE,
ITEM_CODE,
FACTORY_CODE,
SPECS,
PACK_UNIT,
PACK_QTY,
PACK_PRICE,
REGISTER_CODE,
SPECIAL_TYPE,
REGISTER_DATE,
OVER_DATE,
DEFAULT_FLAG,
VALID_FLAG,
MEMO,
OPER_CODE,
OPER_DATE
)
values
 (
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 to_date('{9}', 'yyyy-mm-dd hh24:mi:ss'),
 to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 to_date('{15}', 'yyyy-mm-dd hh24:mi:ss')
 )";
                sql = string.Format(sql, GetMatRegObjParam(MatRegObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int UpdateMatReg(BaseEntityer db, HisCommon.DataEntity.MatRegInfo MatRegObj)
        {
            int exec = 0;
            try
            {
                string sql = @"update mat_bs_basereginfo set 
ITEM_CODE = '{1}',
FACTORY_CODE = '{2}',
SPECS = '{3}',
PACK_UNIT = '{4}',
PACK_QTY = '{5}',
PACK_PRICE = '{6}',
REGISTER_CODE = '{7}',
SPECIAL_TYPE = '{8}',
REGISTER_DATE = to_date('{9}', 'yyyy-mm-dd hh24:mi:ss'),
OVER_DATE = to_date('{10}', 'yyyy-mm-dd hh24:mi:ss'),
DEFAULT_FLAG = '{11}',
VALID_FLAG = '{12}',
MEMO = '{13}',
OPER_CODE = '{14}',
OPER_DATE = to_date('{15}', 'yyyy-mm-dd hh24:mi:ss')
where REG_CODE = '{0}'";
                sql = string.Format(sql, GetMatRegObjParam(MatRegObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int DeleteMatReg(BaseEntityer db, string regNo)
        {
            string sql = @"delete from mat_bs_basereginfo reg where reg.reg_code = '{0}'";
            sql = string.Format(sql, regNo);
            return db.ExecuteNonQuery(sql);
        }

        private string[] GetMatRegObjParam(HisCommon.DataEntity.MatRegInfo MatRegObj)
        {
            string[] para = new string[]{
                                        MatRegObj.REG_CODE,
                                        MatRegObj.ITEM_CODE,
                                        MatRegObj.FACTORY_CODE,
                                        MatRegObj.SPECS,
                                        MatRegObj.PACK_UNIT,
                                        MatRegObj.PACK_QTY.ToString(),
                                        MatRegObj.PACK_PRICE.ToString(),
                                        MatRegObj.REGISTER_CODE,
                                        MatRegObj.SPECIAL_TYPE,
                                        MatRegObj.REGISTER_DATE.ToString(),
                                        MatRegObj.OVER_DATE.ToString(),
                                        MatRegObj.DEFAULT_FLAG,
                                        MatRegObj.VALID_FLAG,
                                        MatRegObj.MEMO,
                                        MatRegObj.OPER_CODE,
                                        MatRegObj.OPER_DATE.ToString(),
            };
            return para;
        }

        public List<HisCommon.DataEntity.MatBasicInfo> GetMatBasicList(string deptCode)
        {
            string sql = @"select ITEM_CODE,
                                        KIND_CODE,
                                        (select k.kind_name from mat_bs_kindinfo k where k.kind_code = b.kind_code and rownum = 1) KIND_NAME,
                                        STORAGE_CODE,
                                        '' STORAGE_NAME,
                                        ITEM_NAME,
                                        SPELL_CODE,
                                        WB_CODE,
                                        CUSTOM_CODE,
                                        GB_CODE,
                                        OTHER_NAME,
                                        OTHER_SPELL,
                                        OTHER_WB,
                                        OTHER_CUSTOM,
                                        EFFECT_AREA,
                                        EFFECT_DEPT,
                                        SPECS,
                                        MIN_UNIT,
                                        IN_PRICE,
                                        SALE_PRICE,
                                        PACK_UNIT,
                                        PACK_QTY,
                                        PACK_PRICE,
                                        ADD_RATE,
                                        FEE_CODE,
                                        FINANCE_FLAG,
                                        VALID_FLAG,
                                        SPECIAL_FLAG,
                                        FACTORY_CODE,
                                        '' FACTORY_NAME,
                                        COMPANY_CODE,
                                        '' COMPANY_NAME,
                                        IN_SOURCE,
                                        USAGE,
                                        PACK_FLAG,
                                        NORECYCLE_FLAG,
                                        MEMO,
                                        OPER_CODE,
                                        OPER_DATE,
                                        BATCH_FLAG,
                                        PLAN,
                                        UNDRUG_ITEMCODE
                                        from mat_bs_baseinfo b where b.STORAGE_CODE = '{0}'";
            sql = string.Format(sql, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.MatBasicInfo>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int InsertMatBasic(BaseEntityer db, HisCommon.DataEntity.MatBasicInfo matBasicObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into mat_bs_baseinfo
(ITEM_CODE,
                                        KIND_CODE,
                                        STORAGE_CODE,
                                        ITEM_NAME,
                                        SPELL_CODE,
                                        WB_CODE,
                                        CUSTOM_CODE,
                                        GB_CODE,
                                        OTHER_NAME,
                                        OTHER_SPELL,
                                        OTHER_WB,
                                        OTHER_CUSTOM,
                                        EFFECT_AREA,
                                        EFFECT_DEPT,
                                        SPECS,
                                        MIN_UNIT,
                                        IN_PRICE,
                                        SALE_PRICE,
                                        PACK_UNIT,
                                        PACK_QTY,
                                        PACK_PRICE,
                                        ADD_RATE,
                                        FEE_CODE,
                                        FINANCE_FLAG,
                                        VALID_FLAG,
                                        SPECIAL_FLAG,
                                        FACTORY_CODE,
                                        COMPANY_CODE,
                                        IN_SOURCE,
                                        USAGE,
                                        PACK_FLAG,
                                        NORECYCLE_FLAG,
                                        MEMO,
                                        OPER_CODE,
                                        OPER_DATE,
                                        BATCH_FLAG,
                                        PLAN,
                                        UNDRUG_ITEMCODE)
                                        values
                                        (
                                        '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 '{17}',
 '{18}',
 '{19}',
 '{20}',
 '{21}',
 '{22}',
 '{23}',
 '{24}',
 '{25}',
 '{26}',
 '{27}',
 '{28}',
 '{29}',
 '{30}',
 '{31}',
 '{32}',
 '{33}',
 to_date('{34}', 'yyyy-mm-dd hh24:mi:ss'),
 '{35}',
 '{36}',
 '{37}'
 )";
                sql = string.Format(sql, GetMatBasicObjParam(matBasicObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int UpdateMatBasic(BaseEntityer db, HisCommon.DataEntity.MatBasicInfo matBasicObj)
        {
            int exec = 0;
            try
            {
                string sql = @"update mat_bs_baseinfo set 
KIND_CODE='{1}',
                                        STORAGE_CODE='{2}',
                                        ITEM_NAME='{3}',
                                        SPELL_CODE='{4}',
                                        WB_CODE='{5}',
                                        CUSTOM_CODE='{6}',
                                        GB_CODE='{7}',
                                        OTHER_NAME='{8}',
                                        OTHER_SPELL='{9}',
                                        OTHER_WB='{10}',
                                        OTHER_CUSTOM='{11}',
                                        EFFECT_AREA='{12}',
                                        EFFECT_DEPT='{13}',
                                        SPECS='{14}',
                                        MIN_UNIT='{15}',
                                        IN_PRICE='{16}',
                                        SALE_PRICE='{17}',
                                        PACK_UNIT='{18}',
                                        PACK_QTY='{19}',
                                        PACK_PRICE='{20}',
                                        ADD_RATE='{21}',
                                        FEE_CODE='{22}',
                                        FINANCE_FLAG='{23}',
                                        VALID_FLAG='{24}',
                                        SPECIAL_FLAG='{25}',
                                        FACTORY_CODE='{26}',
                                        COMPANY_CODE='{27}',
                                        IN_SOURCE='{28}',
                                        USAGE='{29}',
                                        PACK_FLAG='{30}',
                                        NORECYCLE_FLAG='{31}',
                                        MEMO='{32}',
                                        OPER_CODE='{33}',
                                        OPER_DATE=to_date('{34}', 'yyyy-mm-dd hh24:mi:ss'),
                                        BATCH_FLAG='{35}',
                                        PLAN='{36}',
                                        UNDRUG_ITEMCODE='{37}'
                                        where ITEM_CODE = '{0}'";
                sql = string.Format(sql, GetMatBasicObjParam(matBasicObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int DeleteMatBasic(BaseEntityer db, string itemCode)
        {
            string sql = @"delete from mat_bs_baseinfo base where base.item_code = '{0}'";
            sql = string.Format(sql, itemCode);
            return db.ExecuteNonQuery(sql);
        }

        private string[] GetMatBasicObjParam(HisCommon.DataEntity.MatBasicInfo matBasicObj)
        {
            string[] para = new string[]{
                                        matBasicObj.ITEM_CODE,
                                        matBasicObj.KIND_CODE,
                                        matBasicObj.STORAGE_CODE,
                                        matBasicObj.ITEM_NAME,
                                        matBasicObj.SPELL_CODE,
                                        matBasicObj.WB_CODE,
                                        matBasicObj.CUSTOM_CODE,
                                        matBasicObj.GB_CODE,
                                        matBasicObj.OTHER_NAME,
                                        matBasicObj.OTHER_SPELL,
                                        matBasicObj.OTHER_WB,
                                        matBasicObj.OTHER_CUSTOM,
                                        matBasicObj.EFFECT_AREA,
                                        matBasicObj.EFFECT_DEPT,
                                        matBasicObj.SPECS,
                                        matBasicObj.MIN_UNIT,
                                        matBasicObj.IN_PRICE.ToString(),
                                        matBasicObj.SALE_PRICE.ToString(),
                                        matBasicObj.PACK_UNIT,
                                        matBasicObj.PACK_QTY.ToString(),
                                        matBasicObj.PACK_PRICE.ToString(),
                                        matBasicObj.ADD_RATE,
                                        matBasicObj.FEE_CODE,
                                        matBasicObj.FINANCE_FLAG,
                                        matBasicObj.VALID_FLAG,
                                        matBasicObj.SPECIAL_FLAG,
                                        matBasicObj.FACTORY_CODE,
                                        matBasicObj.COMPANY_CODE,
                                        matBasicObj.IN_SOURCE,
                                        matBasicObj.USAGE,
                                        matBasicObj.PACK_FLAG,
                                        matBasicObj.NORECYCLE_FLAG,
                                        matBasicObj.MEMO,
                                        matBasicObj.OPER_CODE,
                                        matBasicObj.OPER_DATE.ToString(),
                                        matBasicObj.BATCH_FLAG,
                                        matBasicObj.PLAN,
                                        matBasicObj.UNDRUG_ITEMCODE
            };
            return para;
        }

        public List<HisCommon.DataEntity.MatKind> GetMatKindList(string deptCode)
        {
            string sql = @"select
KIND_CODE,
PRE_CODE,
(select i.kind_name from mat_bs_kindinfo i where i.kind_code = k.PRE_CODE and rownum = 1) PRE_NAME,
STORAGE_CODE,
KIND_NAME,
SPELL_CODE,
WB_CODE,
CUSTOM_CODE,
GB_CODE,
EFFECT_AREA,
EFFECT_DEPT,
BATCH_FLAG,
VALIDDATE_FLAG,
VALID_FLAG,
ACCOUNT_CODE,
ACCOUNT_NAME,
ORDER_NO,
FINANCE_FLAG,
MEMO,
OPER_CODE,
OPER_DATE
from mat_bs_kindinfo k where k.STORAGE_CODE = '{0}'";
            sql = string.Format(sql, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.MatKind>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        public int InsertMatKind(BaseEntityer db, HisCommon.DataEntity.MatKind matKindObj)
        {
            int exec = 0;
            try
            {
                string sql = @"insert into mat_bs_kindinfo
(
KIND_CODE,
PRE_CODE,
STORAGE_CODE,
KIND_NAME,
SPELL_CODE,
WB_CODE,
CUSTOM_CODE,
GB_CODE,
EFFECT_AREA,
EFFECT_DEPT,
BATCH_FLAG,
VALIDDATE_FLAG,
VALID_FLAG,
ACCOUNT_CODE,
ACCOUNT_NAME,
ORDER_NO,
FINANCE_FLAG,
MEMO,
OPER_CODE,
OPER_DATE
)
values
(
 '{0}',
 '{1}',
 '{2}',
 '{3}',
 '{4}',
 '{5}',
 '{6}',
 '{7}',
 '{8}',
 '{9}',
 '{10}',
 '{11}',
 '{12}',
 '{13}',
 '{14}',
 '{15}',
 '{16}',
 '{17}',
 '{18}',
 to_date('{19}', 'yyyy-mm-dd hh24:mi:ss')
)";
                sql = string.Format(sql, GetMatKindObjParam(matKindObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int UpdateMatKind(BaseEntityer db, HisCommon.DataEntity.MatKind matKindObj)
        {
            int exec = 0;
            try
            {
                string sql = @"update mat_bs_kindinfo set 
PRE_CODE='{1}',
STORAGE_CODE='{2}',
KIND_NAME='{3}',
SPELL_CODE='{4}',
WB_CODE='{5}',
CUSTOM_CODE='{6}',
GB_CODE='{7}',
EFFECT_AREA='{8}',
EFFECT_DEPT='{9}',
BATCH_FLAG='{10}',
VALIDDATE_FLAG='{11}',
VALID_FLAG='{12}',
ACCOUNT_CODE='{13}',
ACCOUNT_NAME='{14}',
ORDER_NO='{15}',
FINANCE_FLAG='{16}',
MEMO='{17}',
OPER_CODE='{18}',
OPER_DATE=to_date('{19}', 'yyyy-mm-dd hh24:mi:ss')
where KIND_CODE = '{0}'";
                sql = string.Format(sql, GetMatKindObjParam(matKindObj));
                exec = db.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                db.Err = ex.Message;
                exec = -1;
            }
            return exec;
        }

        public int DeleteMatKind(BaseEntityer db, string kindCode)
        {
            string sql = @"delete from mat_bs_kindinfo k where k.kind_code = '{0}'";
            sql = string.Format(sql, kindCode);
            return db.ExecuteNonQuery(sql);
        }

        private string[] GetMatKindObjParam(HisCommon.DataEntity.MatKind matKindObj)
        {
            string[] para = new string[]{
                                        matKindObj.KIND_CODE,
                                        matKindObj.PRE_CODE,
                                        matKindObj.STORAGE_CODE,
                                        matKindObj.KIND_NAME,
                                        matKindObj.SPELL_CODE,
                                        matKindObj.WB_CODE,
                                        matKindObj.CUSTOM_CODE,
                                        matKindObj.GB_CODE,
                                        matKindObj.EFFECT_AREA,
                                        matKindObj.EFFECT_DEPT,
                                        matKindObj.BATCH_FLAG,
                                        matKindObj.VALIDDATE_FLAG,
                                        matKindObj.VALID_FLAG,
                                        matKindObj.ACCOUNT_CODE,
                                        matKindObj.ACCOUNT_NAME,
                                        matKindObj.ORDER_NO.ToString(),
                                        matKindObj.FINANCE_FLAG,
                                        matKindObj.MEMO,
                                        matKindObj.OPER_CODE,
                                        matKindObj.OPER_DATE.ToString()
            };
            return para;
        }

        public int GetKindNum(string kindCode)
        {
            string sql = @"select count(k.kind_code) from mat_bs_kindinfo k where k.pre_code = '{0}'";
            sql = string.Format(sql, kindCode);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
        }

        public int GetItemNum(string kindCode)
        {
            string sql = @"select count(b.item_code) from mat_bs_baseinfo b where b.kind_code = '{0}'";
            sql = string.Format(sql, kindCode);
            object obj = BaseEntityer.Db.ExecuteScalar(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            else
            {
                return 0;
            }
        }

        #region 物资申请相关业务处理方法 by dong_w 2014年9月10日

        /// <summary>
        /// 插入物资入库记录
        /// </summary>
        /// <param name="matApplyRecord"></param>
        /// <param name="dby"></param>
        /// <returns></returns>
        public int InsertMatRecordInfo(MatApplyRecord matApplyRecord, BaseEntityer dby)
        {
            string sql = @"INSERT INTO mat_com_apply
                            (apply_no,--0
                            apply_list_code,--1
                            apply_serial_no,--2
                            storage_code,--3
                            target_dept,--4
                            class2_priv,---5
                            apply_state,--6
                            item_code,--7
                            item_name,--8
                            kind_code,--9
                            specs,--10
                            min_unit,--11
                            pack_unit,--12
                            pack_qty,--13
                            apply_num,--14
                            apply_price,--15
                            apply_cost,--16
                            sale_price,--17
                            sale_cost,--18
                            apply_oper,--19
                            apply_date,--20
                            exam_oper,--21
                            exam_date,--22
                            approve_oper,--23
                            approve_date,--24
                            abolish_oper,--25
                            abolish_date,--26
                            exam_num,--27
                            company_code,--28
                            company_name,--29
                            store_sum,--30
                            store_totsum,--31
                            valid_state,--32
                            out_list_code,--33
                            in_list_code,--34
                            memo,--35
                            oper_code,--36
                            oper_date,--37
                            apply_check,--38
                            check_oper,--39
                            check_date,--40
                            apply_type,--41
                            is_applyexam,--42
                            apply_examoper,--43
                            apply_examdate,--44
                            apply_examnum)--45
                            VALUES
                            ('{0}',
                            '{1}',
                            '{2}',
                            '{3}',
                            '{4}',
                            '{5}',
                            '{6}',
                            '{7}',
                            '{8}',
                            '{9}',
                            '{10}',
                            '{11}',
                            '{12}',
                            '{13}',
                            '{14}',
                            '{15}',
                            '{16}',
                            '{17}',
                            '{18}',
                            '{19}',
                            to_date( '{20}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{21}',
                          to_date('{22}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{23}',
                           to_date( '{24}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{25}',
                             to_date( '{26}', 'yyyy-mm-dd hh24:mi:ss'), 
                            {27},
                            '{28}',
                            '{29}',
                            '{30}',
                            '{31}',
                            '{32}',
                            '{33}',
                            '{34}',
                            '{35}',
                            '{36}',
                             to_date( '{37}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{38}',
                            '{39}',
                             to_date( '{40}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{41}',
                            '{42}',
                            '{43}',
                             to_date( '{44}', 'yyyy-mm-dd hh24:mi:ss'),
                            '{45}')";
            sql = string.Format(sql, GetMatApplyRecordParam(matApplyRecord));
            return dby.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据申请单号查询物资记录
        /// </summary>
        /// <param name="applyLstCode">申请单号</param>
        /// <returns></returns>
        public IList<MatApplyRecord> QueryMatApplyRecord(string applyLstCode)
        {
            string sql = @"SELECT
                        t.APPLY_EXAMNUM,   --内部入库申请审批数量
                        t.APPLY_EXAMDATE,   --内部入库申请审批时间
                        t.APPLY_EXAMOPER,   --内部入库申请审批人
                        t.IS_APPLYEXAM,   --是否需要申请审批（0-否,1-是）
                        t.APPLY_TYPE,   --申请类型(0-常规,1-临时,2-紧急)
                        t.CHECK_DATE,   --审核时间
                        t.CHECK_OPER,   --审核人
                        t.APPLY_CHECK,   --申请科室审核(1-审核,0-未审核)
                        t.OPER_DATE,   --操作日期
                        t.OPER_CODE,   --操作员
                        t.MEMO,   --备注
                        t.IN_LIST_CODE,   --入库单号
                        t.OUT_LIST_CODE,   --出库单号
                        t.VALID_STATE,   --有效性状态(1-有效,0-无效)
                        t.STORE_TOTSUM,   --全院库存总和
                        t.STORE_SUM,   --申请科室库存
                        t.COMPANY_NAME,   --公司名称
                        t.COMPANY_CODE,   --公司编码
                        t.EXAM_NUM,   --审批数量
                        t.ABOLISH_DATE,   --作废时间
                        t.ABOLISH_OPER,   --作废人编码
                        t.APPROVE_DATE,   --核准时间
                        t.APPROVE_OPER,   --核准人
                        t.EXAM_DATE,   --审批人时间
                        t.EXAM_OPER,   --审批人
                        t.APPLY_DATE,   --申请时间
                        t.APPLY_OPER,   --申请人
                        t.SALE_COST,   --零售金额
                        t.SALE_PRICE,   --零售价格
                        t.APPLY_COST,   --申请金额
                        t.APPLY_PRICE,   --申请价格(入库价)
                        t.APPLY_NUM,   --申请数量
                        t.PACK_QTY,   --大包装包装数量
                        t.PACK_UNIT,   --大包装单位
                        t.MIN_UNIT,   --最小单位
                        t.SPECS,   --规格
                        t.KIND_CODE,   --物品科目编码
                        t.ITEM_NAME,   --物品名称
                        t.ITEM_CODE,   --物品编码
                        t.APPLY_STATE,   --申请状态(0-申请,1-审批,2-核准)
                        t.CLASS2_PRIV,   --二级权限(5510-入库申请,5520-出库申请)
                        t.TARGET_DEPT,   --目标科室
                        t.STORAGE_CODE,   --申请部门(库存部门)
                        t.APPLY_SERIAL_NO,   --单内序号
                        t.APPLY_LIST_CODE,   --申请单号
                        t.APPLY_NO   --申请流水号
                        FROM
                        MAT_COM_APPLY  t   --物资入出库申请表
                        WHERE
                          t.apply_list_code='{0}'";

            sql = string.Format(sql, applyLstCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.MatApplyRecord>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 更新物资申请记录有效状态
        /// </summary>
        /// <param name="applyNO"></param>
        /// <param name="validState"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <param name="dby"></param>
        /// <returns></returns>
        public int UpdateMatApplyRecordValidState(string applyNO, string validState, string operCode, DateTime operDate, BaseEntityer dby)
        {
            string sql = @"UPDATE MAT_COM_APPLY  t   --物资入出库申请表
                        SET
                        t.ABOLISH_DATE=TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS'),   --作废时间
                        t.ABOLISH_OPER='{1}',   --作废人编码
                        t.OPER_DATE=TO_DATE('{2}','YYYY-MM-DD HH24:MI:SS'),   --操作日期
                        t.OPER_CODE='{3}',   --操作员
                        t.VALID_STATE='{4}'   --有效性状态(1-有效,0-无效)
                        WHERE 
                         t.apply_list_code='{5}'
                        ";
            sql = string.Format(sql, operDate, operCode, operDate, operCode, validState, applyNO);
            return dby.ExecuteNonQuery(sql);
        }


        /// <summary>
        ///  查询历史物资申请记录集合
        /// </summary>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="applyDept"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IList<MatApplyRecord> QueryHistoryMatApplyRecordLst(DateTime begDate, DateTime endDate, string applyDept, string state, bool isBack)
        {
            string sql = string.Empty;
            if (!isBack)
            {
                sql = @"SELECT
                        t.APPLY_EXAMNUM,   --内部入库申请审批数量
                        t.APPLY_EXAMDATE,   --内部入库申请审批时间
                        t.APPLY_EXAMOPER,   --内部入库申请审批人
                        t.IS_APPLYEXAM,   --是否需要申请审批（0-否,1-是）
                        t.APPLY_TYPE,   --申请类型(0-常规,1-临时,2-紧急)
                        t.CHECK_DATE,   --审核时间
                        t.CHECK_OPER,   --审核人
                        t.APPLY_CHECK,   --申请科室审核(1-审核,0-未审核)
                        t.OPER_DATE,   --操作日期
                        t.OPER_CODE,   --操作员
                        t.MEMO,   --备注
                        t.IN_LIST_CODE,   --入库单号
                        t.OUT_LIST_CODE,   --出库单号
                        t.VALID_STATE,   --有效性状态(1-有效,0-无效)
                        t.STORE_TOTSUM,   --全院库存总和
                        t.STORE_SUM,   --申请科室库存
                        t.COMPANY_NAME,   --公司名称
                        t.COMPANY_CODE,   --公司编码
                        t.EXAM_NUM,   --审批数量
                        t.ABOLISH_DATE,   --作废时间
                        t.ABOLISH_OPER,   --作废人编码
                        t.APPROVE_DATE,   --核准时间
                        t.APPROVE_OPER,   --核准人
                        t.EXAM_DATE,   --审批人时间
                        t.EXAM_OPER,   --审批人
                        t.APPLY_DATE,   --申请时间
                        t.APPLY_OPER,   --申请人
                        t.SALE_COST,   --零售金额
                        t.SALE_PRICE,   --零售价格
                        t.APPLY_COST,   --申请金额
                        t.APPLY_PRICE,   --申请价格(入库价)
                        t.APPLY_NUM,   --申请数量
                        t.PACK_QTY,   --大包装包装数量
                        t.PACK_UNIT,   --大包装单位
                        t.MIN_UNIT,   --最小单位
                        t.SPECS,   --规格
                        t.KIND_CODE,   --物品科目编码
                        t.ITEM_NAME,   --物品名称
                        t.ITEM_CODE,   --物品编码
                        t.APPLY_STATE,   --申请状态(0-申请,1-审批,2-核准)
                        t.CLASS2_PRIV,   --二级权限(5510-入库申请,5520-出库申请)
                        t.TARGET_DEPT,   --目标科室
                        t.STORAGE_CODE,   --申请部门(库存部门)
                        t.APPLY_SERIAL_NO,   --单内序号
                        t.APPLY_LIST_CODE,   --申请单号
                        t.APPLY_NO   --申请流水号
                        FROM
                        MAT_COM_APPLY  t   --物资入出库申请表
                          WHERE t.apply_date >= TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS')
                           AND t.apply_date <= TO_DATE('{1}','YYYY-MM-DD HH24:MI:SS')
                           AND t.storage_code = '{2}'
                           AND t.apply_state = '{3}'
                           AND t.valid_state='1'";
                sql = string.Format(sql, begDate.ToString("yyyy-MM-dd") + " 00:00:00", endDate.ToString("yyyy-MM-dd") + " 23:59:59", applyDept, state);
            }
            else
            {
                sql = @"SELECT

                             '1' AS apply_check, --申请科室审核(1-审核,0-未审核)
                             t.oper_date AS oper_date, --操作日期
                             t.oper_code AS oper_code, --操作员
                             t.memo AS memo, --备注
                             t.in_list_code AS in_list_code, --入库单号
                             '' AS out_list_code, --出库单号
                             '' AS valid_state, --有效性状态(1-有效,0-无效)
                             '' AS store_totsum, --全院库存总和
                             '' AS store_sum, --申请科室库存
                             t.company_name AS company_name, --公司名称
                             t.company_code AS company_code, --公司编码
                             t.in_num AS exam_num, --审批数量
                             '' AS abolish_date, --作废时间
                             '' AS abolish_oper, --作废人编码
                             t.oper_date AS approve_date, --核准时间
                             t.oper_code AS approve_oper, --核准人
                             t.oper_date AS exam_date, --审批人时间
                             t.oper_code AS exam_oper, --审批人
                             t.oper_date AS apply_date, --申请时间
                             t.oper_code AS apply_oper, --申请人
                             t.sale_cost sale_cost, --零售金额
                             t.sale_price AS sale_price, --零售价格
                             t.in_cost AS apply_cost, --申请金额
                             t.pack_price AS apply_price, --申请价格(入库价)
                             t.in_num AS apply_num, --申请数量
                             t.pack_qty AS pack_qty, --大包装包装数量
                             t.pack_unit AS pack_unit, --大包装单位
                             t.min_unit AS min_unit, --最小单位
                             t.specs AS specs, --规格
                             t.kind_code AS kind_code, --物品科目编码
                             t.item_name AS item_name, --物品名称
                             t.item_code AS item_code, --物品编码
                             '' AS apply_state, --申请状态(0-申请,1-审批,2-核准)
                             '' AS class2_priv, --二级权限(5510-入库申请,5520-出库申请)
                             '' AS target_dept, --目标科室
                             t.storage_code AS storage_code, --申请部门(库存部门)
                             t.in_serial_no AS apply_serial_no, --单内序号
                             t.in_list_code AS apply_list_code, --申请单号
                             t.apply_no as apply_no, --申请流水号
                             t.stock_code as info_id-- 库存编码
                              FROM dept_mat_input t --物资入出库申请表
                             WHERE t.in_date >= to_date('{0}', 'YYYY-MM-DD HH24:MI:SS')
                               AND t.in_date <= to_date('{1}', 'YYYY-MM-DD HH24:MI:SS')
                               AND t.storage_code = '{2}'
                            ";
                sql = string.Format(sql, begDate.ToString("yyyy-MM-dd") + " 00:00:00", endDate.ToString("yyyy-MM-dd") + " 23:59:59", applyDept);
            }
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.MatApplyRecord>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 获得物资申请记录参数集合
        /// </summary>
        /// <param name="matApplyRecord"></param>
        /// <returns></returns>
        private object[] GetMatApplyRecordParam(HisCommon.DataEntity.MatApplyRecord matApplyRecord)
        {
            object[] para = new object[]{
                                        matApplyRecord.Apply_NO,
                                        matApplyRecord.Apply_list_code,
                                        matApplyRecord.Apply_serial_no,
                                        matApplyRecord.Storage_code,
                                        matApplyRecord.Target_dept,
                                        matApplyRecord.Class2_priv,
                                        matApplyRecord.Apply_state,
                                        matApplyRecord.Item_code,
                                        matApplyRecord.Item_name,
                                        matApplyRecord.Kind_code,
                                        matApplyRecord.Specs,
                                        matApplyRecord.Min_unit,
                                        matApplyRecord.Pack_unit,
                                        matApplyRecord.Pack_qty,
                                        matApplyRecord.Apply_num,
                                        matApplyRecord.Apply_price,
                                        matApplyRecord.Apply_cost,
                                        matApplyRecord.Sale_price,
                                        matApplyRecord.Sale_cost,
                                        matApplyRecord.Apply_oper,
                                        matApplyRecord.Apply_date,
                                        matApplyRecord.Exam_oper,
                                        matApplyRecord.Exam_date,
                                        matApplyRecord.Approve_oper,
                                        matApplyRecord.Approve_date,
                                        matApplyRecord.Abolish_oper,
                                        matApplyRecord.Abolish_date,
                                        matApplyRecord.Exam_num,
                                        matApplyRecord.Company_code,
                                        matApplyRecord.Company_name,
                                        matApplyRecord.Store_sum,
                                        matApplyRecord.Store_totsum,
                                        matApplyRecord.Valid_state,
                                        matApplyRecord.Out_list_code,
                                        matApplyRecord.In_list_code,
                                        matApplyRecord.Memo,
                                        matApplyRecord.Oper_code,
                                        matApplyRecord.Oper_date,
                                        matApplyRecord.Apply_check,
                                        matApplyRecord.Check_oper,
                                        matApplyRecord.Check_date,
                                        matApplyRecord.Apply_type,
                                        matApplyRecord.Is_applyexam,
                                        matApplyRecord.Apply_examoper,
                                        matApplyRecord.Apply_examdate,
                                        matApplyRecord.Apply_examnum,
                                        matApplyRecord.Info_ID

            };
            return para;
        }

        /// <summary>
        /// 删除物资申请记录根据单据号
        /// </summary>
        /// <param name="applyNO"></param>
        /// <param name="dby"></param>
        /// <returns></returns>
        public int DeleteMatApplyRecordByNO(string applyNO, BaseEntityer dby)
        {
            string sql = @"DELETE MAT_COM_APPLY   t  --物资入出库申请表
                            WHERE
                             t.apply_list_code='{0}'
                        ";
            sql = string.Format(sql, applyNO);
            return dby.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 加载一级物资库存的明细
        /// </summary>
        /// <param name="deptCode">库存编码</param>
        /// <returns></returns>
        public List<DEPT_MAT_STOCKDETAIL> LoadMatStockDetail(string deptCode)
        {
            List<DEPT_MAT_STOCKDETAIL> deptMatStockList = new List<DEPT_MAT_STOCKDETAIL>();
            string sql = @"SELECT t.low_num,
                               t.top_num,
                               t.highvalue_barcode,
                               t.highvalue_flag,
                               t.lack_flag,
                               t.memo,
                               t.company_name,
                               t.factory_name as factory_code ,
                               t.pack_unit,
                               t.pack_qty,
                               t.sale_cost,
                               t.sale_price,
                               t.store_cost,
                               t.in_price,
                               t.min_unit,
                               t.store_num,
                               t.batch_no,
                               t.specs,
                               t.item_code,
                               t.item_name,
                               t.kind_code,
                               t.finance_flag-- 记账标记
                          FROM mat_com_stockdetail_view t
                         WHERE t.stockdept = '{0}'
                            ";
            sql = string.Format(sql, deptCode);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DEPT_MAT_STOCKDETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 加载科室物资库存的明细
        /// </summary>
        /// <param name="deptCode">库存编码</param>
        /// <param name="funMarker">记账标记</param>
        /// <returns></returns>
        public List<DEPT_MAT_STOCKDETAIL> LoadDeptMatStockDetail(string deptCode, string funMarker)
        {
            List<DEPT_MAT_STOCKDETAIL> deptMatStockList = new List<DEPT_MAT_STOCKDETAIL>();
            string sql = @"SELECT t.low_num,
                               t.top_num,
                               t.highvalue_barcode,
                               t.highvalue_flag,
                               t.lack_flag,
                               t.memo,
                               t.company_code,
                               t.factory_code,
                               t.pack_unit,
                               t.pack_qty,
                               t.sale_cost,
                               t.sale_price,
                               t.store_cost,
                               t.in_price,
                               t.min_unit,
                               t.store_num,
                               t.batch_no,
                               t.specs,
                               t.item_code,
                               t.item_name,
                               t.kind_code,
                               t.stock_code,
                               t.stock_no,
                               t.place_code,
                               t.pack_price,
                               t.finance_flag,
                               t.In_no
                          FROM dept_mat_stockdetail t
                        WHERE t.storage_code = '{0}'
                             AND (t.finance_flag = '{1}' OR 'ALL'= '{1}')
                            ";
            sql = string.Format(sql, deptCode, funMarker);
            return DataSetToEntity.DataSetToT<HisCommon.DataEntity.DEPT_MAT_STOCKDETAIL>(BaseEntityer.Db.GetDataSet(sql)).ToList();
        }

        /// <summary>
        /// 加载物资库存的明细
        /// </summary>
        /// <param name="deptCode">库存编码</param>
        /// <returns></returns>
        public DataTable QueryStockDept()
        {
            string sql = @"SELECT
                        r.DEPT_CODE,   --科室代码
                        r.NAME,   --库房名称
                        r.CODE,   --科室代码
                        r.STORE_ID   --库房代码

                        FROM
                        EXP_STOCK_NAME  r   --
                            ";

            return BaseEntityer.Db.GetDataTable(sql);
        }

        /// <summary>
        /// 查询科室库存信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="deptCode"></param>
        /// <param name="outBillNO">一级出库编号</param>
        /// <param name="inBillNO">二级库入库编码</param>
        /// <returns></returns>
        public List<DEPT_MAT_STOCKDETAIL> QueryDeptMatStock(BaseEntityer db, string deptCode, string outBillNO, string inBillNO)
        {
            List<DEPT_MAT_STOCKDETAIL> deptMatStockList = new List<DEPT_MAT_STOCKDETAIL>();
            string strSql = string.Empty;
            if (string.IsNullOrEmpty(inBillNO))
            {
                strSql = @" SELECT t.stock_code,
                               t.stock_no,
                               t.storage_code,
                               t.kind_code,
                               t.item_code,
                               t.item_name,
                               t.specs,
                               t.batch_no,
                               t.place_code,
                               t.store_num,
                               t.store_cost,
                               t.min_unit,
                               r.in_no,
                               r.in_num AS in_num,
                               t.in_price,
                               t.input_date,
                               t.sale_price,
                               t.sale_cost,
                               t.pack_qty,
                               t.pack_unit,
                               t.pack_price,
                               t.factory_code,
                               t.company_code,
                               t.output_date,
                               t.valid_state,
                               t.top_num,
                               t.low_num,
                               t.lack_flag,
                               t.memo,
                               t.oper_code,
                               t.oper_date,
                               t.highvalue_flag,
                               t.highvalue_barcode,
                               t.finance_flag,
                               (select exp_stock_name.dept_code from exp_stock_name where store_id = source_dept) as   source_dept
                          FROM dept_mat_stockdetail t,
                               dept_mat_input       r
                         WHERE r.stock_code = t.stock_code
                           AND r.in_list_code = '{0}'
                           AND r.storage_code = '{1}'
                           AND t.store_num > 0
                         ORDER BY t.item_code,
                                  t.store_num DESC

                                            ";
                strSql = string.Format(strSql, outBillNO, deptCode);
            }
            else
            {
                strSql = @" SELECT stock_code,
                                stock_no,
                                storage_code,
                                kind_code,
                                item_code,
                                item_name,
                                specs,
                                batch_no,
                                place_code,
                                store_num,
                                store_cost,
                                min_unit,
                                in_no,
                                in_num,
                                in_price,
                                input_date,
                                sale_price,
                                sale_cost,
                                pack_qty,
                                pack_unit,
                                pack_price,
                                factory_code,
                                company_code,
                                output_date,
                                valid_state,
                                top_num,
                                low_num,
                                lack_flag,
                                memo,
                                oper_code,
                                oper_date,
                                highvalue_flag,
                                highvalue_barcode,
                                finance_flag
                           FROM dept_mat_stockdetail t
                          WHERE t.in_no = '{0}'
                            AND t.storage_code = '{1}'
                            AND t.store_num > 0
                          ORDER BY item_code,
                                   store_num DESC
                        ";
                strSql = string.Format(strSql, inBillNO, deptCode);
            }

            DataSet ds = db.GetDataSet(strSql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_STOCKDETAIL>(ds).ToList();
        }

        /// <summary>
        /// 获得科室库存的退库数量根据库存编码
        /// </summary>
        /// <param name="returnInNO"></param>
        /// <returns></returns>
        public decimal GetBackDeptStorageNum(string returnInNO)
        {
            string strSql = @"SELECT nvl(SUM(t.out_num), 0)
                  FROM dept_mat_output t
                 WHERE t.Return_Out_No = '{0}'
                   AND t.trans_type = '2'
                   AND t.out_state = '6'";
            strSql = string.Format(strSql, returnInNO);
            return BaseEntityer.Db.ExecuteScalar<decimal>(strSql);
        }

        /// <summary>
        ///  更新物资申请记录的状态
        /// </summary>
        /// <param name="operCode">操作员</param>
        /// <param name="operDate">操作时间</param>
        /// <param name="inlistCode">入库单号</param>
        /// <param name="applyListCode">申请单号</param>
        /// <param name="applySerialNO">申请单内序号</param>
        /// <param name="inNum">入库数量</param>
        /// <returns></returns>
        public int UpdateMatApplyRecordState(BaseEntityer dby, string operCode, string operDate, string inlistCode, string applyListCode, string applySerialNO, decimal inNum)
        {
            string strSql = @"
                            UPDATE mat_com_apply t
                               SET t.apply_examoper = '{0}', --入库人
                                   t.apply_examdate = to_date('{1}', 'YYYY-MM-DD HH24:MI:SS'), --入库时间
                                   t.in_list_code   = '{2}', -- 入库单据号
                                   t.apply_state    = '2', --申请状态
                                   t.apply_examnum   = t.apply_examnum + '{5}' --申请数量
                             WHERE t.apply_list_code = '{3}' --申请单据号
                               AND t.apply_serial_no = '{4}' --申请单内序号
                                ";
            strSql = string.Format(strSql, operCode, operDate, inlistCode, applyListCode, applySerialNO, inNum);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        ///  获得物资申请记录
        /// </summary>
        /// <param name="applyListCode"></param>
        /// <param name="applySerialNO"></param>
        /// <returns></returns>
        public MatApplyRecord GetMatApplyRecordByListCodeAndSerialNO(string applyListCode, string applySerialNO)
        {
            string strSql = @"SELECT
                        t.APPLY_EXAMNUM,   --内部入库申请审批数量
                        t.APPLY_EXAMDATE,   --内部入库申请审批时间
                        t.APPLY_EXAMOPER,   --内部入库申请审批人
                        t.IS_APPLYEXAM,   --是否需要申请审批（0-否,1-是）
                        t.APPLY_TYPE,   --申请类型(0-常规,1-临时,2-紧急)
                        t.CHECK_DATE,   --审核时间
                        t.CHECK_OPER,   --审核人
                        t.APPLY_CHECK,   --申请科室审核(1-审核,0-未审核)
                        t.OPER_DATE,   --操作日期
                        t.OPER_CODE,   --操作员
                        t.MEMO,   --备注
                        t.IN_LIST_CODE,   --入库单号
                        t.OUT_LIST_CODE,   --出库单号
                        t.VALID_STATE,   --有效性状态(1-有效,0-无效)
                        t.STORE_TOTSUM,   --全院库存总和
                        t.STORE_SUM,   --申请科室库存
                        t.COMPANY_NAME,   --公司名称
                        t.COMPANY_CODE,   --公司编码
                        t.EXAM_NUM,   --审批数量
                        t.ABOLISH_DATE,   --作废时间
                        t.ABOLISH_OPER,   --作废人编码
                        t.APPROVE_DATE,   --核准时间
                        t.APPROVE_OPER,   --核准人
                        t.EXAM_DATE,   --审批人时间
                        t.EXAM_OPER,   --审批人
                        t.APPLY_DATE,   --申请时间
                        t.APPLY_OPER,   --申请人
                        t.SALE_COST,   --零售金额
                        t.SALE_PRICE,   --零售价格
                        t.APPLY_COST,   --申请金额
                        t.APPLY_PRICE,   --申请价格(入库价)
                        t.APPLY_NUM,   --申请数量
                        t.PACK_QTY,   --大包装包装数量
                        t.PACK_UNIT,   --大包装单位
                        t.MIN_UNIT,   --最小单位
                        t.SPECS,   --规格
                        t.KIND_CODE,   --物品科目编码
                        t.ITEM_NAME,   --物品名称
                        t.ITEM_CODE,   --物品编码
                        t.APPLY_STATE,   --申请状态(0-申请,1-审批,2-核准)
                        t.CLASS2_PRIV,   --二级权限(5510-入库申请,5520-出库申请)
                        t.TARGET_DEPT,   --目标科室
                        t.STORAGE_CODE,   --申请部门(库存部门)
                        t.APPLY_SERIAL_NO,   --单内序号
                        t.APPLY_LIST_CODE,   --申请单号
                        t.APPLY_NO   --申请流水号
                        FROM
                        MAT_COM_APPLY  t   --物资入出库申请表 
                        WHERE t.apply_list_code = '{0}'
                           AND t.apply_serial_no = '{1}'";
            strSql = string.Format(strSql, applyListCode, applySerialNO);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql);
            var tempLst = DataSetToEntity.DataSetToT<MatApplyRecord>(ds);
            if (tempLst == null || tempLst.Count <= 0)
                return null;
            else
                return tempLst.First();
        }

        /// <summary>
        /// 更新科室库存过账属性
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="storageCode"></param>
        /// <param name="priceFlag"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int UpdateDeptStockPriceAttribute(BaseEntityer dby,string stockCode, string storageCode,string priceFlag, string operCode)
        {
            if (!dby.IsBeginTransaction)
                dby.BeginTransaction();

            string sql = @"
update  dept_mat_input  r set  r.finance_flag='{0}' ,r.oper_code='{1}' ,r.oper_date=sysdate where  r.stock_code='{2}' and  r.storage_code='{3}'";

            sql = string.Format(sql, priceFlag, operCode, stockCode, storageCode);

            string sql2 = @"update  dept_mat_stockdetail  r set  r.finance_flag='{0}' ,r.oper_code='{1}' ,r.oper_date=sysdate where  r.stock_code='{2}' and  r.storage_code='{3}'";

            sql2 = string.Format(sql2,priceFlag, operCode, stockCode, storageCode);

            int rev = dby.ExecuteNonQuery(sql);
            if (rev > 0)
            {
                int rev2 = dby.ExecuteNonQuery(sql2);
                if (rev2 > 0)
                {
                    dby.CommitTransaction();
                    return 1;
                }
                else
                {
                    dby.RollbackTransaction();
                    return -1;
                }
            }
            else
            {
                dby.RollbackTransaction();
                return -1;
            }
        }
        #endregion

        #region 物资盘点相关业务处理方法 by dong_w 2015年3月11日

        #region 公有方法
        /// <summary>
        ///  保存物资盘点明细信息到数据库表里
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="matComCheckDetail"></param>
        /// <returns></returns>
        public int SaveCheckDetailInfoToDB(BaseEntityer dby, MatComCheckDetail matComCheckDetail)
        {
            string sql = @"INSERT INTO MAT_COM_CHECKDETAIL     --物资盘点明细表
                            (
                            OPER_DATE,   --操作日期
                            OPER_CODE,   --操作员
                            MEMO,   --备注
                            CHECK_STATE,   --盘点状态(0-封帐,1-结存,2-取消)
                            PROFIT_LOSS_NUM,   --盈亏数量
                            PROFIT_FLAG,   --盈亏标记(0-盘亏,1-盘盈,2-无盈亏)
                            IN_NUM,   --原始购入数量
                            CSTORE_NUM,   --结存库存数量
                            ADJUST_NUM,   --实际盘存数量
                            FSTORE_NUM,   --封帐库存数量
                            COMPANY_CODE,   --供货公司
                            FACTORY_CODE,   --生产厂家
                            PLACE_CODE,   --库位编号
                            PACK_QTY,   --大包装数量
                            PACK_UNIT,   --大包装单位
                            UNIT,   --最小单位
                            SPECS,   --规格
                            SALE_PRICE,   --零售价格
                            IN_PRICE,   --购入价格(大包装)
                            STOCK_COLLECTTYPE,   --库存汇总方式
                            STOCK_NO,   --库存序号
                            STOCK_CODE,   --库存流水号
                            ITEM_NAME,   --物品名称
                            ITEM_CODE,   --物品编码
                            STORAGE_CODE,   --仓库编码
                            CHECK_CODE,   --盘点主表流水号
                            CHECKDETAIL_CODE   --盘点明细流水号
                            ) 
                            VALUES
                            (
                            TO_DATE('{0}','YYYY-MM-DD HH24:MI:SS'),   --操作日期
                            '{1}',   --操作员
                            '{2}',   --备注
                            '{3}',   --盘点状态(0-封帐,1-结存,2-取消)
                            '{4}',   --盈亏数量
                            '{5}',   --盈亏标记(0-盘亏,1-盘盈,2-无盈亏)
                            '{6}',   --原始购入数量
                            '{7}',   --结存库存数量
                            '{8}',   --实际盘存数量
                            '{9}',   --封帐库存数量
                            '{10}',   --供货公司
                            '{11}',   --生产厂家
                            '{12}',   --库位编号
                            '{13}',   --大包装数量
                            '{14}',   --大包装单位
                            '{15}',   --最小单位
                            '{16}',   --规格
                            '{17}',   --零售价格
                            '{18}',   --购入价格(大包装)
                            '{19}',   --库存汇总方式
                            '{20}',   --库存序号
                            '{21}',   --库存流水号
                            '{22}',   --物品名称
                            '{23}',   --物品编码
                            '{24}',   --仓库编码
                            '{25}',   --盘点主表流水号
                            '{26}'   --盘点明细流水号
                            ) ";
            sql = string.Format(sql, GetMatCheckDetailParam(matComCheckDetail));
            return dby.ExecuteNonQuery(sql);
        }

        /// <summary>
        ///  保存物资盘点头信息到数据库表里
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="matComCheckhead"></param>
        /// <returns></returns>
        public int SaveCheckHeadInfoToDB(BaseEntityer dby, MatComCheckhead matComCheckhead)
        {
            string sql = @"INSERT  INTO mat_com_checkhead --物资盘点主表
                              (oper_date, --操作日期
                               oper_code, --操作员
                               saleprice_profit, --盘盈金额(零售价)
                               saleprice_loss, --盘亏金额(零售价)
                               inprice_profit, --盘盈金额(入库价)
                               inprice_loss, --盘亏金额(入库价)
                               coper_time, --结存时间
                               coper_code, --结存人
                               foper_time, --封帐时间
                               foper_code, --封帐人
                               check_name, --盘点单名称
                               check_state, --盘点状态(0-封帐,1-结存,2-取消)
                               storage_code, --仓库编码
                               check_code --盘点流水号
                               )
                            VALUES
                              (to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                               '{1}', --操作员
                               '{2}', --盘盈金额(零售价)
                               '{3}', --盘亏金额(零售价)
                               '{4}', --盘盈金额(入库价)
                               '{5}', --盘亏金额(入库价)
                               to_date('{6}', 'YYYY-MM-DD HH24:MI:SS'), --结存时间
                               '{7}', --结存人
                               to_date('{8}', 'YYYY-MM-DD HH24:MI:SS'), --封帐时间
                               '{9}', --封帐人
                               '{10}', --盘点单名称
                               '{11}', --盘点状态(0-封帐,1-结存,2-取消)
                               '{12}', --仓库编码
                               '{13}' --盘点流水号
                               )
                            ";
            sql = string.Format(sql, GetMatCheckHeadParam(matComCheckhead));
            return dby.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取序列值
        /// </summary>
        /// <param name="seqName"></param>
        /// <returns></returns>
        public int GetSequence(string seqName)
        {
            string strSQL = " select {0}.NEXTVAL from dual ";
            strSQL = string.Format(strSQL, seqName);
            string value = BaseEntityer.Db.ExecuteScalar(strSQL).ToString();
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// 查询当前仓房，当前操作员的盘点单信息
        /// </summary>
        /// <param name="foper_code">封账人</param>
        /// <param name="storageCode">仓房编码</param>
        /// <returns></returns>
        public IList<MatComCheckhead> QueryMatComCheckHeadLst(string foper_code, string storageCode)
        {
            string strSql = @"SELECT t.oper_date, --操作日期
                               t.oper_code, --操作员
                               t.saleprice_profit, --盘盈金额(零售价)
                               t.saleprice_loss, --盘亏金额(零售价)
                               t.inprice_profit, --盘盈金额(入库价)
                               t.inprice_loss, --盘亏金额(入库价)
                               t.coper_time, --结存时间
                               t.coper_code, --结存人
                               t.foper_time, --封帐时间
                               t.foper_code, --封帐人
                               t.check_name, --盘点单名称
                               t.check_state, --盘点状态(0-封帐,1-结存,2-取消)
                               t.storage_code, --仓库编码
                               t.check_code --盘点流水号
                          FROM mat_com_checkhead t --物资盘点主表
                         WHERE t.storage_code = '{0}'
                           AND t.check_state = '0'
                           AND t.foper_code = '{1}'
                        ";
            strSql = string.Format(strSql, storageCode, foper_code);
            DbDataReader dataReader = BaseEntityer.Db.ExecuteReader(strSql);
            IList<MatComCheckhead> matComCheckheadLst = new List<MatComCheckhead>();

            while (dataReader.Read())
            {
                MatComCheckhead temp = new MatComCheckhead();
                temp.OperDate = DateTime.Parse(dataReader[0].ToString());
                temp.OperCode = dataReader[1].ToString();
                temp.SalePriceProfit = decimal.Parse(dataReader[2].ToString());
                temp.SalePriceLoss = decimal.Parse(dataReader[3].ToString());
                temp.InpriceProfit = decimal.Parse(dataReader[4].ToString());
                temp.InpriceLoss = decimal.Parse(dataReader[5].ToString());
                temp.CoperTime = DateTime.Parse(dataReader[6].ToString());
                temp.CoperCode = dataReader[7].ToString();
                temp.FoperTime = DateTime.Parse(dataReader[8].ToString());
                temp.FoperCode = dataReader[9].ToString();
                temp.CheckName = dataReader[10].ToString();
                temp.CheckState = dataReader[11].ToString();
                temp.StorageCode = dataReader[12].ToString();
                temp.CheckCode = dataReader[13].ToString();
                matComCheckheadLst.Add(temp);
            }

            if (!dataReader.IsClosed)
                dataReader.Close();
            return matComCheckheadLst;
        }

        /// <summary>
        /// 查询物资盘点明细项目信息集合
        /// </summary>
        /// <param name="checkCode">盘点编号</param>
        /// <returns></returns>
        public IList<MatComCheckDetail> QueryMatComCheckDetailLstByCode(string checkCode)
        {
            string strSql = @"SELECT t.oper_date, --操作日期
                               t.oper_code, --操作员
                               t.memo, --备注
                               t.check_state, --盘点状态(0-封帐,1-结存,2-取消)
                               t.profit_loss_num, --盈亏数量
                               t.profit_flag, --盈亏标记(0-盘亏,1-盘盈,2-无盈亏)
                               t.in_num, --原始购入数量
                               t.cstore_num, --结存库存数量
                               t.adjust_num, --实际盘存数量
                               t.fstore_num, --封帐库存数量
                               t.company_code, --供货公司
                               t.factory_code, --生产厂家
                               t.place_code, --库位编号
                               t.pack_qty, --大包装数量
                               t.pack_unit, --大包装单位
                               t.unit, --最小单位
                               t.specs, --规格
                               t.sale_price, --零售价格
                               t.in_price, --购入价格(大包装)
                               t.stock_collecttype, --库存汇总方式
                               t.stock_no, --库存序号
                               t.stock_code, --库存流水号
                               t.item_name, --物品名称
                               t.item_code, --物品编码
                               t.storage_code, --仓库编码
                               t.check_code, --盘点主表流水号
                               t.checkdetail_code, --盘点明细流水号
                               f_trans_pinyin_capital(t.item_name), -- 拼音码 
                               (t.fstore_num -
                               (SELECT r.store_num
                                   FROM dept_mat_stockdetail r
                                  WHERE r.stock_code = t.stock_code
                                    AND r.storage_code = t.storage_code)) AS marginnum -- 差额数量
                          FROM mat_com_checkdetail t --物资盘点明细表
                         WHERE t.check_code = '{0}'
                        ";
            strSql = string.Format(strSql, checkCode);
            DbDataReader dataReader = BaseEntityer.Db.ExecuteReader(strSql);
            IList<MatComCheckDetail> matComCheckDetailLst = new List<MatComCheckDetail>();

            while (dataReader.Read())
            {
                MatComCheckDetail temp = new MatComCheckDetail();
                temp.OperDate = DateTime.Parse(dataReader[0].ToString());
                temp.OperCode = dataReader[1].ToString();
                temp.Memo = dataReader[2].ToString();
                temp.CheckState = string.IsNullOrEmpty(dataReader[3].ToString()) ? 0 : decimal.Parse(dataReader[3].ToString());
                temp.ProfitLossNum = string.IsNullOrEmpty(dataReader[4].ToString()) ? 0 : decimal.Parse(dataReader[4].ToString());
                temp.ProfitFlag = dataReader[5].ToString();
                temp.InNum = string.IsNullOrEmpty(dataReader[6].ToString()) ? 0 : decimal.Parse(dataReader[6].ToString());
                temp.CstoreNum = string.IsNullOrEmpty(dataReader[7].ToString()) ? 0 : decimal.Parse(dataReader[7].ToString());
                temp.AdjustNum = string.IsNullOrEmpty(dataReader[8].ToString()) ? 0 : decimal.Parse(dataReader[8].ToString());

                temp.FstoreNum = string.IsNullOrEmpty(dataReader[9].ToString()) ? 0 : decimal.Parse(dataReader[9].ToString());
                temp.CompanyCode = dataReader[10].ToString();
                temp.FactoryCode = dataReader[11].ToString();
                temp.PlaceCode = dataReader[12].ToString();
                temp.PackQty = string.IsNullOrEmpty(dataReader[13].ToString()) ? 0 : decimal.Parse(dataReader[13].ToString());
                temp.PackUnit = dataReader[14].ToString();
                temp.UNIT = dataReader[15].ToString();
                temp.Specs = dataReader[16].ToString();
                temp.SalePrice = string.IsNullOrEmpty(dataReader[17].ToString()) ? 0 : decimal.Parse(dataReader[17].ToString());
                temp.InPrice = string.IsNullOrEmpty(dataReader[18].ToString()) ? 0 : decimal.Parse(dataReader[18].ToString());
                temp.StockCollectType = dataReader[19].ToString();
                temp.StockNO = dataReader[20].ToString();
                temp.StockCode = dataReader[21].ToString();
                temp.ItemName = dataReader[22].ToString();
                temp.ItemCode = dataReader[23].ToString();
                temp.StorageCode = dataReader[24].ToString();
                temp.CheckCode = dataReader[25].ToString();
                temp.CheckDetailCode = dataReader[26].ToString();
                temp.SpellCode = dataReader[27].ToString();
                temp.InCost = temp.InPrice * temp.FstoreNum;
                temp.SaleCost = temp.SalePrice * temp.FstoreNum;
                temp.AdjustInCost = temp.InPrice * temp.AdjustNum;
                temp.AdjustSaleCost = temp.SalePrice * temp.AdjustNum;
                temp.MarginNum = string.IsNullOrEmpty(dataReader[28].ToString()) ? 0 : decimal.Parse(dataReader[28].ToString());
                temp.CstoreNum = temp.AdjustNum - temp.MarginNum;
                matComCheckDetailLst.Add(temp);
            }
            if (!dataReader.IsClosed)
                dataReader.Close();
            return matComCheckDetailLst;
        }

        #region 结存
        /// <summary>
        ///  更新科室库存（库存量，库存金额）
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="strockNO"></param>
        /// <param name="stockNum"></param>
        /// <param name="stockInCost"></param>
        /// <returns></returns>
        public int UpdateDeptStockDetail(BaseEntityer dby, string stockCode, string stockNO, decimal stockNum, decimal stockInCost)
        {
            string strSql = @"UPDATE dept_mat_stockdetail t --物资库存明细表
                           SET t.store_cost = '{0}', --库存金额
                               t.store_num  = '{1}' --库存数量
                         WHERE t.stock_no = '{2}' --库存序号
                           AND t.stock_code = '{3}' --库存流水号
                        ";
            strSql = string.Format(strSql, stockInCost, stockNum, stockNO, stockCode);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 更新盘点库存明细
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="checkDetailCode"></param>
        /// <param name="cStoreNum"></param>
        /// <param name="profitLossNum"></param>
        /// <returns></returns>
        public int UpdateCheckStockDetail(BaseEntityer dby, string checkDetailCode, decimal cStoreNum, decimal profitLossNum, string profitFlag, string memo, string operCode, string operDate)
        {
            string strSql = @"UPDATE mat_com_checkdetail t --物资盘点明细表
                               SET t.oper_date       = to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                                   t.oper_code       = '{1}', --操作员
                                   t.memo            = '{2}', --备注
                                   t.check_state     = '1', --盘点状态(0-封帐,1-结存,2-取消)
                                   t.profit_loss_num = '{3}', --盈亏数量
                                   t.profit_flag     = '{4}', --盈亏标记(0-盘亏,1-盘盈,2-无盈亏)
                                   t.cstore_num      = '{5}' --结存库存数量
                             WHERE t.checkdetail_code = '{6}' --盘点明细流水号
                        ";
            strSql = string.Format(strSql, operDate, operCode, memo, profitLossNum, profitFlag, cStoreNum, checkDetailCode);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        ///  更新盘点库存头表信息
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="checkCode"></param>
        /// <param name="salepriceProfit"></param>
        /// <param name="salepriceLoss"></param>
        /// <param name="inpriceProfit"></param>
        /// <param name="inpriceLoss"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateCheckStockHead(BaseEntityer dby, string checkCode, decimal salepriceProfit, decimal salepriceLoss, decimal inpriceProfit, decimal inpriceLoss, string operCode, string operDate)
        {
            string strSql = @"UPDATE mat_com_checkhead t --物资盘点主表
                               SET t.oper_date        = to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                                   t.oper_code        = '{1}', --操作员
                                   t.saleprice_profit = '{2}', --盘盈金额(零售价)
                                   t.saleprice_loss   = '{3}', --盘亏金额(零售价)
                                   t.inprice_profit   = '{4}', --盘盈金额(入库价)
                                   t.inprice_loss     = '{5}', --盘亏金额(入库价)
                                   t.coper_time       = to_date('{6}', 'YYYY-MM-DD HH24:MI:SS'), --结存时间
                                   t.coper_code       = '{7}' ,--结存人
                                   t.Check_State      ='1' --结存状态了
                             WHERE t.check_code = '{8}' --盘点流水号
                        ";
            strSql = string.Format(strSql, operDate, operCode, salepriceProfit, salepriceLoss, inpriceProfit, inpriceLoss, operDate, operCode, checkCode);
            return dby.ExecuteNonQuery(strSql);
        }
        #endregion

        /// <summary>
        /// 获得当前库存项目是否在盘点状态
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="storageCode"></param>
        /// <returns></returns>
        public int GetCheckStockDetail(string stockCode, string storageCode)
        {
            string strSql = @" 
                                 SELECT COUNT(*)
                                   FROM mat_com_checkdetail t
                                  WHERE t.stock_code = '{0}'
                                    AND t.check_state = '0'
                                    AND t.storage_code = '{1}'
                                ";
            strSql = string.Format(strSql, stockCode, storageCode);
            return BaseEntityer.Db.ExecuteScalar<int>(strSql);
        }

        /// <summary>
        ///  更新盘点单的头状态
        /// </summary>
        /// <param name="checkCode"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateCheckHeadState(BaseEntityer dby, string checkCode, string operCode, string operDate)
        {
            string strSql = @"
                                UPDATE mat_com_checkhead t
                                   SET t.check_state = '2',
                                       t.oper_code   = '{1}',
                                       t.oper_date   = to_date('{2}', 'YYYY-MM-DD HH24:MI:SS') --操作时间
                                 WHERE t.check_code = '{0}'
                                       AND t.check_state = '0'
                                ";
            strSql = string.Format(strSql, checkCode, operCode, operDate);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        ///  更新盘点单的明细状态
        /// </summary>
        /// <param name="checkCode"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateCheckDetailState(BaseEntityer dby, string checkCode, string operCode, string operDate)
        {
            string strSql = @"
                      UPDATE mat_com_checkdetail t
                           SET t.check_state = '2',
                               t.oper_code   = '{1}',
                               t.oper_date   = to_date('{2}', 'YYYY-MM-DD HH24:MI:SS')--操作时间
                         WHERE t.check_code = '{0}'
                           AND t.check_state = '0'
                                ";
            strSql = string.Format(strSql, checkCode, operCode, operDate);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        ///  查询盘点单信息通过库存表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCheckBillByStockInfo(string deptCode)
        {
            string strSql = @"
                               SELECT t.item_code, --项目编码
                               t.item_name, --项目名称
                               t.specs, -- 规格
                               t.in_price, --调拨价
                               t.sale_price, --零售价
                               nvl(t.store_num, 0) AS store_num, --库存数量
                               nvl(t.store_cost, 0) AS store_cost, --库存金额
                               nvl((SELECT SUM(r.out_num)
                                     FROM dept_mat_output r
                                    WHERE r.stock_code = t.stock_code
                                      AND t.valid_state = '1'),
                                   0) AS out_num, --出库数量
                               nvl((SELECT SUM(r.out_cost)
                                     FROM dept_mat_output r
                                    WHERE r.stock_code = t.stock_code
                                      AND t.valid_state = '1'),
                                   0) AS out_cost, --出库金额
                               nvl((SELECT SUM(n.in_num)
                                     FROM dept_mat_input n
                                    WHERE n.stock_code = t.stock_code),
                                   0) AS in_num, --入库数量
                               nvl((SELECT SUM(n.in_cost)
                                     FROM dept_mat_input n
                                    WHERE n.stock_code = t.stock_code),
                                   0) AS in_cost, --入库金额
                               nvl((SELECT SUM(m.profit_loss_num)
                                     FROM mat_com_checkdetail m
                                    WHERE m.stock_code = t.stock_code
                                      AND m.check_state = '1'),
                                   0) AS check_num, --盈亏数量
                               nvl((SELECT SUM(m.profit_loss_num * m.in_price)
                                     FROM mat_com_checkdetail m
                                    WHERE m.stock_code = t.stock_code
                                      AND m.check_state = '1'),
                                   0) AS check_cost --盈亏金额
                          FROM dept_mat_stockdetail t
                         WHERE t.storage_code = '{0}'
                           AND t.valid_state = '1'

                                ";
            strSql = string.Format(strSql, deptCode);

            return BaseEntityer.Db.GetDataSet(strSql).Tables[0];
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获得物资盘点明细参数
        /// </summary>
        /// <param name="matComCheckDetail"></param>
        /// <returns></returns>
        private object[] GetMatCheckDetailParam(MatComCheckDetail matComCheckDetail)
        {
            object[] para = new object[]{
                                        matComCheckDetail.OperDate,
                                        matComCheckDetail.OperCode,
                                        matComCheckDetail.Memo,
                                        matComCheckDetail.CheckState,
                                        matComCheckDetail.ProfitLossNum,
                                        matComCheckDetail.ProfitFlag,
                                        matComCheckDetail.InNum,
                                        matComCheckDetail.CstoreNum,
                                        matComCheckDetail.AdjustNum,
                                        matComCheckDetail.FstoreNum,
                                        matComCheckDetail.CompanyCode,
                                        matComCheckDetail.FactoryCode,
                                        matComCheckDetail.PlaceCode,
                                        matComCheckDetail.PackQty,
                                        matComCheckDetail.PackUnit,
                                        matComCheckDetail.UNIT,
                                        matComCheckDetail.Specs,
                                        matComCheckDetail.SalePrice,
                                        matComCheckDetail.InPrice,
                                        matComCheckDetail.StockCollectType,
                                        matComCheckDetail.StockNO,
                                        matComCheckDetail.StockCode,
                                        matComCheckDetail.ItemName,
                                        matComCheckDetail.ItemCode,
                                        matComCheckDetail.StorageCode,
                                        matComCheckDetail.CheckCode,
                                        matComCheckDetail.CheckDetailCode,

            };
            return para;
        }

        /// <summary>
        /// 获得物资盘点明细参数
        /// </summary>
        /// <param name="matComCheckHead"></param>
        /// <returns></returns>
        private object[] GetMatCheckHeadParam(MatComCheckhead matComCheckHead)
        {
            object[] para = new object[]{
                                        matComCheckHead.OperDate,
                                        matComCheckHead.OperCode,
                                        matComCheckHead.SalePriceProfit,
                                        matComCheckHead.SalePriceLoss,
                                        matComCheckHead.InpriceProfit,
                                        matComCheckHead.InpriceLoss,
                                        matComCheckHead.CoperTime,
                                        matComCheckHead.CoperCode,
                                        matComCheckHead.FoperTime,
                                        matComCheckHead.FoperCode,
                                        matComCheckHead.CheckName,
                                        matComCheckHead.CheckState,
                                        matComCheckHead.StorageCode,
                                        matComCheckHead.CheckCode,
            };
            return para;
        }
        #endregion
        #endregion

        #region 物资入库相关业务处理方法  by dong_w 2015年3月12日

        /// <summary>
        /// 更新科室物资库存明细信息
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="deptMatStockDetail">科室物资明细</param>
        /// <returns></returns>
        public int UpdateDeptMatStockDetailInfo(BaseEntityer dby, DEPT_MAT_STOCKDETAIL deptMatStockDetail)
        {
            string strSql = @"UPDATE dept_mat_stockdetail t --物资库存明细表
                               SET t.oper_date  = to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                                   t.oper_code  = '{1}', --操作员
                                   t.sale_cost  = t.sale_cost + '{5}', --零售金额
                                   t.input_date = to_date('{6}', 'YYYY-MM-DD HH24:MI:SS'), --购入日期
                                   t.in_num     = t.in_num + '{9}', --购入数量
                                   t.in_no      = '{10}', --入库记录流水号 流水记录
                                   t.store_cost = t.store_cost + '{11}', --库存金额
                                   t.store_num  = t.store_num + '{12}' --库存数量
                             WHERE t.item_code = '{15}' --物品编码
                               AND (t.batch_no = '{13}' OR t.batch_no IS NULL) --批号
                               AND t.specs = '{14}' --规格
                               AND t.sale_price = '{7}' --零售价格
                               AND t.in_price = '{8}' --购入价格(大包装)
                               AND (t.company_code = '{2}' OR t.company_code IS NULL) --供货公司
                               AND (t.factory_code = '{3}' OR t.factory_code IS NULL) --生产厂家
                               AND t.pack_price = '{4}' --大包装价格(购入价)
                               AND t.storage_code = '{16}' -- 科室记录
                               AND t.item_name = '{17}'
                            ";
            strSql = string.Format(strSql, deptMatStockDetail.OPER_DATE.ToString(), deptMatStockDetail.OPER_CODE, deptMatStockDetail.COMPANY_CODE, deptMatStockDetail.FACTORY_CODE, deptMatStockDetail.PACK_PRICE, deptMatStockDetail.SALE_COST, deptMatStockDetail.INPUT_DATE.ToString(), deptMatStockDetail.SALE_PRICE, deptMatStockDetail.IN_PRICE, deptMatStockDetail.IN_NUM, deptMatStockDetail.IN_NO, deptMatStockDetail.STORE_COST, deptMatStockDetail.STORE_NUM, deptMatStockDetail.BATCH_NO, deptMatStockDetail.SPECS, deptMatStockDetail.ITEM_CODE, deptMatStockDetail.STORAGE_CODE, deptMatStockDetail.ITEM_NAME);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 插入科室
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="InNO"></param>
        /// <param name="in_num"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int InsertDeptMatInAndStockDetailInfo(BaseEntityer dby, string stockCode, string InNO, decimal in_num, string operCode, string operDate)
        {
            string strSql = @"INSERT INTO deptmatin_stockdetail_record --
                              (oper_date, --操作日期
                               oper_code, --操作员
                               in_no, --入库流水号
                               stock_code, --库存流水号
                               in_num-- 入库量
                               )
                            VALUES
                              (to_date('{0}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                               '{1}', --操作员
                               '{2}', --入库流水号
                               '{3}', --库存流水号
                               '{4}'-- 入库量
                               )
                            ";
            strSql = string.Format(strSql, operDate.ToString(), operCode, InNO, stockCode, in_num);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 插入科室
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="InNO"></param>
        /// <param name="in_num"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateDeptMatInAndStockDetailInfo(BaseEntityer dby, string stockCode, string InNO, decimal in_num, string operCode, string operDate)
        {
            string strSql = @"UPDATE deptmatin_stockdetail_record t --
                                SET t.oper_code = '{2}', --操作员
                                    t.oper_date = to_date('{3}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期
                                    t.in_num    + = '{4}' --入库数量
                                WHERE t.stock_code = '{0}', --库存流水号
                                t.in_no = '{1}' --入库流水号
                            ";
            strSql = string.Format(strSql, stockCode, InNO, operCode, operDate, in_num);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 获得物资申请与科室库存记录信息
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="inNO"></param>
        /// <returns></returns>
        public int GetDeptMatInAndStockDetailInfoByPrimarykey(string stockCode, string inNO)
        {
            string strSql = @"SELECT COUNT(1)
                                FROM deptmatin_stockdetail_record t --
                                WHERE t.in_no = '{0}' --入库流水号
                                AND t.stock_code = '{1}' --库存流水号
                            ";
            strSql = string.Format(strSql, stockCode, inNO);
            return BaseEntityer.Db.ExecuteScalar<int>(strSql);
        }

        /// <summary>
        /// 获得科室库存的库存流水号
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="batchNO"></param>
        /// <param name="specs"></param>
        /// <param name="inPrice"></param>
        /// <param name="salePrice"></param>
        /// <param name="factoryCode"></param>
        /// <param name="companyCode"></param>
        /// <param name="packPrice"></param>
        /// <param name="storageCode">库存编码</param>
        /// <returns></returns>
        public string GetDeptMatStockDetailInfoBy(string itemCode, string batchNO, string specs, decimal inPrice, decimal
            salePrice, string factoryCode, string companyCode, decimal packPrice, string storageCode, string itemName)
        {
            string strSql = @"SELECT stock_code --库存流水号
                                  FROM dept_mat_stockdetail
                                 WHERE (company_code = '{0}' OR company_code IS NULL) --供货公司
                                   AND (factory_code = '{1}' OR factory_code IS NULL) --生产厂家
                                   AND pack_price = '{2}' --大包装价格(购入价)
                                   AND sale_price = '{3}' --零售价格
                                   AND in_price = '{4}' --购入价格(大包装)
                                   AND (batch_no = '{5}' OR batch_no IS NULL) --批号
                                   AND item_code = '{6}' --物品编码
                                   AND storage_code = '{7}' --库存编码
                                   AND specs = '{8}'
                                   AND item_name = '{9}'
                            ";
            strSql = string.Format(strSql, companyCode, factoryCode, packPrice, salePrice, inPrice, batchNO, itemCode, storageCode, specs, itemName);
            return BaseEntityer.Db.ExecuteScalar<string>(strSql);
        }
        #endregion

        #region 物资库存初始化相关业务处理方法 by  dong_w  2015年4月9日
        /// <summary>
        ///  校验当前
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public int CheckDeptStockInfo(string deptCode)
        {
            string strSql = @"SELECT COUNT(1)
                                  FROM dept_mat_stockdetail t --物资库存明细表
                                 WHERE t.storage_code = '{0}'
                            ";
            strSql = string.Format(strSql, deptCode);
            return BaseEntityer.Db.ExecuteScalar<int>(strSql);
        }

        /// <summary>
        ///  清空当前科室库存
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public int DeleteDeptStockInfo(string deptCode)
        {
            string strSql = @"DELETE dept_mat_stockdetail t --物资库存明细表
                                 WHERE t.storage_code = '{0}'
                            ";
            string strInputSql = @"DELETE FROM dept_mat_input r WHERE r.storage_code = '{0}'";

            strSql = string.Format(strSql, deptCode);
            strInputSql = string.Format(strInputSql, deptCode);

            int delRev = BaseEntityer.Db.ExecuteNonQuery(strSql);

            if (delRev > 0)
                return BaseEntityer.Db.ExecuteNonQuery(strInputSql);
            else
                return -1;
        }

        /// <summary>
        /// 获得科室库存明细编码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="inPrice"></param>
        /// <param name="salePrice"></param>
        /// <returns></returns>
        public string GetDeptStockDetailCode(string itemCode, decimal inPrice, decimal salePrice, string deptCode)
        {
            string strSql = @"
                           SELECT t.stock_code
                              FROM dept_mat_stockdetail t
                             WHERE t.item_code = '{0}'
                               AND t.in_price = '{1}'
                               AND t.sale_price = '{2}'
                               AND t.storage_code = '{3}'
                               AND rownum = 1
                            ";
            strSql = string.Format(strSql, itemCode, inPrice, salePrice, deptCode);
            return BaseEntityer.Db.ExecuteScalar<string>(strSql);
        }

        /// <summary>
        /// 更新科室库存信息
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockNum"></param>
        /// <param name="stockCost"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateDeptStockDetailNum(BaseEntityer dby, string stockCode, decimal stockNum, decimal stockCost, string operCode, string operDate)
        {
            string strSql = @"   
                         UPDATE dept_mat_stockdetail t
                            SET t.store_num  = '{0}',
                                t.store_cost = '{1}',
                                t.sale_cost  = t.store_num * t.sale_price,
                                t.oper_code  = '{2}', --操作员
                                t.oper_date  = to_date('{3}', 'YYYY-MM-DD HH24:MI:SS') --操作日期
                            WHERE t.stock_code = '{4}'
                        ";
            strSql = string.Format(strSql, stockNum, stockCost, operCode, operDate, stockCode);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 更新科室入库数量
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="inNum"></param>
        /// <param name="inCost"></param>
        /// <param name="saleCost"></param>
        /// <param name="placeCode"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateDeptInPutNum(BaseEntityer dby, string stockCode, decimal inNum, decimal inCost, decimal saleCost, string placeCode, string operCode, string operDate)
        {
            string strSql = @"  
                      UPDATE dept_mat_input t
                         SET t.priv_store_num = t.in_num,
                             t.in_num         = '{0}',
                             t.in_cost        = '{1}',
                             t.sale_cost      = '{2}',
                             t.place_code     = '{3}', --
                             t.oper_code      = '{4}', --操作员    
                             t.oper_date      = to_date('{5}', 'YYYY-MM-DD HH24:MI:SS'), --操作日期 
                             t.in_date        = to_date('{5}', 'YYYY-MM-DD HH24:MI:SS') --操作日期
                       WHERE t.stock_code = '{6}'";

            strSql = string.Format(strSql, inNum, inCost, saleCost, placeCode, operCode, operDate, stockCode);
            return dby.ExecuteNonQuery(strSql);
        }

        /// <summary>
        /// 查询出库记录
        /// </summary>
        /// <param name="storageCode"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public decimal GetOutStockRecordbyWhere(string storageCode, string stockCode)
        {
            string strSql = @"  
                     SELECT  nvl(SUM(t.out_num),0)
                      FROM dept_mat_output t
                     WHERE t.stock_code = '{0}'
                       AND t.storage_code = '{1}'";

            strSql = string.Format(strSql, storageCode, stockCode);
            return BaseEntityer.Db.ExecuteScalar<decimal>(strSql);
        }

        /// <summary>
        ///  查询虚库存的记录
        /// </summary>
        /// <param name="storageCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<DEPT_MAT_INPUT> QueryDeptVirtualStockInfo(string storageCode, string beginDate, string endDate)
        {
            string strSql = @"  
                    SELECT t.retail_price, --调拨价
                           t.in_id, --入库流水帐号
                           t.in_bill, --入库单号
                           t.apply_no, --申请流水号
                           t.finance_flag, --财务标志
                           t.virtual_date, --备货入库操作日期
                           t.virtual_oper, --备货入库操作员
                           t.highvalue_barcode, --高值耗材条形码
                           t.highvalue_flag, --高值耗材标志
                           t.oper_date, --操作日期
                           t.oper_code, --操作员
                           t.memo, --备注
                           t.packin_flag, --入库时是否使用大包装入库
                           t.return_in_no, --退掉的入库流水号
                           t.return_num, --已退数量
                           t.out_no, --出库记录流水号
                           t.factory_code, --生产厂家
                           t.company_name, --供货公司名称
                           t.company_code, --供货公司编码
                           t.source_deptname, --来源科室名称
                           t.source_dept, --来源科室
                           t.invoice_date, --发票日期(发票上写的日期)
                           t.invoice_no, --发票号码
                           t.approve_date, --核准日期
                           t.approve_oper, --核准操作员
                           t.exam_date, --审核入库日期
                           t.exam_oper, --审核入库操作员
                           t.apply_date, --申请入库日期
                           t.apply_oper, --申请入库操作员
                           t.in_date, --入库日期
                           t.place_code, --库位编号
                           t.priv_store_num, --入库前库存量
                           t.sale_cost, --零售金额
                           t.sale_price, --零售价格
                           t.in_cost, --入库金额
                           t.in_price, --入库价(大包装)
                           t.pack_qty, --大包装包装数量
                           t.pack_price, --大包装价格
                           t.pack_unit, --大包装单位
                           t.pack_in_num, --大包装入库数量
                           t.min_unit, --最小单位
                           t.in_num, --入库数量
                           t.batch_no, --批号
                           t.specs, --规格
                           t.kind_code, --物品科目编码
                           t.item_name, --物品名称
                           t.item_code, --物品编码
                           t.in_state, --状态(0-申请入库,1-正式入库,2-核准入库)
                           t.trans_type, --交易类型(1-正交易,2-反交易)
                           t.in_class3mean, --入库分类  系统定义
                           t.in_class3, --入库类型  用户定义
                           t.stock_code, --库存流水号
                           t.storage_code, --仓库编码
                           t.in_serial_no, --入库单内序号
                           t.in_list_code, --入库单号
                           t.in_no --入库流水号
                      FROM dept_mat_input t --物资入库记录表
                     WHERE t.in_date >= to_date('{0}', 'YYYY-MM-DD HH24:MI:SS')
                       AND t.in_date <= to_date('{1}', 'YYYY-MM-DD HH24:MI:SS')
                       AND t.storage_code = '{2}'
                       AND t.in_class3 = '005'
                       AND t.in_class3mean = 'Dept_Mat_InType'
                       AND t.in_state = '4'
                    ";

            strSql = string.Format(strSql, beginDate, endDate, storageCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_INPUT>(ds).ToList();
        }


        /// <summary>
        /// 加载科室列表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Dictionary<string, string> LoadDeptLst(BaseEntityer dby, string beginDate, string endDate)
        {

            string strSql = @"
                            SELECT t.storage_code,
                                   (SELECT r.dept_name
                                      FROM dept_dict r
                                     WHERE r.dept_code = t.storage_code) dept_name
                              FROM dept_mat_input t --物资入库记录表
                               WHERE t.in_date >= to_date('{0}', 'YYYY-MM-DD HH24:MI:SS')
                                                   AND t.in_date <= to_date('{1}', 'YYYY-MM-DD HH24:MI:SS')
                                                   AND t.in_class3 = '005'
                                                   AND t.in_class3mean = 'Dept_Mat_InType'
                                                   AND t.in_state = '4'
                             GROUP BY t.storage_code
                            ";
            strSql = string.Format(strSql, beginDate, endDate);

            Dictionary<string, string> dictDept = new Dictionary<string, string>();

            #region 在Read关闭前读取信息 by yan_x {946DFF35-B1CA-4460-B9ED-99EAC83C6CDE}

            //int result = dby.ExecQuery(strSql);
            //if (result == -1)
            //{
            //    return dictDept;
            //}
            dby.Reader = dby.ExecuteReader(strSql);

            #endregion

            if (dby.Reader != null)
            {
                while (dby.Reader.Read())
                {
                    if (!dictDept.ContainsKey(dby.Reader[0].ToString()))
                    {
                        dictDept.Add(dby.Reader[0].ToString(), dby.Reader[1].ToString());
                    }
                }
            }
            return dictDept;
        }

        /// <summary>
        ///  查询虚库存的记录
        /// </summary>
        /// <param name="storageCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<DEPT_MAT_INPUT> QueryDeptVirtualStockInfoByStockCode(string storageCode, string stockCode, string beginDate, string endDate)
        {
            string strSql = @"  
                    SELECT t.retail_price, --调拨价
                           t.in_id, --入库流水帐号
                           t.in_bill, --入库单号
                           t.apply_no, --申请流水号
                           t.finance_flag, --财务标志
                           t.virtual_date, --备货入库操作日期
                           t.virtual_oper, --备货入库操作员
                           t.highvalue_barcode, --高值耗材条形码
                           t.highvalue_flag, --高值耗材标志
                           t.oper_date, --操作日期
                           t.oper_code, --操作员
                           t.memo, --备注
                           t.packin_flag, --入库时是否使用大包装入库
                           t.return_in_no, --退掉的入库流水号
                           t.return_num, --已退数量
                           t.out_no, --出库记录流水号
                           t.factory_code, --生产厂家
                           t.company_name, --供货公司名称
                           t.company_code, --供货公司编码
                           t.source_deptname, --来源科室名称
                           t.source_dept, --来源科室
                           t.invoice_date, --发票日期(发票上写的日期)
                           t.invoice_no, --发票号码
                           t.approve_date, --核准日期
                           t.approve_oper, --核准操作员
                           t.exam_date, --审核入库日期
                           t.exam_oper, --审核入库操作员
                           t.apply_date, --申请入库日期
                           t.apply_oper, --申请入库操作员
                           t.in_date, --入库日期
                           t.place_code, --库位编号
                           t.priv_store_num, --入库前库存量
                           t.sale_cost, --零售金额
                           t.sale_price, --零售价格
                           t.in_cost, --入库金额
                           t.in_price, --入库价(大包装)
                           t.pack_qty, --大包装包装数量
                           t.pack_price, --大包装价格
                           t.pack_unit, --大包装单位
                           t.pack_in_num, --大包装入库数量
                           t.min_unit, --最小单位
                           t.in_num, --入库数量
                           t.batch_no, --批号
                           t.specs, --规格
                           t.kind_code, --物品科目编码
                           t.item_name, --物品名称
                           t.item_code, --物品编码
                           t.in_state, --状态(0-申请入库,1-正式入库,2-核准入库)
                           t.trans_type, --交易类型(1-正交易,2-反交易)
                           t.in_class3mean, --入库分类  系统定义
                           t.in_class3, --入库类型  用户定义
                           t.stock_code, --库存流水号
                           t.storage_code, --仓库编码
                           t.in_serial_no, --入库单内序号
                           t.in_list_code, --入库单号
                           t.in_no --入库流水号
                      FROM dept_mat_input t --物资入库记录表
                   WHERE t.in_date >= to_date('{0}', 'YYYY-MM-DD HH24:MI:SS')
                       AND t.in_date <= to_date('{1}', 'YYYY-MM-DD HH24:MI:SS')
                       AND t.storage_code = '{2}'
                       AND t.stock_code = '{3}'
                       AND t.in_class3 = '005'
                       AND t.in_class3mean = 'Dept_Mat_InType'
                       AND t.in_state = '4'

                    ";

            strSql = string.Format(strSql, beginDate, endDate, storageCode, stockCode);
            DataSet ds = BaseEntityer.Db.GetDataSet(strSql);
            return DataSetToEntity.DataSetToT<DEPT_MAT_INPUT>(ds).ToList();
        }


        /// <summary>
        /// 更新科室库存信息
        /// </summary>
        /// <param name="dby"></param>
        /// <param name="stockCode"></param>
        /// <param name="stockNum"></param>
        /// <param name="stockCost"></param>
        /// <param name="operCode"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateDeptStockDetailAddNum(BaseEntityer dby, string stockCode, decimal stockNum, decimal stockCost, string operCode, string operDate)
        {
            string strSql = @"   
                        UPDATE dept_mat_stockdetail t
                           SET t.store_num  = t.store_num + '{0}',
                               t.store_cost = t.store_cost + '{1}',
                               t.sale_cost  = t.store_num * t.sale_price,
                               t.oper_code  = '{2}', --操作员
                               t.oper_date  = to_date('{3}', 'YYYY-MM-DD HH24:MI:SS') --操作日期
                         WHERE t.stock_code = '{4}'

                        ";
            strSql = string.Format(strSql, stockNum, stockCost, operCode, operDate, stockCode);
            return dby.ExecuteNonQuery(strSql);
        }

        #endregion
    }

}