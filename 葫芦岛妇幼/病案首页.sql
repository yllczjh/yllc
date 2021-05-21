select t.机构编码,
       t.住院病历号,
       t.病人类型编码,
       t.病人类型名称,
       t.病人姓名,
       --TO_NUMBER(REGEXP_REPLACE(T.年龄, '[^-0-9.]', '')) 年龄,
	   t.年龄,
       decode(t.性别, '1', '男', '2', '女') 性别,
       t.出生日期,
       t.身份证号,
       t.婚姻状况,
       t.联系电话,
       t.家庭地址,
       t.工作单位,
       t.入院时间,
       t.出院时间,
       t.病人科室编码,
       t.住院医生编码,
       t.责任护士编码,
       t.管床医生编码,
       t.入院方式编码,
       t.入院病情编码,
       t.疾病编码,
       t.疾病名称,
       t.出院诊断编码,
       t.出院诊断名称,
       t1.*

  from 住院管理_出院病人信息 t,
       (SELECT T.住院病历号,
               SUM(T.归类金额) AS 总金额,
               
               NVL(SUM(CASE T.归类编码
                         WHEN '1' THEN
                          T.归类金额
                       END),
                   0) AS 西药费,
               NVL(SUM(CASE T.归类编码
                         WHEN '2' THEN
                          T.归类金额
                       END),
                   0) AS 成药费,
               NVL(SUM(CASE T.归类编码
                         WHEN '3' THEN
                          T.归类金额
                       END),
                   0) AS 草药费,
               NVL(SUM(CASE T.归类编码
                         WHEN '4' THEN
                          T.归类金额
                       END),
                   0) AS 注射费,
               NVL(SUM(CASE T.归类编码
                         WHEN '5' THEN
                          T.归类金额
                       END),
                   0) AS 手术费,
               NVL(SUM(CASE T.归类编码
                         WHEN '6' THEN
                          T.归类金额
                       END),
                   0) AS 麻醉费,
               NVL(SUM(CASE T.归类编码
                         WHEN '7' THEN
                          T.归类金额
                       END),
                   0) AS 化验费,
               NVL(SUM(CASE T.归类编码
                         WHEN '8' THEN
                          T.归类金额
                       END),
                   0) AS B超费,
               NVL(SUM(CASE T.归类编码
                         WHEN '9' THEN
                          T.归类金额
                       END),
                   0) AS 放射费,
               NVL(SUM(CASE T.归类编码
                         WHEN '10' THEN
                          T.归类金额
                       END),
                   0) AS 检查费,
               NVL(SUM(CASE T.归类编码
                         WHEN '16' THEN
                          T.归类金额
                       
                       END),
                   0) AS 护理费,
               NVL(SUM(CASE T.归类编码
                         WHEN '18' THEN
                          T.归类金额
                       END),
                   0) AS 材料费,
               NVL(SUM(CASE T.归类编码
                         WHEN '19' THEN
                          T.归类金额
                       END),
                   0) AS 治疗费,
               NVL(SUM(CASE T.归类编码
                         WHEN '20' THEN
                          T.归类金额
                       END),
                   0) AS 诊查费,
               NVL(SUM(CASE T.归类编码
                         WHEN '28' THEN
                          T.归类金额
                       END),
                   0) AS 换药费,
               NVL(SUM(CASE T.归类编码
                         WHEN '33' THEN
                          T.归类金额
                       END),
                   0) AS 床位费,
               NVL(SUM(CASE T.归类编码
                         WHEN '40' THEN
                          T.归类金额
                       END),
                   0) AS 检验费,
               NVL(SUM(CASE T.归类编码
                         WHEN '42' THEN
                          T.归类金额
                       END),
                   0) AS 监护费,
               NVL(SUM(CASE T.归类编码
                         WHEN '48' THEN
                          T.归类金额
                       END),
                   0) AS 心电图,
               NVL(SUM(CASE T.归类编码
                         WHEN '49' THEN
                          T.归类金额
                       END),
                   0) AS 卫生材料,
               NVL(SUM(CASE T.归类编码
                         WHEN '50' THEN
                          T.归类金额
                       END),
                   0) AS 接生费,
               NVL(SUM(CASE T.归类编码
                         WHEN '55' THEN
                          T.归类金额
                       END),
                   0) AS 一般诊疗费,
               NVL(SUM(CASE T.归类编码
                         WHEN '56' THEN
                          T.归类金额
                       END),
                   0) AS 大型检查类,
               NVL(SUM(CASE T.归类编码
                         WHEN '57' THEN
                          T.归类金额
                       END),
                   0) AS 成药费中,
               NVL(SUM(CASE T.归类编码
                         WHEN '1' THEN
                          0
                         WHEN '2' THEN
                          0
                         WHEN '3' THEN
                          0
                         WHEN '4' THEN
                          0
                         WHEN '5' THEN
                          0
                         WHEN '6' THEN
                          0
                         WHEN '7' THEN
                          0
                         WHEN '8' THEN
                          0
                         WHEN '9' THEN
                          0
                         WHEN '10' THEN
                          0
                         WHEN '16' THEN
                          0
                         WHEN '18' THEN
                          0
                         WHEN '19' THEN
                          0
                         WHEN '20' THEN
                          0
                         WHEN '28' THEN
                          0
                         WHEN '33' THEN
                          0
                         WHEN '40' THEN
                          0
                         WHEN '42' THEN
                          0
                         WHEN '48' THEN
                          0
                         WHEN '49' THEN
                          0
                         WHEN '50' THEN
                          0
                         WHEN '55' THEN
                          0
                         WHEN '56' THEN
                          0
                         WHEN '57' THEN
                          0
                         else
                          T.归类金额
                       END),
                   0) AS 其他费
        
          FROM (select a.住院病历号,
                       c.名称,
                       a.归类编码,
                       sum(a.总金额) as 归类金额
                  from 住院管理_出院病人处方 a, 基础项目_字典明细 c
                 where a.归类编码 = c.编码
                   and c.分类编码 = 'GB_009001'
                   and a.住院病历号 in (select 住院病历号
                                     from 住院管理_出院病人发票登记
                                    where 机构编码 = a.机构编码)
                 group by a.归类编码, c.名称, a.住院病历号
                 order by a.住院病历号, c.名称) T,
               住院管理_出院病人信息 TT
         WHERE T.住院病历号 = TT.住院病历号
         group by t.住院病历号) t1
 where t.住院病历号 = t1.住院病历号
   --and t.住院病历号 = '20190000021';
