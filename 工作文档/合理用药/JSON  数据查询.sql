--select * from cant_inp_patient_view;

/*--病人基本信息
select t.yydm as his_code,
       '营口中西医结合医院' as his_mc,
       t.zyh as mz_jz_zyh,--住院号
       t.brid as br_id,--就诊卡号
       t.zycs as lsh_num,-- 门诊流水号/住院次数
       t.brxm as br_mc,--病人姓名
       t.csrq as birthday,--出生日期
       t.xb as sex,--性别 1  为男性   2为女
       nvl(t.tz,0) as weight,--体重 无法确定传入0，单位KG
       nvl(t.sg,0) as height,--身高 无法确定传入0，单位CM
       '处方号' as cfh,--处方号
       '' as review_id,--HIS处方的唯一识别
       t.ksdm as dept_code,--就诊科室编码
       t.ksmc as dept_mc,--就诊科室名称
       t.ysdm as doctor_code,--就诊医生编码
       t.ysmc as doctor_mc,--就诊医生名称
       t.gshcd as gz_tag,--肝损程度
       t.buruzt as is_brq,--哺乳
       t.rszt as is_rs,--妊娠
       t.brzt as data_cat,--病人类型 1 住院 2门诊 3急诊
       t.rsztsj as rs_start_time,--妊娠开始时间
       t.sshcd as sz_tag,--肾损程度
       '' as ybbr,--医保类型
       '' as jun_ren ---军人类型
  from cant_inp_patient_view t;*/

--病人诊断信息
/*select t.brid,
     t.zycs,
     t.zddm as zddm, --诊断编码
     row_number() over(partition by t.brid order by t.zddm) as zdxh, --诊断序号
     t.zdmc as zdmc, --诊断名称
     '处方号' as cfh
from cant_inp_disease_view t;*/

--病人手术信息
/*select t.brid,
     t.zycs,
     row_number() over(partition by t.brid order by t.ssdm) as operxh, --手术序号
     t.ssdm as oper_code, --手术编码
     t.ssmc as oper_mc, --手术名称
     t.ssqk as oper_qk, --手术切口类型
     t.sskssj as oper_ks_time, --手术开始时间
     t.ssjssj as oper_js_time --手术结束时间
from cant_inp_operation_view t;*/

--病人过敏信息
/*select t.yydm,
     t.brid,
     t.zycs,
     row_number() over(partition by t.brid order by t.gmydm) as gmyxh, --过敏源序号
     t.gmydm as gmy_code, --过敏源编码
     t.gmymc as gmy_mc, --过敏源名称
     t.gmzz as gmzz_ms --过敏症状
from cant_inp_allergen_view t;*/

--病人检验信息
/*select t.yydm,
       t.brid,
       t.zycs,
       t.jyxmdm   as lab_code, --检验编码
       t.jyxmmc   as lab_name, --检验名称
       t.jyjg     as lab_result, --检验结果
       t.jyjgbz   as result_flag, --结果标志
       t.jyjgckfw as lab_value, --参考范围
       t.dw       as lab_dw --检验结果单位
  from cant_inp_lab_view t;*/
  
  
  --病人医嘱信息
  
  select t.yydm,
       t.brid,
       t.zycs,
       t.ksdm as dept_code,--开嘱科室编码
       t.ksmc as dept_mc,--开嘱科室名称
       t.ysdm as doctor_code,--开嘱医生编码
       t.ysmc as doctore_mc,--开嘱医生名称
       t.yzdm as ypdm,--药品编码
       t.ypwydm as ypwydm,--药品唯一码
       t.yzmc as ypmc,--药品名称
       t.dcjl as liang_ci,--每次剂量
       t.kfgydw as gydw,--每次剂量单位
       0 as kf_num,--开出数量
       '' as kf_dw,--开出数量单位
       t.kssj as time_kz,--用药开始时间
       0 as cxts,--用药持续天数
       t.jssj as time_tz,--用药停止时间
       t.zxsj as time_zx,--用药执行时间
       t.pc as pinci,--用药频次
       t.czbj as tag,--成组组号
       '处方号' as cfh, --处方号
       0 as cf_type, --处方类型
       t.cqlsyz as sflsyy,--是否临时医嘱
       0 as ypxh,--药品序号
       t.yzxh as yzxh,--医嘱序号
       0 as yzlx,--医嘱类型
       t.gyfsdm as route_code,--给药方式名称
       t.gyfsmc as route_mc,--给药方式名称
       
       
       from cant_inp_orders_view t;


